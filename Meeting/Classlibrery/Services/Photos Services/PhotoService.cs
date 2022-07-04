using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using DataAccess.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Services.Photos_Services
{
    public class PhotoService : IPhotoService
    {
        private readonly Cloudinary _cloudinary;

        public PhotoService(IOptions<CloudinarySettings> config)
        {
            Account? account = new Account(
                config.Value.CloudName,
                config.Value.ApiKey,
                config.Value.ApiSecret
            );
            _cloudinary = new Cloudinary(account);
        }

        public async Task<DeletionResult> DeletePhotoAsync(string publicId)
        {
            DeletionParams? deleteParams = new DeletionParams(publicId);
            DeletionResult? result = await _cloudinary.DestroyAsync(deleteParams);
            return result;
        }

        public async Task<ImageUploadResult> UploadPhotoAsync(IFormFile file)
        {
            ImageUploadResult? uploadResult = new ImageUploadResult();
            if (file.Length > 0)
            {
                using Stream? stream = file.OpenReadStream();
                ImageUploadParams? uploadParams = new ImageUploadParams()
                {
                    File = new FileDescription(file.FileName, stream),
                    Transformation = new Transformation().Width(500).Height(500).Crop("fill")
                };
                uploadResult = await _cloudinary.UploadAsync(uploadParams);
            }
            return uploadResult;
        }
    }
}
