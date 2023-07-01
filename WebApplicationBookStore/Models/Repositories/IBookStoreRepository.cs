namespace WebApplicationBookStore.Models.Repositories
{
    public interface IBookStoreRepository<TEntity>
    {
        IList<TEntity> List();
        TEntity Find(int id); 
        void Delete(int id);
        void Add(TEntity entity);
        void Update(TEntity entity);
    }
}
