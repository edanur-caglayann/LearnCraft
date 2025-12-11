using LearnCraftt.Application.Common;
using LearnCraftt.Application.Common;
using LearnCraftt.Application.Services.Authentication;


namespace LearnCraftt.Application.Services.Authentication;

public interface IAuthenticationService
{
    Task<ServiceResult<TokenResponse>> LoginAsync( LoginDto dto);

}