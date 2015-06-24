using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using DataModel;

namespace Checkpoint04
{
    class Program
    {
        private static void OnChanged(object source, FileSystemEventArgs e)
        {
            Console.WriteLine("Somthing was appeared in folder:{0}", e.FullPath);
            string regPattern = @"[a-zA-Z][0-9]+_(0[1-9]|[12][0-9]|3[01])(0[1-9]|1[012])(19|20)\d\d.csv";

            Regex reg = new Regex(regPattern);
            MatchCollection matchCollection = reg.Matches(Path.GetFileName(e.FullPath));

            if (matchCollection.Count == 1)
            {
                if (ProcessFile(e.FullPath))
                {
                    // move file to ProcessedDirectory
                    if (File.Exists(e.FullPath))
                    {
                        File.Move(e.FullPath, Path.GetDirectoryName(e.FullPath));
                    }
                };
            }
        }

        private static bool  ProcessFile(string filename)
        {
            if (!File.Exists(filename))
            {
                Console.WriteLine("File not exists:{0}", filename);
                //Console.ReadKey();
                return false;
            }
            filename = Path.GetFileName(filename);

            using (SalesEntities salesEntities = new SalesEntities())
            {
                if (salesEntities.FileLogs.Any(x => x.FileName.Equals(filename, StringComparison.OrdinalIgnoreCase)))
                {
                    Console.WriteLine(@"File was already processed:{0}", filename);
                    //Console.ReadKey();
                    return false;
                }
            }

            string secondName = new Regex(@"([a-zA-Z]+){1}").Match(filename).Captures[0].ToString();

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
                        Console.WriteLine(@"Ошибка в файле:{0}\nВ строке {1} нет 4х параметров", filename, line);
                    }
                    else if (!DateTime.TryParse(split[0], out dtDateTime))
                    {
                        Console.WriteLine(@"Ошибка в файле:{0}\nВ строке {1} неверная дата : {2}", filename, line, split[0]);
                    }
                    else if (!Decimal.TryParse(split[3].Replace(".", ","), out price))
                    {
                        Console.WriteLine(@"Ошибка в файле:{0}\nВ строке {1} неверная цена : {2}", filename, line, split[3]);
                    }
                    else
                    {
                        Cortege cortege = new Cortege()
                        {
                            Date = dtDateTime,
                            Client = split[1],
                            ManagerName = new ManagerName(){FirstName = "", SecondName = secondName},
                            Article = split[2],
                            Price = price,
                            FileLog = filename
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
            using (SalesEntities salesEntities = new SalesEntities())
            {
                salesEntities.Database.Log = x => Console.WriteLine(x);

                Managers manager = salesEntities.Managers.FirstOrDefault(x => x.SecondName.ToLower().Equals(cortege.ManagerName.SecondName.ToLower()));
                if (manager == null)
                {
                    manager = new Managers(){SecondName = cortege.ManagerName.SecondName,};
                    salesEntities.Managers.Add(manager);
                    salesEntities.SaveChanges();
                }

                Clients client = salesEntities.Clients.FirstOrDefault(x => x.Name.ToLower().Equals(cortege.Client.ToLower()));
                if (client == null)
                {
                    client = new Clients(){Name = cortege.Client};
                    salesEntities.Clients.Add(client);
                    salesEntities.SaveChanges();
                }
                Articles article = salesEntities.Articles.FirstOrDefault(x => x.Name.ToLower().Equals(cortege.Article.ToLower()));
                if (article == null)
                {
                    article = new Articles(){Name = cortege.Article,};
                    salesEntities.Articles.Add(article);
                    salesEntities.SaveChanges();
                }

                FileLogs filelog = salesEntities.FileLogs.FirstOrDefault(x => x.FileName.ToLower().Equals(cortege.FileLog.ToLower()));
                if (filelog == null)
                {
                    filelog = new FileLogs() {Date = cortege.Date, FileName = cortege.FileLog, Managers = manager,};
                }
                // ok, now we have all IDs of objects. We can put this cortege to DB
                var sale = new Sales()
                {
                    Articles = article,
                    Clients = client,
                    Date = cortege.Date,
                    FileLogs = filelog,
                    Sum = cortege.Price,
                };
                salesEntities.Sales.Add(sale);
                salesEntities.SaveChanges();
            }
            
        }

        static void Main(string[] args)
        {
            #region FileSystemWatcher
            FileSystemWatcher fileSystemWatcher = new FileSystemWatcher();
            fileSystemWatcher.Path = Properties.Settings.Default.WorkingDirectory;
            fileSystemWatcher.NotifyFilter = NotifyFilters.LastWrite;
            fileSystemWatcher.Filter = Properties.Settings.Default.FileExtension;
            fileSystemWatcher.Changed += OnChanged;
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
