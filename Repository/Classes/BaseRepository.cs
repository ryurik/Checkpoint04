namespace Repository.Classes
{
    public abstract class BaseRepository
    {
        protected DataModel.SalesEntities context = new DataModel.SalesEntities();
    }
}