using LearnCraftt.Application.Common;
using LearnCraftt.Application.Common.Security;
using LearnCraftt.Application.Dto;
using LearnCraftt.Application.Dto.Auth;
using LearnCraftt.Application.Repositories.EmailConfirmationTokenRepositories;
using LearnCraftt.Application.Repositories.PasswordResetTokenRepositories;
using LearnCraftt.Application.Repositories.UserRepositories;
using LearnCraftt.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using LearnCraftt.Application.Services.Authentication;


namespace LearnCraftt.Application.Services.Authentication;

public class AuthenticationService(
    IUserReadRepository userRead,
    IPasswordHasher<User> passwordHasher,
    ITokenService tokenService,
    IPasswordResetTokenWriteRepository passwordResetTokenWrite,
    IPasswordResetTokenReadRepository passwordResetTokenRead,
    IUserWriteRepository userWrite,
    IEmailConfirmationTokenWriteRepository emailConfirmationTokenWriteRepository,
    IEmailConfirmationTokenReadRepository emailConfirmationTokenReadRepository) : IAuthenticationService
{
    // Login
    // Task<ServiceResult<TokenResponse>> yapisi ile basarili/basarisiz bilgisini,eger basariliysa TokenResponse bilgisini beraber dondurur
    public async Task<ServiceResult<TokenResponse>> LoginAsync(LoginDto dto)
    {
        var user = await userRead.GetSingleAsync(x => x.Email == dto.Email);
        if (user == null)
            return ServiceResult<TokenResponse>.FailResult("User not found.");

        var verify = passwordHasher.VerifyHashedPassword(user, user.PasswordHash, dto.Password);
        if (verify == PasswordVerificationResult.Failed)
            return ServiceResult<TokenResponse>.FailResult("Password is incorrect.");

        // JWT token üretir
        var token = tokenService.GenerateToken(user);
        return ServiceResult<TokenResponse>.SuccessResult(token);
    }

    // Forgot Password
    public async Task<ServiceResult<ForgotPasswordResponseDto>> ForgotPasswordAsync(string email)
    {
        var user = await userRead.GetSingleAsync(x => x.Email == email);
        if (user == null)
            return ServiceResult<ForgotPasswordResponseDto>.FailResult("User not found.");

        // ham(raw) token uretir. Rastgele uretir.Tahmin edilemez.Tek kullanimlik gecici bir anahtardir.
        // Bu token email ile linke gidecek ama db'ye bu haliyle yazilmayacak
        var rawToken = Guid.NewGuid().ToString();

        //token hashlenip db'ye yazilir
        var tokenHash = HashHelper.Hash(rawToken);

        //rest token'i db'ye kaydederim
        await passwordResetTokenWrite.AddAsync(new PasswordResetToken
        {
            UserId = user.Id, // hangi kullanici icin
            TokenHash = tokenHash, // hangi token
            ExpireAt = DateTime.UtcNow.AddMinutes(15), // ne zamana kadar gecerli
            IsUsed = false // kullanildi mi
        });

        await passwordResetTokenWrite.SaveAsync();

        //suan mail atmiyoruz sadece token'ini loglariz
        Console.WriteLine(rawToken);
        return ServiceResult<ForgotPasswordResponseDto>.SuccessResult(
            new ForgotPasswordResponseDto
            {
                EmailSent = true
            }

            // client token'i gormez. backend token'i uretir ve yonetir
        );
    }

    public async Task<ServiceResult<ResetPasswordDto>> ResetPasswordAsync(ResetPasswordDto resetDto)
    {
        //  Gelen token'ı hashle
        var tokenHash = HashHelper.Hash(resetDto.Token);

        // Token geçerli mi kontrol et 
        var resetToken = await passwordResetTokenRead.GetValidTokenAsync(tokenHash);
        if (resetToken == null)
            return ServiceResult<ResetPasswordDto>.FailResult("Invalid or expired token.");

        // Token hangi kullanıcıya ait
        var user = await userRead.GetByIdAsync(resetToken.UserId);
        if (user == null)
            return ServiceResult<ResetPasswordDto>.FailResult("User not found.");

        // Yeni şifreyi hashleyip kullanıcıya ata
        user.PasswordHash = passwordHasher.HashPassword(user, resetDto.NewPassword);


        resetToken.IsUsed = true;
        await userWrite.SaveAsync();
        await passwordResetTokenWrite.SaveAsync();
        return ServiceResult<ResetPasswordDto>.SuccessResult(resetDto);
    }

    // Email verification - send token
    public async Task<ServiceResult<SendEmailVerificationResponseDto>> SendEmailVerificationAsync(Guid userId)
    {
        //Email dogrulama icin token'i uret ve kullaniciya gonder
        var rawToken = Guid.NewGuid().ToString(); // kullanici login olunca ham tokeni uretir
        var tokenHash = HashHelper.Hash(rawToken); // ham token'i hashle

        await emailConfirmationTokenWriteRepository.AddAsync( // yeni dogrulama tokeni olustur 
            new EmailConfirmationToken
            {
                UserId = userId, // hangi kullaniciya
                TokenHash = tokenHash, // hasheldigimiz token ile 
                ExpireAt = DateTime.UtcNow.AddMinutes(30),
                IsUsed = false
            });

        await emailConfirmationTokenWriteRepository.SaveAsync();

        // Token'i kullaniciya gonderiyor
        Console.WriteLine(rawToken);

        return ServiceResult<SendEmailVerificationResponseDto>.SuccessResult(
            new SendEmailVerificationResponseDto
            {
                EmailSent = true
            });
    }

    // Email verification - confirm token - complete
    public async Task<ServiceResult<CompleteEmailVerificationResponseDto>> CompleteEmailVerificationAsync(string token)
    {
        // gönderilen token ile email’ini gerçekten doğrulamak.
        // frontend'den gelen ham token'i hashle, db'deki hash ile karsilastirmak icin
        var tokenHash = HashHelper.Hash(token);

        //token gecerli mi
        var emailToken = await emailConfirmationTokenReadRepository.GetValidTokenAsync(tokenHash);

        if (emailToken == null)
            return ServiceResult<CompleteEmailVerificationResponseDto>.FailResult("Invalid or expired confirmation token.");

        // token'a bagli kullaniciyi bul
        var user = await userRead.GetByIdAsync(emailToken.UserId);
        if (user == null)
            return ServiceResult<CompleteEmailVerificationResponseDto>.FailResult("User not found.");

        // kullanicinin email'ini dogrula
        if (user.IsEmailConfirmed)
            return ServiceResult<CompleteEmailVerificationResponseDto>.FailResult("Email is already confirmed.");
        user.IsEmailConfirmed = true;

        emailToken.IsUsed = true;
        await userWrite.SaveAsync();
        await emailConfirmationTokenWriteRepository.SaveAsync();

        return ServiceResult<CompleteEmailVerificationResponseDto>.SuccessResult(
            new CompleteEmailVerificationResponseDto
            {
                EmailConfirmed = true
            }
        );
    }

    public async Task<ServiceResult<ChangePasswordResponseDto>> ChangePasswordAsync(
        Guid userId,
        ChangePasswordRequestDto changePasswordRequestDto)
    {
        // Yeni sifre, yeni sifre tekrar
        if(changePasswordRequestDto.NewPassword != changePasswordRequestDto.ConfirmNewPassword)
            return ServiceResult<ChangePasswordResponseDto>.FailResult("Passwords do not match.");
        
        // JWT'den gelen userId ile kullaniciyi veritabanindan cekeriz.Kullanici gercekten var mi
        var user = await userRead.GetByIdAsync(userId);
        if(user == null)
            return ServiceResult<ChangePasswordResponseDto>.FailResult("User not found.");
        
        // Kullanicinin girdigi eski sifre dogru mu
        var verifyResult = passwordHasher.VerifyHashedPassword(
            user, 
            user.PasswordHash,
            changePasswordRequestDto.CurrentPassword);
        
        if(verifyResult == PasswordVerificationResult.Failed) 
            return ServiceResult<ChangePasswordResponseDto>.FailResult("Invalid password.");
        
        user.PasswordHash = passwordHasher.HashPassword(user, changePasswordRequestDto.NewPassword);
        
        // guncellenen kullanici kaydi veritabanina yazilir
        await userWrite.UpdateAsync(user);
        await userWrite.SaveAsync(); 
        return ServiceResult<ChangePasswordResponseDto>.SuccessResult(
            new ChangePasswordResponseDto
            {
                IsChanged = true
            });

    }
}