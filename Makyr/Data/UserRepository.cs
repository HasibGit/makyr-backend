using System;
using API.Entities;
using API.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Data;

public class UserRepository(DataContext context) : IUserRepository
{
    public async Task<AppUser?> GetUserByUserNameAsync(string userName)
    {
        return await context.Users
                            .Include(user => user.Photo)
                            .Where(user => user.NormalizedUserName == userName.ToUpper())
                            .FirstOrDefaultAsync();
    }

    public async Task<bool> SaveChangesAsync()
    {
        return await context.SaveChangesAsync() > 0;
    }
}
