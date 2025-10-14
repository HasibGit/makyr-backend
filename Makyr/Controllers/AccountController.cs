using API.DTOs;
using API.Entities;
using API.Helpers;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController(UserManager<AppUser> userManager, ITokenService tokenService, IMapper mapper) : ControllerBase
    {
        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {
            if (await UserNameExists(registerDto.UserName))
            {
                return BadRequest(new ApiError { StatusCode = 400, Message = "User name already exists" });
            }

            if (await EmailExists(registerDto.Email))
            {
                return BadRequest(new ApiError { StatusCode = 400, Message = "Email already exists" });
            }

            var user = mapper.Map<AppUser>(registerDto);
            user.KnownAs = registerDto.UserName;

            var result = await userManager.CreateAsync(user, registerDto.Password);

            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            return new UserDto
            {
                UserName = user.UserName!,
                Token = await tokenService.CreateToken(user),
                KnownAs = user.KnownAs,
            };

        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            var user = await userManager.Users.Include(user => user.Photo)
                                        .FirstOrDefaultAsync(user => user.Email == loginDto.Email);

            if (user is null)
            {
                return Unauthorized(new ApiError { StatusCode = 401, Message = "Invalid email or password" });
            }

            var isPasswordValid = await userManager.CheckPasswordAsync(user, loginDto.Password);

            if (!isPasswordValid)
            {
                return Unauthorized(new ApiError { StatusCode = 401, Message = "Invalid email or password" });
            }

            return new UserDto
            {
                UserName = user.UserName!,
                Token = await tokenService.CreateToken(user),
                KnownAs = user.KnownAs,
                PhotoUrl = user.Photo?.Url
            };
        }

        private async Task<bool> EmailExists(string email)
        {
            return await userManager.Users.AnyAsync(user => user.Email == email);
        }

        private async Task<bool> UserNameExists(string userName)
        {
            return await userManager.Users.AnyAsync(user => user.NormalizedUserName == userName.ToUpper());
        }
    }
}
