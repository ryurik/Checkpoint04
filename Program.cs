using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.ServiceProcess;
using Checkpoint04.Classes;
using DataModel;

namespace Checkpoint04
{
    static class Program
    {
        static void Main(string[] args)
        {
            if (!Environment.UserInteractive)
            {
                // running as service
                using (var service = new FileWatcherService())
                {
                    ServiceBase.Run(service);
                }
            }
            else
            {
                // running as console app
                EventLogs.AddLog("Starting console app at " + DateTime.Now.ToString());
                var fw = new FileWatcher();
                fw.CreateFileSystemWatcher();

                //const ConsoleKey exitKey = ConsoleKey.Enter;
                const ConsoleKey exitKey = ConsoleKey.Escape; // Esc - exit from Console
                ConsoleKeyInfo cki;
                do
                {
                    cki = Console.ReadKey(true);
                    EventLogs.AddLog(cki.Key.ToString());
                } while (cki.Key != exitKey);

            }
        }
    }
}
