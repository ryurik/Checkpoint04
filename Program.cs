using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Checkpoint04.Classes;
using DataModel;

namespace Checkpoint04
{
    class Program
    {
        static void Main(string[] args)
        {
            WorkWithFiles workWithFiles = new WorkWithFiles();

            workWithFiles.ProcessAllExistingFilesInDirectory();
            #region FileSystemWatcher
            FileSystemWatcher fileSystemWatcher = new FileSystemWatcher();
            fileSystemWatcher.Path = Properties.Settings.Default.WorkingDirectory;
            fileSystemWatcher.NotifyFilter = NotifyFilters.LastWrite;
            fileSystemWatcher.Filter = Properties.Settings.Default.FileExtension;
            fileSystemWatcher.Changed += workWithFiles.OnChanged;
            fileSystemWatcher.EnableRaisingEvents = true;
            #endregion FileSystemWatcher

            const ConsoleKey exitKey = ConsoleKey.Enter;
            ConsoleKeyInfo cki;
            do
            {
                cki = Console.ReadKey(true);
            } while (cki.Key != exitKey);
        }
    }
}
