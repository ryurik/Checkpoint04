using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using DataModel;
using Repository.Classes;

namespace Checkpoint04.Classes
{
    public class WorkWithFiles
    {
        private const string RegPattern = @"([a-zA-Z]){1,}([0-9]){2}_(0[1-9]|[12][0-9]|3[01])(0[1-9]|1[012])(19|20)\d\d.csv";
        private const string SecondNamePattern = @"^(.+)_.*$";

        private static Repository.Interaces.IModelRepository<Repository.Models.Articles> _articlesRepository = new Repository.Classes.ArticlesRepository();
        private static Repository.Interaces.IModelRepository<Repository.Models.Clients> _clientsRepository = new Repository.Classes.ClientsRepository();
        private static Repository.Interaces.IModelRepository<Repository.Models.FileLogs> _filelogsRepository = new Repository.Classes.FileLogsRepository();
        private static Repository.Interaces.IModelRepository<Repository.Models.Managers> _managersRepository = new Repository.Classes.ManagersRepository();
        private static Repository.Interaces.IModelRepository<Repository.Models.Sales> _salesRepository = new Repository.Classes.SalesRepository();


        public void OnChanged(object source, FileSystemEventArgs e)
        {
            Console.WriteLine(@"Something was appeared in folder:{0}", e.FullPath);

            ProcessFile(e.FullPath);
        }

        public void ProcessAllExistingFilesInDirectory()
        {
            foreach (var f in Directory.GetFiles(Properties.Settings.Default.WorkingDirectory, Properties.Settings.Default.FileExtension))
            {
                Console.WriteLine(@"Process file:{0}", f);
                ProcessFile(f);    
            }
        }

        private static void ProcessFile(string filename)
        {
            Regex reg = new Regex(RegPattern);
            MatchCollection matchCollection = reg.Matches(Path.GetFileName(filename));

            if (matchCollection.Count == 1)
            {
                if (ProcessValidFile(filename))
                {
                    // move file to ProcessedDirectory
                    if (File.Exists(filename))
                    {
                        File.Move(filename, Properties.Settings.Default.ProcessedDirectory + Path.GetFileName(filename));
                    }
                }
                else
                {
                    File.Move(filename, Properties.Settings.Default.BadFilesDirectory + Path.GetFileName(filename));
                };
            }
            
        }

        private static bool ProcessValidFile(string filename)
        {
            if (!File.Exists(filename))
            {
                Console.WriteLine(@"File not exists:{0}", filename);
                return false;
            }
            string shortfilename = Path.GetFileName(filename);

            var filelogs = _filelogsRepository.Items.FirstOrDefault(x => x.FileName.Equals(filename, StringComparison.OrdinalIgnoreCase));

            if (filelogs != null)
            {
                Console.WriteLine(@"File was already processed:{0}", shortfilename);
                return false;
            }

            string secondName = new Regex(SecondNamePattern).Match(shortfilename).Groups[1].ToString();

            FileInfo f = new FileInfo(filename);
            using (StreamReader s = f.OpenText())
            {
                int line = 1;
                string readLine;
                while ((readLine = s.ReadLine()) != null)
                {
                    string[] split = Regex.Split(readLine, @",");
                    DateTime dtDateTime;
                    Decimal price;
                    if (split.Count() != 4)
                    {
                        Console.WriteLine(@"Error in file:{0}\nRow: {1} missing 4 parameters", shortfilename, line);
                    }
                    else if (!DateTime.TryParse(split[0], out dtDateTime))
                    {
                        Console.WriteLine(@"Error in file:{0}\nRow: {1} wrong date : {2}", shortfilename, line, split[0]);
                    }
                    else if (!Decimal.TryParse(split[3].Replace(".", ","), out price))
                    {
                        Console.WriteLine(@"Error in file:{0}\nRow: {1} wrong price : {2}", shortfilename, line, split[3]);
                    }
                    else
                    {
                        Cortege cortege = new Cortege()
                        {
                            Date = dtDateTime,
                            Client = split[1],
                            ManagerName = new ManagerName() { FirstName = "", SecondName = secondName },
                            Article = split[2],
                            Price = price,
                            FileLog = shortfilename
                        };
                        AddCortegeToDb(cortege);
                    }
                    //Console.WriteLine(readLine);
                    line++;
                }

                s.Close();
            }
            return true;
        }

        private static void AddCortegeToDb(ICortege cortege)
        {
            var managers = _managersRepository.Items.FirstOrDefault(x => x.SecondName.ToLower().Equals(cortege.ManagerName.SecondName.ToLower()));

            if (managers == null)
            {
                managers = new Repository.Models.Managers(){ FirstName = "", SecondName = cortege.ManagerName.SecondName, };
                _managersRepository.Add(managers);
            }

            var clients = _clientsRepository.Items.FirstOrDefault(x => x.Name.ToLower().Equals(cortege.Client.ToLower()));
            if (clients == null)
            {
                clients = new Repository.Models.Clients() { Name = cortege.Client };
                _clientsRepository.Add(clients);
            }

            var articles = _articlesRepository.Items.FirstOrDefault(x => x.Name.ToLower().Equals(cortege.Article.ToLower()));
            if (articles == null)
            {
                articles = new Repository.Models.Articles() { Name = cortege.Article, };
                _articlesRepository.Add(articles);
            }

            var filelogs = _filelogsRepository.Items.FirstOrDefault(x => x.FileName.ToLower().Equals(cortege.FileLog.ToLower()));
            if (filelogs == null)
            {
                filelogs = new Repository.Models.FileLogs() { Date = cortege.Date, FileName = cortege.FileLog,};
                _filelogsRepository.Add(filelogs);
            }

            // ok, now we have all IDs of objects. We can put this cortege to DB
            var sales = new Repository.Models.Sales()
            {
                Articles = articles,
                Clients = clients,
                Date = cortege.Date,
                FileLogs = filelogs,
                Sum = cortege.Price,
            };
            _salesRepository.Add(sales);
        }
         
    }
}