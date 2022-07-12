using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;

namespace DataAccess.Services.Interfaces
{
    public interface IPhotoService
    {
        public Task<ImageUploadResult> UploadPhotoAsync(IFormFile file);
        public Task<DeletionResult> DeletePhotoAsync(string publicId);
    }
}
