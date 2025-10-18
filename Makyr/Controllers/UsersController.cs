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
    public class UsersController(IUserRepository userRepository, IPhotoService photoService, IMapper mapper) : ControllerBase
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

        [HttpPost("add-photo")]
        public async Task<ActionResult<PhotoDto>> AddPhotoAsync(IFormFile file)
        {
            var user = await userRepository.GetUserByUserNameAsync(User.GetUserName());

            if (user is null)
            {
                return BadRequest(new ApiError(401, "User not found"));
            }

            var result = await photoService.AddPhotoAsync(user, file);

            if (result.Error is not null)
            {
                return BadRequest(new ApiError(401, result.Error.Message));
            }

            user.Photo = new Photo { PublicId = result.PublicId, Url = result.SecureUrl.AbsoluteUri };

            if (await userRepository.SaveChangesAsync())
            {
                return Created(user.Photo.Url, mapper.Map<PhotoDto>(user.Photo));
            }

            return BadRequest(new ApiError(401, "Profile picture update failed"));
        }

        [HttpPost("delete-photo")]
        public async Task<ActionResult> DeletePhotoAsync()
        {
            var user = await userRepository.GetUserByUserNameAsync(User.GetUserName());

            if (user?.Photo?.PublicId is null)
            {
                return BadRequest(new ApiError(401, "User not found"));
            }

            var result = await photoService.DeletePhotoAsync(user.Photo.PublicId);

            if (result.Error is not null)
            {
                return BadRequest(new ApiError(401, result.Error.Message));
            }

            user.Photo = null;

            if (await userRepository.SaveChangesAsync())
            {
                return Ok();
            }

            return BadRequest(new ApiError(401, "Photo deletion failed"));
        }
    }
}
