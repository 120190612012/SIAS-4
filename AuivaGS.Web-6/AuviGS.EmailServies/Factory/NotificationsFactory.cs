

using AuviaGS.Notifications.Implementation;
using Microsoft.Extensions.DependencyInjection;

namespace AuviaGS.Notifications.Factory
{
    public static class NotificationsFactory
    {
        public static void RegisterDependencies(IServiceCollection services)
        {
            services.AddScoped<IEmailSender, EmailSender>();
        }
    }
}