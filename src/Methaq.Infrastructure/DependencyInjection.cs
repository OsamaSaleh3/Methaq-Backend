using Methaq.Application.Interfaces;
using Methaq.Infrastructure.Common.Persistence;
using Methaq.Infrastructure.Services.Emails;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Methaq.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.Configure<EmailSettings>(
                configuration.GetSection("EmailSettings"));

            services.AddScoped<IEmailService, EmailService>();

            return services;
        }
    }
}
