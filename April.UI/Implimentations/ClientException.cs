using System;
using System.Collections.Generic;
using System.Linq;
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
            var exCode = ex.HResult; // хм...
            var exMessage = ex.Message;

            var screenCapture = new ScreenCapture();
            var screen = screenCapture.Get();

            var versionOS = Environment.OSVersion;
            var userNameOS = Environment.UserName;
            var dotNetVersion = Environment.Version;
        }
    }
}
