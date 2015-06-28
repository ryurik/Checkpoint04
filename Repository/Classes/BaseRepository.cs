using System;

namespace Repository.Classes
{
    public abstract class BaseRepository
    {
        protected DataModel.SalesEntities context = new DataModel.SalesEntities();

        public BaseRepository()
        {
            if (Environment.UserInteractive)
                context.Database.Log = x => Console.WriteLine(x);
        }
    }
}