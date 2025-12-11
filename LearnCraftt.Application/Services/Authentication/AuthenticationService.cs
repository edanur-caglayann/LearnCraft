using LearnCraftt.Application.Common;
using LearnCraftt.Application.Repositories.UserRepositories;
using LearnCraftt.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using LearnCraftt.Application.Services.Authentication;


namespace LearnCraftt.Application.Services.Authentication;

public class AuthenticationService : IAuthenticationService
{
        private readonly IUserReadRepository userRead;
        private readonly IPasswordHasher<User> passwordHasher;
        private readonly ITokenService tokenService;

        public AuthenticationService(
            IUserReadRepository userRead,
            IPasswordHasher<User> passwordHasher,
            ITokenService tokenService)
        {
            this.userRead = userRead;
            this.passwordHasher = passwordHasher;
            this.tokenService = tokenService;
        }

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
    }

