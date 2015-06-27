using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.Interaces;


namespace Repository.Classes
{
    class ArticlesRepository : BaseRepository, IModelRepository<Repository.Models.Articles>
    {
        DataModel.Articles ToEntity(Models.Articles source)
        {
            return new DataModel.Articles() { Id = source.Id, Name = source.Name };
        }

        Repository.Models.Articles ToObject(DataModel.Articles source)
        {
            return new Repository.Models.Articles() { Id = source.Id, Name = source.Name };
        }

        #region ArticleRepository
        public void Add(Models.Articles item)
        {
            var i = ToEntity(item);
            context.Articles.Add(i);
            SaveChanges();
        }

        public void Remove(Models.Articles item)
        {
            var i = ToEntity(item);
            context.Articles.Remove(i);
            SaveChanges();
        }

        public void Update(Models.Articles item)
        {
            var i = ToEntity(item);
            var a = context.Articles.FirstOrDefault(x => x.Id == i.Id);
            if (a != null)
            {
                a.Name = i.Name;
                SaveChanges();
            }
        }

        public IEnumerable<Models.Articles> Items
        {
            get
            {
                var q = this.context.Articles;
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
