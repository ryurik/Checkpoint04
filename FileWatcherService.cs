using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using Checkpoint04.Classes;

namespace Checkpoint04
{
    public partial class FileWatcherService : ServiceBase
    {
        public FileWatcherService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            EventLogs.AddLog(@"//***********************\\");
            EventLogs.AddLog(@"Starting Checkpoint04 Service at " + DateTime.Now.ToString());
            var fw = new FileWatcher();
            fw.CreateFileSystemWatcher();
        }

        protected override void OnStop()
        {
            EventLogs.AddLog(@"Stopping Checkpoint04 Service at " + DateTime.Now.ToString());
            EventLogs.AddLog(@"\\***********************//");
        }
    }
}
