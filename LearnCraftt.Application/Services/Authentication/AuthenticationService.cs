using LearnCraftt.Application.Common;
using LearnCraftt.Application.Common.Security;
using LearnCraftt.Application.Dto.Auth;
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
    IUserWriteRepository userWrite) : IAuthenticationService
{       
        //LOGIN
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

        // FORGOT PASSWORD
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

}

