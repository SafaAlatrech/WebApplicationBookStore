using Microsoft.EntityFrameworkCore;

namespace WebApplicationBookStore.Models
{
    public class BookStoreDbContext : DbContext
    {
        //public BookStoreDbContext(DbContextOptions<BookStoreDbContext> options) : base(options)
        //{

        //}

        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=.\\SQLEXPRESS; Database=test; Integrated security=true");
        }

    }
}
