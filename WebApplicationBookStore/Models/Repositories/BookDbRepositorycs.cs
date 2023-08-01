namespace WebApplicationBookStore.Models.Repositories
{
    public class BookDbRepositorycs : IBookStoreRepository<Book>
    {
        BookStoreDbContext db;

        public BookDbRepositorycs(BookStoreDbContext _db)
        {
            this.db = _db;

        }

        public void Add(Book entity)
        {
            entity.Id = db.Books.Max(b => b.Id) + 1;
            db.Books.Add(entity);
            db.SaveChanges();
        }

        public void Delete(int id)
        {
            var book = Find(id);
            db.Books.Remove(book);
            db.SaveChanges();
        }

        public Book Find(int id)
        {
            var book = db.Books.SingleOrDefault(b => b.Id == id);
            return book;
        }

        public IList<Book> List()
        {
            return db.Books.ToList();
        }

        public void Update(int id, Book newBook)
        {
            db.Books.Update(newBook);
            db.SaveChanges();
            //var book = Find(id);
            //book.Title = newBook.Title;
            //book.Author = newBook.Author;
            //book.Description = newBook.Description;
            //book.ImageURL = newBook.ImageURL;
        }
    }

}
