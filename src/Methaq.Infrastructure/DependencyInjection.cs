using Methaq.Application.Common.Interfaces;
using Methaq.Domain.ApplicationUsers;
using Methaq.Domain.AttendanceRecords;
using Methaq.Infrastructure.Common;
using Methaq.Infrastructure.Common.Persistence;
using Methaq.Infrastructure.Repositories;
using Methaq.Infrastructure.Services;
using Methaq.Infrastructure.Services.Chat;
using Methaq.Infrastructure.Services.Emails;
using Methaq.Infrastructure.Services.FileService;
using Methaq.Infrastructure.Services.JWT;
using Methaq.Infrastructure.Services.OTP;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Methaq.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {

        var connectionString = configuration.GetConnectionString("DefaultConnection");

        if (connectionString?.StartsWith("postgresql://") == true ||
            connectionString?.StartsWith("postgres://") == true)
        {
            var uri = new Uri(connectionString);
            connectionString = $"Host={uri.Host};Port={uri.Port};Database={uri.AbsolutePath.TrimStart('/')};Username={uri.UserInfo.Split(':')[0]};Password={uri.UserInfo.Split(':')[1]};SSL Mode=Require;Trust Server Certificate=true";
        }

        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(connectionString));

        //services.AddDbContext<ApplicationDbContext>(options =>
        //        options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));


        services.AddIdentity<ApplicationUser, IdentityRole>(options =>
        {
            options.Tokens.EmailConfirmationTokenProvider = TokenOptions.DefaultEmailProvider;
        })
        .AddEntityFrameworkStores<ApplicationDbContext>()
        .AddDefaultTokenProviders();

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = configuration["JwtSettings:Issuer"],
                ValidAudience = configuration["JwtSettings:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(configuration["JwtSettings:SecretKey"]!))
            };

            options.Events = new JwtBearerEvents
            {
                OnMessageReceived = context =>
                {
                    var accessToken = context.Request.Query["access_token"];
                    var path = context.HttpContext.Request.Path;

                    if (!string.IsNullOrEmpty(accessToken) && path.StartsWithSegments("/hubs"))
                    {
                        context.Token = accessToken;
                    }
                    return Task.CompletedTask;
                }
            };
        });

        services.AddSignalR();


        services.Configure<EmailSettings>(configuration.GetSection("EmailSettings"));
        services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));
        services.Configure<SuperAdminSettings>(configuration.GetSection("SuperAdminSettings"));
        services.Configure<CloudinarySettings>(configuration.GetSection("CloudinarySettings"));

        services.AddScoped<IEmailService, EmailService>();
        services.AddScoped<IOtpService, OtpService>();
        services.AddScoped<IJwtTokenService, JwtTokenService>();

        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
        services.AddScoped<IEmployeeRepository, EmployeeRepository>();
        services.AddScoped<IQuranCenterRepository, QuranCenterRepository>();
        services.AddScoped<IEnrollmentRequestRepository, EnrollmentRequestRepository>();
        services.AddScoped<IStudentRepository, StudentRepository>();
        services.AddScoped<ISectionRepository, SectionRepository>();
        services.AddScoped<IGroupChatRepository, GroupChatRepository>();
        services.AddScoped<ILectureRepository, LectureRepository>();
        services.AddScoped<IAttendanceRecordRepository, AttendanceRecordRepository>();
        services.AddScoped<ISectionTaskRepository, SectionTaskRepository>();
        services.AddScoped<IFinalReportRepository, FinalReportRepository>();
        services.AddScoped<IChatSender, ChatSender>();
        services.AddScoped<INotificationRepository, NotificationRepository>();
        services.AddScoped<INotificationService, NotificationService>();
        services.AddScoped<INotificationSender, NotificationSender>();
        services.AddScoped<IFileService, CloudinaryService>();


        return services;
    }
}