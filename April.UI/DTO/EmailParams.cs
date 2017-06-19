using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace April.UI.DTO
{
    public class EmailParams
    {
        public MailAddress From { get; set; }
        public MailAddress To { get; set; }

        public string Subject { get; set; }
        public string Body { get; set; }

        public bool IsBodyHtml { get; set; }
        public MailMessage Message
        {
            get
            {
                var message = new MailMessage(From, To);

                message.Subject = Subject;
                message.Body = Body;
                message.IsBodyHtml = IsBodyHtml;

                return message;
            }
        }

    }
}
