using April.UI.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace April.UI.Implimentations
{
    public static class SendMail
    {

        public static void Send(EmailParams eParams)
        {
            SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
            smtp.Credentials = new NetworkCredential("somemail@gmail.com", "mypassword");

            smtp.EnableSsl = true;
            smtp.Send(eParams.Message);
        }
    }
}
