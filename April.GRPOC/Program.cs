using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.IO;
using System.Threading;
using System.Diagnostics;
using System.IO.Compression;

namespace April.GRPOC
{
    class Program
    {
        static bool downloadComplite;

        static string fileName = $"lp{DateTime.Now.ToString("yyyy-MM-dd")}-1";
        static string dirDownload = @"C:\AprilDownload";
        static Uri url = new Uri(@"http://www.grls.rosminzdrav.ru/GetLimPrice.aspx?FileGUID=50ae5bc4-ad8f-47c0-9462-e2780378bb8d&UserReq=6226967");

        static void Main(string[] args)
        {
            if (File.Exists($@"{dirDownload}\{fileName}.zip"))
            {
                File.Delete($@"{dirDownload}\{fileName}.zip");
            }

            Console.WriteLine($"Try download from {url}.");

            var client = new WebClient();
            client.DownloadProgressChanged += Client_DownloadProgressChanged;
            client.DownloadFileCompleted += Client_DownloadFileCompleted;
            client.DownloadFileAsync(url, $@"{dirDownload}\{fileName}.zip");

            while (!downloadComplite)
            {
                Thread.Sleep(1000);
            }

            Console.ReadKey();
        }

        private static void Client_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            Console.WriteLine($"Download complite.");

            try
            {
                Console.WriteLine($"Try unzip.");

                if (File.Exists($@"{dirDownload}\{fileName}.xls"))
                {
                    File.Delete($@"{dirDownload}\{fileName}.xls");
                }

                ZipFile.ExtractToDirectory($@"{dirDownload}\{fileName}.zip", dirDownload);
                Console.WriteLine($"Unzip succsess.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unzip error: {ex.Message}");
            }

            downloadComplite = true;
        }

        private static void Client_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
           
        }
    }
}
