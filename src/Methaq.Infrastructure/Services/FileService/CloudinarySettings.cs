using System;
using System.Collections.Generic;
using System.Text;

namespace Methaq.Infrastructure.Services.FileService
{
    public class CloudinarySettings
    {
        public string CloudName { get; set; } = null!;
        public string ApiKey { get; set; } = null!;
        public string ApiSecret { get; set; } = null!;
    }
}
