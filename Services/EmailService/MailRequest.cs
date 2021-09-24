using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace new_project.Services.EmailService
{
    
    public class MailRequest
    {
        public MailRequest(string reciver, string subject, string body, byte[] attachment)
        {
            this.reciver = reciver;
            this.subject = subject;
            this.body = body;
            this.attachment = attachment;
        }
        public string reciver { get; set; }
        public string subject { get; set; }
        public string body { get; set; }
        public byte[] attachment { get; set; }
    }

}
