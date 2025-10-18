using System;
using API.DTOs;
using API.Entities;
using API.Helpers;

namespace API.Interfaces;

public interface IUserRepository
{
    Task<PaginatedResponse<UserProfileDto>> GetUsers(UserParams userParams);
    Task<AppUser?> GetUserByUserNameAsync(string userName);

    Task<bool> SaveChangesAsync();
}
