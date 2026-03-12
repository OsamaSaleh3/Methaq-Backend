using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Methaq.Application.Common.Interfaces;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace Methaq.Infrastructure.Services.FileService
{
    public class CloudinaryService : IFileService
    {
        private readonly Cloudinary _cloudinary;
        private readonly IOptions<CloudinarySettings> _setting;

        public CloudinaryService(IOptions<CloudinarySettings> setting)
        {
            _setting = setting;
            var account = new Account(
                _setting.Value.CloudName,
                _setting.Value.ApiKey,
                _setting.Value.ApiSecret
                );

            _cloudinary = new Cloudinary(account);
            _cloudinary.Api.Secure = true;
        }

        public async Task<string> UploadAsync(Stream fileStream, string fileName, string folder)
        {
            var uploudParams = new RawUploadParams
            {
                File = new FileDescription(fileName, fileStream),
                Folder = folder,
                PublicId=$"{folder}/{Guid.NewGuid()}"
            };

            var result = await _cloudinary.UploadAsync(uploudParams);
            if(result.Error is not null)
                throw new Exception($"Cloudinary upload failed: {result.Error.Message}");

            return result.SecureUrl.ToString();


        }

        public async Task DeleteAsync(string publicId)
        {

            var deleteParams = new DeletionParams(publicId);
            await _cloudinary.DestroyAsync(deleteParams);
        }
    }
}
