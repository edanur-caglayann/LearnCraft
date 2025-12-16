using LearnCraftt.Application.Common;
using LearnCraftt.Application.Dto.User;
using LearnCraftt.Application.Repositories.UserRepositories;
using LearnCraftt.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace LearnCraftt.Application.Services;

public class UserService(
    IUserReadRepository userRead,
    IUserWriteRepository userWrite,
    IPasswordHasher<User> passwordHasher)
{
    public async Task<ServiceResult<string>> CreateUser(CreateUserDto createUserDto)
    {
        // email var mi
        var emailControl = await userRead.GetSingleAsync(x => x.Email == createUserDto.Email);
        if (emailControl != null)
            return ServiceResult<string>.FailResult("Email already registered.");

        // user olustur
        var user = new User
        {
            UserName = createUserDto.UserName,
            UserSurname = createUserDto.UserSurname,
            Email = createUserDto.Email,
            PasswordHash = "",
        };

        // sifre hashleme
        var hashedPassword = passwordHasher.HashPassword(user, createUserDto.Password);
        user.PasswordHash = hashedPassword;

        await userWrite.AddAsync(user);
        var saveResult = await userWrite.SaveAsync();
        if (saveResult <= 0)
            return ServiceResult<string>.FailResult("User could not be created.");

        return ServiceResult<string>.SuccessResult("User created successfully.");
    }

    public async Task<ServiceResult<string>> UpdateUser(UpdateUserDto updateUserDto, string userId)
    {
        // mevcut kullaniciyi bul. Ayni mail baska kullanicida var mi kontrolu yap
        var existingUser = await userRead.GetSingleAsync(x => x.Id == Guid.Parse((userId)));
        if (existingUser == null)
            return ServiceResult<string>.FailResult("User could not be found.");
        
        if (existingUser.Email != updateUserDto.Email)
        {
            var emailCheck = await userRead.GetSingleAsync(x => x.Email == updateUserDto.Email);
            if (emailCheck != null)
                return ServiceResult<string>.FailResult("Email is already in use by another user.");
        }
        
        var passwordCheck = passwordHasher.VerifyHashedPassword(existingUser, existingUser.PasswordHash, updateUserDto.CurrentPassword);
        if (passwordCheck == PasswordVerificationResult.Failed) //Eğer kullanıcı girdiği şifre mevcut şifre ile eşleşmiyorsa
            return ServiceResult<string>.FailResult("Password is incorrect.");

        //kullanici bilgilerini guncelle
        existingUser.UserName = updateUserDto.UserName;
        existingUser.UserSurname = updateUserDto.UserSurname;
        existingUser.Email = updateUserDto.Email;

        //eger sifre degistirildiyse yeni sifreyi hashle
        if (!string.IsNullOrWhiteSpace(updateUserDto.NewPassword))
        {
            var hashedNewPassword = passwordHasher.HashPassword(existingUser, updateUserDto.NewPassword);
            existingUser.PasswordHash = hashedNewPassword;
        }
        //kaydet
        var saveResult = await userWrite.SaveAsync();
        if (saveResult <= 0)
            return ServiceResult<string>.FailResult("User could not be updated.");

        return ServiceResult<string>.SuccessResult("User updated successfully.");
    }

    public async Task<ServiceResult<string>> DeleteUser(string userId)
    {
        var existingUser = await userRead.GetSingleAsync(x => x.Id == Guid.Parse(userId));
        if (existingUser == null) 
         return ServiceResult<string>.FailResult("User could not be found.");
        
        userWrite.Delete(existingUser);
        var saveResult = await userWrite.SaveAsync();
        if (saveResult <= 0)
            return ServiceResult<string>.FailResult("User could not be deleted.");
        return ServiceResult<string>.SuccessResult("User deleted successfully.");
    }

    public async Task<ServiceResult<UserResponseDto>> GetUserById(Guid userId)
    {
        var existingUser = await userRead.GetSingleAsync(x => x.Id == userId);
        if (existingUser == null) 
            return ServiceResult<UserResponseDto>.FailResult("User could not be found.");
        var response = new UserResponseDto
        {
            Id = existingUser.Id,
            Email = existingUser.Email,
            UserName = existingUser.UserName,
            UserSurname = existingUser.UserSurname,
            ProfileImage = existingUser.ProfileImage
        };
        return ServiceResult<UserResponseDto>.SuccessResult(response);
    }

    public async Task<ServiceResult<List<UserListResponseDto>>> GetAllUsers()
    {
        var users = userRead.GetAll();
        
        var userList = users.Select(x => new UserListResponseDto
        {
            Id = x.Id,
            Email = x.Email,
            UserName = x.UserName,
            UserSurname = x.UserSurname
        }).ToList();
        return ServiceResult<List<UserListResponseDto>>.SuccessResult(userList);
    }

    public async Task<ServiceResult<UserProfileDto>> GetMyProfileAsync(Guid userId)
    {
        var existingUser = await userRead.GetSingleAsync(x => x.Id == userId);
        if (existingUser == null)
            return ServiceResult<UserProfileDto>.FailResult("User could not be found.");

        var userProfileDto = new UserProfileDto
        {
            Id = existingUser.Id,
            UserName = existingUser.UserName,
            UserSurname = existingUser.UserSurname,
            Email = existingUser.Email,
        };
        return ServiceResult<UserProfileDto>.SuccessResult(userProfileDto);
    }
     }
     