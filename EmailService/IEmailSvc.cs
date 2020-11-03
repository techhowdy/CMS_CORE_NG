using System;
using System.Threading.Tasks;

namespace EmailService
{
    public interface IEmailSvc
    {
        Task SendEmailAsync(string email, string subject, string message, string template);
    }
}
