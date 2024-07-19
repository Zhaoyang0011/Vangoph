using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using MimeKit.Text;
using Vangoph.Reqeusts;

namespace Vangoph.Services;

public class MailSender
{
    private const string ReplyTemplate = @"
Subject: Thank You for Contacting Us

Dear {0},

Thank you for reaching out to us through our Contact Us page. We have received your message and will address your request as soon as possible.

We aim to respond within as soon as possible.

Thank you for your patience.

Best regards,
Customer Service Team
";

    private const string ShipTemplate = @"
Dear Customer,

We are pleased to inform you that your package has been shipped. Here are the details of your order:

- Tracking ID: {0}
- Item Number: {1}
- Description: {2}

You can use the tracking ID to check the latest status of your package on our shipping partner's website.

Thank you for shopping with us!

Best regards,
Vangoph's Backyard";

    public async Task NotifyAdmin(ContactRequest request)
    {
        string subject = $"Message from {request.Name}";
        string text = $"User email: ${request.Email}\n"
                      + $"User Message:\n\n"
                      + $"{request.Description}";
        await SendAsync("vangoph.6844@hotmail.com", subject, text);
    }

    public async Task ReplyUser(ContactRequest request)
    {
        string reply = string.Format(ReplyTemplate, request.Name);
        await SendAsync(request.Email, "Thank You for Contacting Us", reply);
    }

    public async Task ShipNotifyAsync(ShipNotifyRequest request)
    {
        string reply = string.Format(ShipTemplate, request.TrackingID, request.Item, request.Description);
        await SendAsync(request.Email, "Tracking Info", reply);
    }

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