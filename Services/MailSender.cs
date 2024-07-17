using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using MimeKit.Text;

namespace Vangoph.Services;

public class MailSender
{
    public async Task SendAsync(string to, string subject, string text)
    {
        // create message
        var email = new MimeMessage();
        email.From.Add(MailboxAddress.Parse("vangoph.6844@hotmail.com"));
        email.To.Add(MailboxAddress.Parse(to));
        email.Subject = subject;
        email.Body = new TextPart(TextFormat.Plain) { Text = text };

        // send email
        using (var smtp = new SmtpClient())
        {
            await smtp.ConnectAsync("smtp.office365.com", 587, SecureSocketOptions.StartTls);
            await smtp.AuthenticateAsync("vangoph.6844@hotmail.com", "Vangoph_6844");
            await smtp.SendAsync(email);
            await smtp.DisconnectAsync(true);
        }
    }
}