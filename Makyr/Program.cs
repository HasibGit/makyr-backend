using API.Data;
using API.Entities;
using API.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddIdentityServices(builder.Configuration);

var app = builder.Build();

app.UseCors(x => x.AllowAnyHeader()
                  .AllowAnyMethod()
                  .AllowCredentials()
                  .WithOrigins("http://localhost:4200"));


app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;

try
{
    var context = services.GetRequiredService<DataContext>();
    await context.Database.MigrateAsync();

    var userManager = services.GetRequiredService<UserManager<AppUser>>();

    string testUsername = "testuser";
    string testEmail = "testuser@example.com";
    string testPassword = "minFiCo34$%";

    if (await userManager.FindByNameAsync(testUsername) == null)
    {
        var testUser = new AppUser
        {
            UserName = testUsername,
            Email = testEmail,
            EmailConfirmed = true
        };

        var result = await userManager.CreateAsync(testUser, testPassword);

        if (result.Succeeded)
        {
            var logger = services.GetRequiredService<ILogger<Program>>();
            logger.LogInformation("Test user created successfully!");
        }
        else
        {
            var logger = services.GetRequiredService<ILogger<Program>>();
            logger.LogError("Failed to create test user: {Errors}", string.Join(", ", result.Errors.Select(e => e.Description)));
        }
    }
}
catch (Exception ex)
{
    var logger = services.GetRequiredService<ILogger<Program>>();
    logger.LogError(ex.Message, "An error occured during migration");
}


app.Run();