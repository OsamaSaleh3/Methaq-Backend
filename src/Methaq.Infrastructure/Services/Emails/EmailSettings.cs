using System;
using System.Collections.Generic;
using System.Text;

namespace Methaq.Infrastructure.Services.Emails
{
    public class EmailSettings
    {
        public string Host { get; set; } = null!;
        public int Port { get; set; }
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string SenderEmail { get; set; } = null!;
        public string SenderName { get; set; } = null!;
        public string ApiKey { get; set; } = null!;

    }
}
