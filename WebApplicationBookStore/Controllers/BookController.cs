using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using WebApplicationBookStore.Models;
using WebApplicationBookStore.Models.Repositories;
using WebApplicationBookStore.ViewModels;

namespace WebApplicationBookStore.Controllers
{
    public class BookController : Controller
    {
        private readonly IBookStoreRepository<Book>? bookRepository;
        private readonly IBookStoreRepository<Author>? authorRepository;


        public BookController(IBookStoreRepository<Book> bookRepository, IBookStoreRepository<Author> authorRepository)
        {
            this.bookRepository = bookRepository;
            this.authorRepository = authorRepository;
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
                Authors = authorRepository?.List().ToList(),
            };
            return View(model);
        }

        // POST: BookController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BookAuthorViewModel model)
        {
            try
            {
                var author = authorRepository?.Find(model.AuthorId);
                Book book = new Book
                {
                    Id = model.BookId,
                    Title = model.Title,
                    Description = model.Description,
                    Author = author
                };
                bookRepository?.Add(book);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: BookController/Edit/5
        public ActionResult Edit(int id)
        {
            var book = bookRepository?.Find(id);
            var authorId = book.Author == null ? book.Author.Id = 0 :book.Author.Id;
            var viewModel = new BookAuthorViewModel
            {            
                BookId = book.Id,
                Title= book.Title,
                Description= book.Description,
                AuthorId = authorId,
                Authors = authorRepository?.List().ToList(),    
            };
            return View(viewModel);
        }

        // POST: BookController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( BookAuthorViewModel viewModel)
        {
            try
            {
                var author = authorRepository?.Find(viewModel.AuthorId);
                Book book = new Book
                {
                    Title = viewModel.Title,
                    Description = viewModel.Description,
                    Author= author, 
                }; 
                bookRepository?.Update(viewModel.BookId ,book);
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
            return View();
        }

        // POST: BookController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
