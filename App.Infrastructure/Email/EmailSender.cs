using MailKit.Security;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MimeKit;
using MailKit.Net.Smtp;


namespace App.Infrastructure.Email;

public class EmailSender(IOptions<MailSettings> mailSettings,ILogger<EmailSender> logger) : IEmailSender
{
    private readonly MailSettings _mailSettings = mailSettings.Value;
    private readonly ILogger<EmailSender> _logger = logger;

    public async Task SendEmailAsync(string email, string subject, string htmlMessage)
    {
        var senderEmail = _mailSettings.Mail;
        var message = new MimeMessage
        {
            Sender = MailboxAddress.Parse(_mailSettings.Mail), 
            Subject = subject
        };

        message.To.Add(MailboxAddress.Parse(email));

        var builder = new BodyBuilder
        {
            HtmlBody = htmlMessage
        };

        message.Body = builder.ToMessageBody();

        using var smtp = new SmtpClient();

        _logger.LogInformation("Sending email to {email}", email);

        smtp.Connect(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.StartTls);

        smtp.Authenticate(_mailSettings.Mail, _mailSettings.Password);

        await smtp.SendAsync(message);

        smtp.Disconnect(true);
    }
}
