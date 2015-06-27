using System.Collections.Generic;
using System.Linq;
using Repository.Interaces;
using Repository;

namespace Repository.Classes
{
    public class ManagerRepository : IModelRepository<Repository.Models.Manager>
    {
        private DataModel.SalesEntities context = new DataModel.SalesEntities();

        DataModel.Managers ToEntity(Models.Manager source)
        {
            return new DataModel.Managers() { Id = source.Id, FirstName = source.FirstName, SecondName = source.SecondName };
        }

        Repository.Models.Manager ToObject(DataModel.Managers source)
        {
            return new Repository.Models.Manager() { Id = source.Id, FirstName = source.FirstName, SecondName = source.SecondName };
        }

        #region ManagerRepository
        public void Add(Models.Manager item)
        {
            var i = ToEntity(item);
            context.Managers.Add(i);
            SaveChanges();
        }

        public void Remove(Models.Manager item)
        {
            var i = ToEntity(item);
            context.Managers.Remove(i);
            SaveChanges();
        }

        public void Update(Models.Manager item)
        {
            var i = ToEntity(item);
            var u = context.Managers.FirstOrDefault(x => x.Id == i.Id);
            if (u != null)
            {
                u.FirstName = i.FirstName;
                u.SecondName = i.SecondName;
                SaveChanges();
            }
        }

        public IEnumerable<Models.Manager> Items
        {
            get
            {
                var q = this.context.Managers;
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