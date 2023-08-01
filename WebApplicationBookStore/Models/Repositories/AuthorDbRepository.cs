namespace WebApplicationBookStore.Models.Repositories
{
    public class AuthorDbRepository : IBookStoreRepository<Author>
    {
        BookStoreDbContext db;

        public AuthorDbRepository(BookStoreDbContext _db)
        {
           this.db = _db;
        }
        public void Add(Author entity)
        {
            entity.Id = db.Authors.Max(b => b.Id) + 1;
            db.Authors.Add(entity);
            db.SaveChanges();
        }

        public void Delete(int id)
        {
            var author = Find(id);
            db.Authors.Remove(author);
            db.SaveChanges();
        }

        public Author Find(int id)
        {
            var author = db.Authors.SingleOrDefault(b => b.Id == id);
            return author;
        }

        public IList<Author> List()
        {
            return db.Authors.ToList();
        }

        public void Update(int id, Author newAuthor)
        {
            db.Authors.Update(newAuthor);
            db.SaveChanges();
            //var author = Find(id);
            //author.Id = newAuthor.Id;
            //author.FullName = newAuthor.FullName;

        }
    }

}
