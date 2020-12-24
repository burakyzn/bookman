using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using BookstoreProject.Models;
using Microsoft.AspNetCore.Authorization;
using BookstoreProject.Data;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace BookstoreProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            List<Book> _kitaplar = await _context.Books.OrderByDescending(x => x.CreateDate).Take(10).ToListAsync();
            return View(_kitaplar);
        }

        [Authorize]
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public async Task<IActionResult>BookDetails(int searchId)
        {
            
            
            List<Book> kitaplar = await _context.Books.OrderBy(u => Guid.NewGuid()).Where(u=>u.Id!=searchId).Take(4).ToListAsync();
            
            Book _kitap = _context.Books.Where(x => x.Id == searchId).FirstOrDefault();
            kitaplar.Add(_kitap);
            if (_kitap == null)
                return RedirectToAction("ErrorPage", "Home", new { statusCode = 901 });
            else 
                return View(kitaplar);
           
               
           
        }
      
        public async Task<IActionResult> SearchBooks(string searchItem)
        {
            List<Book> kitaplar = await _context.Books.Where(x => x.Name_TR.ToLower().Contains(searchItem.ToLower())).ToListAsync();
            return View(kitaplar);
        }

        [Route("Home/ErrorPage/{statusCode}")]
        public IActionResult ErrorPage(int statusCode)
        {
            switch (statusCode)
            {
                case 901:
                    ViewBag.ErrorMessage = "Hata böyle bir kitap yok veya ulaşılamıyor.";
                    break;
                default:
                    ViewBag.ErrorMessage = "Hata böyle bir sayfa yok veya ulaşılamıyor.";
                    break;
            }
            return View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddBookToBasket(int bookId)
        {
            Basket _basket = new Basket
            {
                BookId = bookId,
                Active = true,
                UserId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier))
            };

            _context.Add(_basket);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index", "Baskets");
        }
    }
}
