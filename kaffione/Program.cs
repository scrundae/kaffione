using System;
using System.Net;
using System.Runtime.InteropServices;

namespace kaffione
{
    public class Program
    {
        public static string coffee;
        public Program()
        {
        }

        public static void Boot()
        {
            Console.Title = "kaffione: It's pronounced Caffeine";
            Console.BackgroundColor = ConsoleColor.Black;
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine("kaffione: It's pronounced Caffeine");
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Licensed under the MIT license\n");
        }
        
        public static void Main(string[] args)
        {
            Boot();
            while (true)
            {
                Console.Write("Enter a command >>> ");
                string ans = Console.ReadLine();

                if (ans.Contains("_web_obj_download "))
                {
                    string newans = ans.Replace("_web_obj_download ", "");
                    Console.WriteLine("Attempting to download: " + newans +
                                      "; This might take a bit ranging on the size of the site.\n");
                    WebClient wc = new WebClient();
                    wc.Proxy = null;
                    string webData = wc.DownloadString(newans);
                    //wc.DownloadProgressChanged += ProgressUpdated;
                    Console.WriteLine("Web Objects successfully downloaded: ");
                    Console.WriteLine(webData + "\n");
                    Console.WriteLine("Type Y to copy to coffee.");
                    ConsoleKeyInfo ckey = Console.ReadKey(true);
                    if (ckey.Key == ConsoleKey.Y)
                    {
                        coffee = webData;
                        Console.Clear();
                    }
                    else
                    {
                        Console.Clear();
                    }
                }
                else if (ans == "_current_coffee")
                {
                    Console.WriteLine(coffee);
                }
                else if (ans == "_destroy_coffee")
                {
                    coffee = string.Empty;
                }
                else if (ans.Contains("_save_coffee "))
                {
                    string newans = ans.Replace("_save_coffee ", "");
                    try
                    {
                        File.WriteAllText(newans, coffee);
                    }
                    catch
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Write ERROR");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                }
                else if (ans == "cls")
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("WARN: Buffer will clear in 500 milliseconds");
                    System.Threading.Thread.Sleep(500);
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Clear();
                    
                }
                else if (ans.Contains("_buffer_pause "))
                {
                    string newans = ans.Replace("_buffer_pause ", "");
                    int result = Int32.Parse(newans);
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("WARN: Buffer will pause for " + newans + " milliseconds");
                    System.Threading.Thread.Sleep(result);
                    Console.ForegroundColor = ConsoleColor.White;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Command ERROR");
                    Console.ForegroundColor = ConsoleColor.White;
                }
            }
        }
        
        public static void ProgressUpdated(object o, DownloadProgressChangedEventArgs dpcea)
        {
            Console.WriteLine(dpcea.ProgressPercentage + "% completed.");
        }
    }
}