using System;
using System.Collections.Generic;
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
            string regPattern = @"[a-zA-Z]+_(0[1-9]|[12][0-9]|3[01])(0[1-9]|1[012])(19|20)\d\d.csv";

            Regex reg = new Regex(regPattern);
            MatchCollection matchCollection = reg.Matches(Path.GetFileName(e.FullPath));

            if (matchCollection.Count == 1)
            {
                ProcessFile(e.FullPath);
            }
        }

        private static void ProcessFile(string filename)
        {
            if (!File.Exists(filename))
            {
                Console.WriteLine("Не могу найти файл:{0}", filename);
                //Console.ReadKey();
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
                            ManagerName = secondName,
                            Article = split[2],
                            Price = price,
                        };
                        AddCortegeToDB(cortege);
                    }
                    //Console.WriteLine(readLine);
                    line++;
                }

                s.Close();
            }
            
        }

        private static void AddCortegeToDB(ICortege cortege)
        {
            using (SaleContainer dc = new SaleContainer())
            {
                dc.Database.Log = x => Console.WriteLine(x);

                if (dc.Managers.Where(x=>x.SecondName.ToLower().CompareTo(cortege.ManagerName)))
                Manager m = new Manager(); cor;
                Blog b = new Blog();
                dc.BlogSet.Add(b);

                BlogItem bi = new BlogItem() { User = u1, Blog = b };
                dc.BlogItemSet.Add(bi);

                u1.UserName = "Петя";


                //User u = new User(){UserName = "Федя"};
                //dc.UserSet.Add(u);

                // dc.SaveChanges();

                //dc.UserSet.Attach()

                // var c = dc.Entry<User>(u1);
                var c = dc.UserSet.FirstOrDefault().BlogItems.ToList();


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

        }
    }
}
