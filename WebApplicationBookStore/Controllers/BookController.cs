using Microsoft.AspNetCore.Mvc;
using WebApplicationBookStore.Models;
using WebApplicationBookStore.Models.Repositories;
using WebApplicationBookStore.ViewModels;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace WebApplicationBookStore.Controllers
{
    public class BookController : Controller
    {
        private readonly IBookStoreRepository<Book>? bookRepository;
        private readonly IBookStoreRepository<Author>? authorRepository;
        [Obsolete]
        private readonly IHostingEnvironment hosting;

        [Obsolete]
        public BookController(IBookStoreRepository<Book> bookRepository, IBookStoreRepository<Author> authorRepository,
            Microsoft.AspNetCore.Hosting.IHostingEnvironment hosting)
        {
            this.bookRepository = bookRepository;
            this.authorRepository = authorRepository;
            this.hosting = hosting;
        }
        // GET: BookController
        public ActionResult Index()
        {
            var books = bookRepository?.List();
            return View(books);
        }

        // GET: BookController/Details/5
        public ActionResult Details(int id)
        {
            var books = bookRepository?.Find(id);
            return View(books);
        }

        // GET: BookController/Create
        public ActionResult Create()
        {
            var model = new BookAuthorViewModel
            {
                Authors = FillSelectList()
            };
            return View(model);
        }

        // POST: BookController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Obsolete]
        public ActionResult Create(BookAuthorViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    string fileName = string.Empty;
                   
                    if (model.File != null)
                    {
                        string uploads = Path.Combine(hosting.WebRootPath, "uploads");
                        fileName = model.File.FileName;
                        string fullPath = Path.Combine(uploads, fileName);
                        model.File.CopyTo(new FileStream (fullPath,FileMode.Create));
                    }

                    if (model.AuthorId == -1)
                    {
                        ViewBag.Message = "Please select an another from the list !";
                        return View(GetAllAuthors());
                    }
                    var author = authorRepository?.Find(model.AuthorId);
                    Book book = new Book
                    {
                        Id = model.BookId,
                        Title = model.Title,
                        Description = model.Description,
                        Author = author,
                        ImageURL = fileName
                    };
                    bookRepository?.Add(book);
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    return View();
                }
            }
            ModelState.AddModelError("", "You have to fill all the required fields");
            return View(GetAllAuthors());
        }

        // GET: BookController/Edit/5
        public ActionResult Edit(int id)
        {
            var book = bookRepository?.Find(id);
            var authorId = book.Author == null ? book.Author.Id = 0 : book.Author.Id;
            var viewModel = new BookAuthorViewModel
            {
                BookId = book.Id,
                Title = book.Title,
                Description = book.Description,
                AuthorId = authorId,
                Authors = authorRepository?.List().ToList(),
            };
            return View(viewModel);
        }

        // POST: BookController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(BookAuthorViewModel viewModel)
        {
            try
            {
                var author = authorRepository?.Find(viewModel.AuthorId);
                Book book = new Book
                {
                    Title = viewModel.Title,
                    Description = viewModel.Description,
                    Author = author,
                };
                bookRepository?.Update(viewModel.BookId, book);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: BookController/Delete/5
        public ActionResult Delete(int id)
        {
            var book = bookRepository?.Find(id);
            return View();
        }

        // POST: BookController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ConfirmDelete(int id)
        {
            try
            {
                bookRepository?.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        List<Author> FillSelectList()
        {
            var authors = authorRepository.List().ToList();
            authors.Insert(0, new Author { Id = -1, FullName = "---Please Select an authors---" });
            return authors;

        }

        BookAuthorViewModel GetAllAuthors()
        {
            var Vmodel = new BookAuthorViewModel
            {
                Authors = FillSelectList()
            };

            return Vmodel;
        }
    }
}
