using MailKit.Net.Smtp;
using MailKit.Security;
using Methaq.Application.Common.Interfaces;
using Microsoft.Extensions.Options;
using MimeKit;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace Methaq.Infrastructure.Services.Emails;

public class EmailService : IEmailService
{
    private readonly EmailSettings _setting;

    public EmailService(IOptions<EmailSettings> setting)
    {
        _setting = setting.Value;
    }

    public async Task SendEmailAsync(string toEmail, string subject, string body)
    {
        var client = new SendGridClient(_setting.ApiKey);
        var from = new EmailAddress(_setting.SenderEmail, _setting.SenderName);
        var to = new EmailAddress(toEmail);
        var msg = MailHelper.CreateSingleEmail(from, to, subject, null, body);
        await client.SendEmailAsync(msg);
    }
}
