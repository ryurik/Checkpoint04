using System.Collections.Generic;
using System.Linq;
using DataModel;
using Repository.Interaces;
using Repository;

namespace Repository.Classes
{
    public class FileLogsRepository : BaseRepository, IModelRepository<Repository.Models.FileLogs>
    {
        public DataModel.FileLogs ToEntity(Models.FileLogs source)
        {
            return new DataModel.FileLogs() { Id = source.Id, Manager_Id = source.Manager_Id, Date = source.Date, FileName = source.FileName};
        }

        public Repository.Models.FileLogs ToObject(DataModel.FileLogs source)
        {
            return new Repository.Models.FileLogs() { Id = source.Id, FileName = source.FileName, Date  = source.Date};
        }

        #region FileLogsRepository

        public void Add(Models.FileLogs item)
        {
            var i = ToEntity(item);
            context.FileLogs.Add(i);
            SaveChanges();
        }

        public void Remove(Models.FileLogs item)
        {
            var i = ToEntity(item);
            context.FileLogs.Remove(i);
            SaveChanges();
        }

        public void Update(Models.FileLogs item)
        {
            var i = ToEntity(item);
            var fl = context.FileLogs.FirstOrDefault(x => x.Id == i.Id);
            if (fl != null)
            {
                fl.FileName = i.FileName;
                fl.Date = i.Date;

                SaveChanges();
            }
        }

        public IEnumerable<Models.FileLogs> Items
        {
            get
            {
                var q = this.context.FileLogs;
                foreach (var u in q)
                {
                    yield return ToObject(u);
                }
            }
        }

        public void SaveChanges()
        {
            context.SaveChanges();
        }
        #endregion
    }
}