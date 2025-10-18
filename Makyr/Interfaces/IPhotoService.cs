using System;
using API.Entities;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Mvc;

namespace API.Interfaces;

public interface IPhotoService
{
    Task<UploadResult> AddPhotoAsync(AppUser user, IFormFile file);
    Task<DeletionResult> DeletePhotoAsync(string photoId);
}
