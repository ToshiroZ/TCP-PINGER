 using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TCP_PINGER
{
    class Program
    {
        private static bool KillLoop;
        static void Main(string[] args)
        {
            Console.Title = "TCP Pinger ~ Toshi";
            Console.CancelKeyPress += delegate(object sender, ConsoleCancelEventArgs e)
            {
                e.Cancel = true;
                KillLoop = true;
            };
            enterip:
            Console.Write("Enter IP: ");
            var ip = Console.ReadLine();
            if(!ValidateIP(ip))
            {
                goto enterip;
            }
            enterport:
            Console.Write("Enter Port: ");
            if(!int.TryParse(Console.ReadLine(), out int result))
            {
                goto enterport;
            }
            while (!KillLoop)
            {
                Socket sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                var watch = new Stopwatch();
                watch.Start();
                sock.Connect(ip, result);
                watch.Stop();
                sock.Close();
                Console.WriteLine($"Connected to {ip} in {watch.ElapsedMilliseconds}ms");
                Thread.Sleep(200);
            }
            Console.Clear();
            KillLoop = false;
            goto enterip;
        }
        private static bool ValidateIP(string ip)
        {
            try
            {
                return Dns.GetHostAddresses(ip).Any();
            }
            catch
            {
                return false;
            }
        }
    }
}
