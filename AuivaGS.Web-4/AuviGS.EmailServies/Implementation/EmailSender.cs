using AuviaGS.EmailService;
using MailKit.Net.Smtp;
using MimeKit;

namespace AuviaGS.Notifications.Implementation
{
    public class EmailSender : IEmailSender
    {
        private readonly EmailConfiguration _emailConfiger;
        public EmailSender(EmailConfiguration emailConfiger)
        {
            _emailConfiger = emailConfiger;
        }

        public void SendEmail(Message message)
        {
            var emailMessage = CreateEmailMessage(message);
            Send(emailMessage);
        }

        private MimeMessage CreateEmailMessage(Message message)
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress(_emailConfiger.UserName));
            emailMessage.To.AddRange(message.To);
            emailMessage.Subject = message.Subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = message.Content };
            return emailMessage;
        }

        private void Send(MimeMessage mailMessage)
        {
            using var client = new SmtpClient();
            try
            {
                client.Connect(_emailConfiger.SmtpServer, 465, true);
                client.AuthenticationMechanisms.Remove("XOAUTH2");
                client.Authenticate(_emailConfiger.UserName, _emailConfiger.Password);
                client.Send(mailMessage);
            }
            catch
            {
                throw;
            }
            finally
            {
                client.Disconnect(true);
                client.Dispose();
            }
        }
    }
}