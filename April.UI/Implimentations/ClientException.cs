using April.UI.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace April.UI.Implimentations
{
    public static class ClientException
    {
        /*
          код ошибки
         текст ошибки
         скриншот рабочего стола пользователя (задание 9)
         версия операционной системы
         имя пользователя в ОС
         последняя версия NET Framework, установленная на компьютере
        */

        public static void Process(object sender, UnhandledExceptionEventArgs e)
        {
            var ex = (Exception)e.ExceptionObject;

            var screenCapture = new ScreenCapture();
            var screen = screenCapture.Get();
            var imgPath = $@"C:\AprilDownload\{Guid.NewGuid().ToString()}.jpg";
            screen.Save(imgPath);

            var htmlMessage = string.Format("",
                $"<p>Exception code: {ex.HResult}</p>",
                $"<p>Exception message: {ex.Message}</p>",
                $"<p>Version OS: {Environment.OSVersion}</p>",
                $"<p>User Name OS: {Environment.UserName}</p>",
                $"<p>dot.Net Version: {Environment.Version}</p>",
                $"<img src={imgPath} />"
                );

            SendMail.Send(new EmailParams {
                From = new MailAddress("somemail@gmail.com", Environment.UserDomainName),
                To = new MailAddress("somemail@yandex.ru"),
                Subject = "ClientException",
                Body = htmlMessage,
                IsBodyHtml = true
            });

        }
    }
}
