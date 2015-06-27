using System.Collections.Generic;
using System.Linq;
using Repository.Interaces;

namespace Repository.Classes
{
    public class ClientsRepository : BaseRepository, IModelRepository<Repository.Models.Clients>
    {

        DataModel.Clients ToEntity(Models.Clients source)
        {
            return new DataModel.Clients() { Id = source.Id, Name = source.Name };
        }

        Repository.Models.Clients ToObject(DataModel.Clients source)
        {
            return new Repository.Models.Clients() { Id = source.Id, Name = source.Name };
        }

        #region ClientRepository
        public void Add(Models.Clients item)
        {
            var i = ToEntity(item);
            context.Clients.Add(i);
            SaveChanges();
            if (item.Id == 0) // new Item - we must update Item.ID
            {
                item.Id = i.Id;
            }
        }

        public void Remove(Models.Clients item)
        {
            var i = ToEntity(item);
            context.Clients.Remove(i);
            SaveChanges();
        }

        public void Update(Models.Clients item)
        {
            var i = ToEntity(item);
            var c = context.Clients.FirstOrDefault(x => x.Id == i.Id);
            if (c != null)
            {
                c.Name = i.Name;
                SaveChanges();
            }
        }

        public IEnumerable<Models.Clients> Items
        {
            get
            {
                var q = this.context.Clients;
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