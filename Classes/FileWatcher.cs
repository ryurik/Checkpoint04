using System.IO;

namespace Checkpoint04.Classes
{
    public class FileWatcher
    {
        public void CreateFileSystemWatcher()
        {
            EventLogs.AddLog(@"WorkWithFiles workWithFiles = new WorkWithFiles();");
            WorkWithFiles workWithFiles = new WorkWithFiles();
            
            EventLogs.AddLog(@"workWithFiles.ProcessAllExistingFilesInDirectory();");
            workWithFiles.ProcessAllExistingFilesInDirectory();

            #region FileSystemWatcher
            EventLogs.AddLog(@"FileSystemWatcher fileSystemWatcher = new FileSystemWatcher();");
            FileSystemWatcher fileSystemWatcher = new FileSystemWatcher();

            fileSystemWatcher.Path = Properties.Settings.Default.WorkingDirectory;
            fileSystemWatcher.NotifyFilter = NotifyFilters.LastWrite;
            fileSystemWatcher.Filter = Properties.Settings.Default.FileExtension;
            fileSystemWatcher.Changed += workWithFiles.OnChanged;
            fileSystemWatcher.EnableRaisingEvents = true;
            #endregion FileSystemWatcher
        }
         
    }
}