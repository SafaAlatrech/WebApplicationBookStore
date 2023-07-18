using static System.Reflection.Metadata.BlobBuilder;

namespace WebApplicationBookStore.Models.Repositories
{
    public class AuthorRepository : IBookStoreRepository<Author>
    { 
        IList<Author> authors;
        public AuthorRepository()
        {
            authors = new List<Author>()
            {

                new Author {Id=1,FullName="ALATRECH Safa"},
                new Author {Id=2, FullName="William Shakespeare"},
                new Author {Id=3,FullName="Barbara Cartland"},
            };
        }
        public void Add(Author entity)
        {
            entity.Id = authors.Max(b => b.Id) + 1;
            authors.Add(entity);
        }

        public void Delete(int id)
        {
            var author = Find(id);
            authors.Remove(author);
        }

        public Author Find(int id)
        {
           var author = authors.SingleOrDefault(b => b.Id == id);
            return author;
        }

        public IList<Author> List()
        {
            return authors;
        }

        public void Update(int id, Author newAuthor)
        {
            var author = Find(id);
            author.Id = newAuthor.Id;
            author.FullName = newAuthor.FullName;

        }
    }
}
