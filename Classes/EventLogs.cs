using System;
using System.IO;

namespace Checkpoint04.Classes
{
    public static class EventLogs
    {
        private static StreamWriter streamWriter;

        public static void AddLog(string logString)
        {
            streamWriter = new StreamWriter(new FileStream(Properties.Settings.Default.LogFileName, System.IO.FileMode.Append));
            streamWriter.WriteLine(String.Format("{0}: {1}", DateTime.Now.ToString(), logString));
            streamWriter.Flush();
            streamWriter.Close();
        }
    }
}