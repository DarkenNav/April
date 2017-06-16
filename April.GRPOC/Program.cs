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
using Excel;
using System.Data;

namespace April.GRPOC
{
    class Program
    {
        static bool downloadComplite, unzipComplite, convertComplite;

        static string fileName = $"lp{DateTime.Now.ToString("yyyy-MM-dd")}-1";
        static string dirDownload = @"C:\AprilDownload";

        static string uploadFile = $@"{dirDownload}\{fileName}.zip";
        static string unzipFile = $@"{dirDownload}\{fileName}.xls";

        static Uri url = new Uri(@"http://www.grls.rosminzdrav.ru/GetLimPrice.aspx?FileGUID=50ae5bc4-ad8f-47c0-9462-e2780378bb8d&UserReq=6226967");

        static long progressByte;
        static long totalByte;
        static string downloadStatus
        {
            get
            {
                return $"DownLoad {progressByte}/{totalByte}";
            }
        }


        static void Main(string[] args)
        {

            DownloadFile();

            while (!downloadComplite && !unzipComplite && !convertComplite)
            {
                if (!downloadComplite)
                {
                    Console.Write($"\r{downloadStatus} ");
                }

                Thread.Sleep(1000);
            }


            Console.ReadKey();
        }

        private static void DownloadFile()
        {
            if (File.Exists(uploadFile))
            {
                File.Delete(uploadFile);
            }

            Console.WriteLine($"Try download from url: {url}.");

            var client = new WebClient();
            client.DownloadProgressChanged += Client_DownloadProgressChanged;
            client.DownloadFileCompleted += Client_DownloadFileCompleted;
            client.DownloadFileAsync(url, uploadFile);
        }

        private static void Client_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            Console.WriteLine("Download complite.");

            if (UnZipFile())
            {
                UploadToDataTable();
            }

            downloadComplite = true;
        }

        private static void Client_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            progressByte = e.BytesReceived;
            totalByte = e.TotalBytesToReceive;
        }

        private static bool UnZipFile()
        {
            var result = false;
            try
            {
                Console.WriteLine("Try unzip.");

                if (File.Exists(unzipFile))
                {
                    File.Delete(unzipFile);
                }

                ZipFile.ExtractToDirectory(uploadFile, dirDownload);
                result = true;

                Console.WriteLine("Unzip complite.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unzip error: {ex.Message}");
            }

            unzipComplite = true;
            return result;
        }

        private static void UploadToDataTable()
        {
            try
            {
                Console.WriteLine("Try load to DataTable.");

                using (var stream = File.Open(unzipFile, FileMode.Open, FileAccess.Read))
                {
                    var excelReader = ExcelReaderFactory.CreateBinaryReader(stream);
                    var dataSet = excelReader.AsDataSet();

                    var dataTable = dataSet.Tables[0];
                }

                Console.WriteLine("Load to DataTable complite.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Load to DataTable error: {ex.Message}");
            }

            convertComplite = true;
        }
    }
}
