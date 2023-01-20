

using AuviaGS.EmailService;

namespace AuviaGS.Notifications
{
    public interface IEmailSender  
    {
        void SendEmail(Message message);
    }
}