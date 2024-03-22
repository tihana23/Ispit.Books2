using Ispit.Books2.Data;
using Ispit.Books2.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Security.Policy;

namespace Ispit.Books2.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class ManageBooksController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<AspNetUser> _userManager;

        public ManageBooksController(ApplicationDbContext context, UserManager<AspNetUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public async Task<IActionResult> Index()
        {
            var books = await _context.Books.Include(b => b.Author)
                              .Include(b => b.Publisher)
                              .ToListAsync();

            return View(books);
            
        }

        public async Task<IActionResult> Details(int id)
        {

            var book = await _context.Books.FirstOrDefaultAsync(c => c.BookId == id);

            if (book == null)
            {
                return NotFound();

            }

            return View(book);
        }

        public async Task<IActionResult> Create()
        {
            var viewModel = new Book
            {
             
                Authors = _context.Authors.Select(a => new SelectListItem
                {
                    Value = a.AuthorId.ToString(),
                    Text = a.Name
                }).ToList(),
                Publishers = _context.Publishers.Select(p => new SelectListItem
                {
                    Value = p.PublisherId.ToString(),
                    Text = p.Name
                }).ToList(),
                Users = await GetAllUsers() // Assuming you have this method implemented
            };

            return View(viewModel);
        }

    
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( Book book)
        {
            if (ModelState.IsValid)
            {
                _context.Add(book);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));


            }
            return View(book);
    }
            public async Task<IActionResult> Edit(int id)
            {
            var book = await _context.Books.FindAsync(id);

            book.Users = await GetAllUsers();
            book.Publishers = await GetAllPublisher();
            book.Authors = await GetAllAuthors();
            if (book == null)
            {
                return NotFound();

            }
            return View(book);
        }
    
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Edit(int id, Book book)
        { 
            if (id != book.BookId)
            {
                return NotFound();

    }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(book);
                    await _context.SaveChangesAsync();
}
                catch (DbUpdateConcurrencyException ex)
                {
                    if (!_context.Books.Any(o=>o.BookId==id))
                    {

                        return NotFound();
                    }
                    else
{
    throw ex;
}
                }
                return RedirectToAction(nameof(Index));

            }
            return View(book);


        }
        public async Task<IActionResult> Delete(int id)
        {

            var order = await _context.Books.FirstOrDefaultAsync(c => c.BookId == id);
            if (order == null)
            {
                return NotFound();

            }

            return View(order);
        }

        [HttpPost, ActionName(
            "Delete")]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> DeleteConfirmed(int id)
        {

            var order = await _context.Books.FindAsync(id);
            _context.Books.Remove(order);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }

        private async Task<List<SelectListItem>> GetAllUsers()
        {

            return await _context.Users.Select(user =>
                 new SelectListItem
                 {
                     Value = user.Id.ToString(),
                     Text = user.FirstName + " " + user.LastName
                 }

                 ).ToListAsync();

        }
        private async Task<List<SelectListItem>> GetAllPublisher()
        {

            return await _context.Publishers.Select(user =>
                 new SelectListItem
                 {
                     Value = user.PublisherId.ToString(),
                     Text = user.Name
                 }

                 ).ToListAsync();

        }
        private async Task<List<SelectListItem>> GetAllAuthors()
        {

            return await _context.Authors.Select(user =>
                 new SelectListItem
                 {
                     Value = user.AuthorId.ToString(),
                     Text = user.Name
                 }

                 ).ToListAsync();

        }
    }
}
