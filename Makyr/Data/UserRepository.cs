using System;
using API.DTOs;
using API.Entities;
using API.Helpers;
using API.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace API.Data;

public class UserRepository(DataContext context, IMapper mapper) : IUserRepository
{
    public async Task<AppUser?> GetUserByUserNameAsync(string userName)
    {
        return await context.Users
                            .Include(user => user.Photo)
                            .Where(user => user.NormalizedUserName == userName.ToUpper())
                            .FirstOrDefaultAsync();
    }

    public async Task<PaginatedResponse<UserProfileDto>> GetUsers(UserParams userParams)
    {
        var query = context.Users.AsQueryable();

        query = query.Where(user => user.UserName != userParams.UserName);

        query.OrderByDescending(x => x.Created);

        return await PaginatedResponse<UserProfileDto>
                            .CreateAsync(query.ProjectTo<UserProfileDto>(mapper.ConfigurationProvider), userParams.PageNumber, userParams.PageSize);
    }

    public async Task<bool> SaveChangesAsync()
    {
        return await context.SaveChangesAsync() > 0;
    }
}
