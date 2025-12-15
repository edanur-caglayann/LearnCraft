using LearnCraftt.Application.Common;
using LearnCraftt.Application.Common;
using LearnCraftt.Application.Dto;
using LearnCraftt.Application.Dto.Auth;
using LearnCraftt.Application.Services.Authentication;


namespace LearnCraftt.Application.Services.Authentication;

public interface IAuthenticationService
{
    Task<ServiceResult<TokenResponse>> LoginAsync( LoginDto dto);
    Task<ServiceResult<ForgotPasswordResponseDto>> ForgotPasswordAsync(string email);
    
    Task<ServiceResult<ResetPasswordDto>> ResetPasswordAsync(ResetPasswordDto dto);

    Task<ServiceResult<SendEmailVerificationResponseDto>> SendEmailVerificationAsync(Guid userId);

    Task<ServiceResult<CompleteEmailVerificationResponseDto>> CompleteEmailVerificationAsync(string token);
    
    Task<ServiceResult<ChangePasswordResponseDto>>ChangePasswordAsync(Guid userId, ChangePasswordRequestDto changePasswordRequestDto);
// userId'yi dto'dan alsaydik(yani client'tan gelseydi)guvenlik acigi olurdu. baska bir kullanicinin userId'si girilebilir.
// Suan bu kullanim ile JWT'den aliyoruz. JWT login sirasinda backend tarafindan uretilir. Icinde userId claim'i vardir.degistirilemez(signature)
// servis sadece is kuralini uygular, JWT, HTTP nedir bilmez bu nednle imza olarak UserId iceir.
}