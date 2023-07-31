namespace WebApplicationBookStore.Models.Repositories
{
    public class BookRepository : IBookStoreRepository<Book>
    {
        List<Book> books;

        public BookRepository()
        {
            books = new List<Book>()
            {
                new Book
                {
                    Id = 1,
                    Title="C# Programming",
                    Description="No description", 
                    ImageURL="csharp.png",
                    Author = new Author{Id=1}
                },
                new Book
                {
                    Id = 2,
                    Title="Java Programming",
                    Description="No description",
                    ImageURL="java.png",
                    Author = new Author{Id=2}
                },
                new Book
                {
                    Id = 3,
                    Title="PHP Programming",
                    Description="No description",
                    ImageURL="Php.png",
                    Author = new Author{Id=3}
                }
            };
        }

        public void Add(Book entity)
        {
            entity.Id = books.Max( b=> b.Id) + 1;
            books.Add(entity);
        }

        public void Delete(int id)
        {
            var book = Find(id);
            books.Remove(book);
        }

        public Book Find(int id)
        {
            var book = books.SingleOrDefault(b => b.Id == id);
            return book;
        }

        public IList<Book> List()
        {
           return books;
        }

        public void Update(int id, Book newBook)
        {
            var book = Find(id);
            book.Title = newBook.Title;
            book.Author = newBook.Author;
            book.Description = newBook.Description;
            book.ImageURL = newBook.ImageURL;
        }
    }
}
