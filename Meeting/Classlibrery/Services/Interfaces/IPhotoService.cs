using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Services.Interfaces
{
    public interface IPhotoService
    {
        public Task<ImageUploadResult> UploadPhotoAsync(IFormFile file);
        public Task<DeletionResult> DeletePhotoAsync(string publicId);
    }
}
