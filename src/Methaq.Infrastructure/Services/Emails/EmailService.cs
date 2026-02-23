using MailKit.Net.Smtp;
using Methaq.Application.Interfaces;
using Microsoft.Extensions.Options;
using MimeKit;

namespace Methaq.Infrastructure.Services.Emails;

public class EmailService : IEmailService
{
    private readonly EmailSettings _setting;
    public EmailService(IOptions<EmailSettings> setting)
    {
        _setting= setting.Value;
    }
    public async Task SendEmailAsync(string toEmail, string subject, string body)
    {
        var message = new MimeMessage();
        message.From.Add(new MailboxAddress(_setting.SenderName, _setting.SenderEmail));
        message.To.Add(new MailboxAddress("", toEmail));
        message.Subject = subject;
        message.Body = new TextPart("html")
        {
            Text = body
        };

        using var client = new SmtpClient();
        await client.ConnectAsync(_setting.Host,_setting.Port,true);
        await client.AuthenticateAsync(_setting.Username, _setting.Password);
        await client.SendAsync(message);
        await client.DisconnectAsync(true);
    }
}
