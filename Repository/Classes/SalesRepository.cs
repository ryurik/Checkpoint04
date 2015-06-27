using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.Interaces;

namespace Repository.Classes
{
    public class SalesRepository : BaseRepository, IModelRepository<Repository.Models.Sales>
    {
        DataModel.Sales ToEntity(Models.Sales source)
        {
            return new DataModel.Sales()
            {
                Id = source.Id, 
                Article_Id = source.Article_Id,
                Client_Id = source.Client_Id, 
                Date = source.Date,
                FileLog_Id = source.FileLog_Id,
                Sum = source.Sum
            };
        }

        Repository.Models.Sales ToObject(DataModel.Sales source)
        {
            return new Repository.Models.Sales()
            {
                Id = source.Id, 
                Article_Id = source.Article_Id,
                Client_Id = source.Client_Id,
                Date = source.Date,
                FileLog_Id = source.FileLog_Id,
                Sum = source.Sum
            };
        }


        #region SalesRepository
        public void Add(Models.Sales item)
        {
            var i = ToEntity(item);
            context.Sales.Add(i);
            SaveChanges();
        }

        public void Remove(Models.Sales item)
        {
            var i = ToEntity(item);
            context.Sales.Remove(i);
            SaveChanges();
        }

        public void Update(Models.Sales item)
        {
            var i = ToEntity(item);
            var s = context.Sales.FirstOrDefault(x => x.Id == i.Id);
            if (s != null)
            {
                s.Id = i.Id;
                s.Article_Id = i.Article_Id;
                s.Client_Id = i.Client_Id;
                s.FileLog_Id = i.FileLog_Id;
                s.Date = i.Date;
                s.Sum = i.Sum;
                SaveChanges();
            }
        }

        public System.Collections.Generic.IEnumerable<Models.Sales> Items
        {
            get
            {
                var q = this.context.Sales;
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