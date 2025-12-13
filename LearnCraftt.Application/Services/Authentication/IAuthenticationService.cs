using LearnCraftt.Application.Common;
using LearnCraftt.Application.Common;
using LearnCraftt.Application.Dto.Auth;
using LearnCraftt.Application.Services.Authentication;


namespace LearnCraftt.Application.Services.Authentication;

public interface IAuthenticationService
{
    Task<ServiceResult<TokenResponse>> LoginAsync( LoginDto dto);
    Task<ServiceResult<ForgotPasswordResponseDto>> ForgotPasswordAsync(string email);
    
    Task<ServiceResult<ResetPasswordDto>> ResetPasswordAsync(ResetPasswordDto dto);


}