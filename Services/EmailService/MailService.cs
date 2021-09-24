using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace new_project.Services.EmailService
{
    public interface MailService
    {
        Task SendEmailAsync(MailRequest mailRequest);
    }
}
