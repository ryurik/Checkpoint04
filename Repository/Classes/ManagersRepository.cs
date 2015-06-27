using System.Collections.Generic;
using System.Linq;
using Repository.Interaces;
using Repository;

namespace Repository.Classes
{
    public class ManagersRepository : BaseRepository, IModelRepository<Repository.Models.Managers>
    {

        DataModel.Managers ToEntity(Models.Managers source)
        {
            return new DataModel.Managers() { Id = source.Id, FirstName = source.FirstName, SecondName = source.SecondName };
        }

        Repository.Models.Managers ToObject(DataModel.Managers source)
        {
            return new Repository.Models.Managers() { Id = source.Id, FirstName = source.FirstName, SecondName = source.SecondName };
        }

        #region ManagerRepository
        public void Add(Models.Managers item)
        {
            var i = ToEntity(item);
            context.Managers.Add(i);
            SaveChanges();
        }

        public void Remove(Models.Managers item)
        {
            var i = ToEntity(item);
            context.Managers.Remove(i);
            SaveChanges();
        }

        public void Update(Models.Managers item)
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

        public IEnumerable<Models.Managers> Items
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