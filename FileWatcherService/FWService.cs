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

namespace FileWatcherService
{
    public partial class FWService : ServiceBase
    {
        StreamWriter streamWriter;
        public string AddMessage = "";
        public FWService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            streamWriter = new StreamWriter(new FileStream(@"C:\\ryurik\\SampleWindowsServiceLogger.txt", System.IO.FileMode.Append));
            this.streamWriter.WriteLine(@"//***********************\\");
            this.streamWriter.WriteLine(@"Starting Checkpoint04 Service at " + DateTime.Now.ToString());
            this.streamWriter.Flush();
            this.streamWriter.Close();            
        }

        protected override void OnStop()
        {
            streamWriter = new StreamWriter(new FileStream(@"C:\\ryurik\\SampleWindowsServiceLogger.txt", System.IO.FileMode.Append));
            this.streamWriter.WriteLine(@"Stopping Checkpoint04 Service at " + DateTime.Now.ToString());
            this.streamWriter.WriteLine(@"\\***********************//");
            this.streamWriter.Flush();
            this.streamWriter.Close();
        }
    }
}
