using System;
using System.Collections.Generic;
using System.Text;

namespace Methaq.Application.Common.Interfaces
{
    public interface IFileService
    {
        Task<string> UploadAsync(Stream fileStream, string fileName, string folder);
        Task DeleteAsync(string publicId);

    }
}
