using API.DTOs;
using API.Entities;
using API.Extensions;
using API.Helpers;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController(IUserRepository userRepository, IMapper mapper) : ControllerBase
    {
        [HttpPut]
        public async Task<ActionResult> UpdateProfile(ProfileUpdateDto profileUpdateDto)
        {
            var user = await userRepository.GetUserByUserNameAsync(User.GetUserName());

            if (user is null)
            {
                return NotFound(new ApiError(404, "User not found"));
            }

            mapper.Map(profileUpdateDto, user);

            if (await userRepository.SaveChangesAsync())
            {
                return NoContent();
            }

            return BadRequest(new ApiError(400, "Profile update failed"));
        }
    }
}
