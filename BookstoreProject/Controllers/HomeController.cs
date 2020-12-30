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
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Http;

namespace BookstoreProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<UserDetails> _userManager;

        public HomeController(ApplicationDbContext context, UserManager<UserDetails> userManager)
        {
            _context = context;
            _userManager = userManager;
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

        public async Task<IActionResult> BookDetails(int searchId)
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
            var basket = await _context.Baskets
                .Where(x => x.UserDetailsId == User.FindFirstValue(ClaimTypes.NameIdentifier) && x.Active == true)
                .FirstOrDefaultAsync();

            Book currentBook = await _context.Books
                .Where(x => x.Id == bookId && x.Active == true)
                .FirstOrDefaultAsync();

            if (currentBook == null) return NotFound();

            if(basket != null)
            {
                BasketItem newBasketItem = new BasketItem
                {
                    BasketId = basket.Id,
                    Book = currentBook,
                    Active = true
                };

                _context.Add(newBasketItem);
                await _context.SaveChangesAsync();
            } else
            {
           
                Basket newBasket = new Basket
                {
                    Status = "YENI",
                    Active = true,
                    UserDetails = await _userManager.GetUserAsync(User)
                };

                _context.Add(newBasket);
                _context.SaveChanges();

                BasketItem newBasketItem = new BasketItem
                {
                    BasketId = newBasket.Id,
                    Book = currentBook,
                    Active = true
                };

                _context.Add(newBasketItem);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Index", "Baskets");
        }

        [Authorize]
        public async Task<IActionResult> MyOrder()
        {
            List<Basket> baskets = await _context.Baskets
                .Where(x => x.UserDetailsId == User.FindFirstValue(ClaimTypes.NameIdentifier) && x.Active == false)
                .ToListAsync();

            return View(baskets);
        }

        [Authorize]
        public async Task<IActionResult> CancelOrder(int? orderId)
        {
            if(orderId == null)
            {
                return NotFound();
            }

            Basket basket = await _context.Baskets
                .Where(x => x.Id == orderId)
                .FirstOrDefaultAsync();

            if(basket == null)
            {
                TempData["HataMesaj"] = "Böyle bir sipariş bulunamadı.";
            } else
            {
                basket.Status = "IPTAL";
                basket.Active = false;
                _context.Update(basket);
                await _context.SaveChangesAsync();
            }

            List<Basket> basketList = await _context.Baskets
                .Where(x => x.UserDetailsId == User.FindFirstValue(ClaimTypes.NameIdentifier) && x.Active == false)
                .ToListAsync();

            return View("MyOrder", basketList);
        }

        [Authorize]
        public async Task<IActionResult> OrderDetails(int? orderId)
        {
            if(orderId == null)
            {
                return NotFound();
            }

            List<BasketItem> basketItems = await _context.BasketItems
                .Where(b => b.BasketId == orderId && b.Active == true)
                .Include(b => b.Book)
                .Include(b => b.Book.Category)
                .Include(b => b.Book.Author)
                .ToListAsync();

            return View(basketItems);
        }

        [HttpPost]
        public IActionResult LangSetting(string culture)
        {
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions {Expires = DateTimeOffset.Now.AddDays(10)}
                );
            return RedirectToAction(nameof(Index));
        }
    }
}
