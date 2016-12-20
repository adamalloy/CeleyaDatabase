using System;
using System.Threading.Tasks;
using MimeKit;
using System.IO;
//using Microsoft.Extensions.DependencyInjection;


namespace ISGeoDatabase.Services
{
    // This class is used by the application to send Email and SMS
    // when you turn on two-factor authentication in ASP.NET Identity.
    // For more details see this link http://go.microsoft.com/fwlink/?LinkID=532713
    public class AuthMessageSender : IEmailSender, ISmsSender
    {
        public async Task SendEmailAsync(string email, string subject, string message)
        {
            var myMessage = new MimeMessage();
            myMessage.From.Add(new MailboxAddress("Adam Alloy", "aralloy@gmail.com"));
            myMessage.To.Add(new MailboxAddress("", email));
            myMessage.Subject = subject;
            myMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = message };
            using (var stream = new FileStream($"c:\\temp\\{Guid.NewGuid()}.eml", FileMode.Create))
            {
                myMessage.WriteTo(stream);
            }

        }

        public Task SendSmsAsync(string number, string message)
        {
            // Plug in your SMS service here to send a text message.
            return Task.FromResult(0);
        }
    }
}
