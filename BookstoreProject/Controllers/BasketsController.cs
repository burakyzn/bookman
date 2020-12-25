using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BookstoreProject.Data;
using BookstoreProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookstoreProject.Controllers
{
    public class BasketsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<UserDetails> _userManager;

        public BasketsController(ApplicationDbContext context, UserManager<UserDetails> userManager)
        {
            _context = context;
            _userManager = userManager;
        }


        [Authorize]
        public async Task<IActionResult> Index()
        {
            Basket basket = _context.Baskets
                .Where(x => x.UserId.ToString() == User.FindFirstValue(ClaimTypes.NameIdentifier) && x.Active == true)
                .FirstOrDefault();

            if(basket == null)
            {
                Basket newBasket = new Basket
                {
                    Durum = "YENI",
                    Active = true,
                    UserId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier))
                };

                _context.Add(newBasket);
                _context.SaveChanges();
            } else
            {
                List<BasketItem> basketItems = await _context.BasketItems
                        .Include(x => x.Basket)
                        .Where(x => x.Basket.UserId.ToString() == User.FindFirstValue(ClaimTypes.NameIdentifier) && x.Basket.Active == true && x.Active == true)
                        .Include(x => x.Book).ToListAsync();

                if (basketItems.Count != 0)
                {
                    ViewData["ToplamFiyat"] = basketItems.Sum(x => x.Book.Price);
                    ViewData["BasketID"] = basketItems[0].BasketId;
                    return View(basketItems);
                }
            }

            return View();
        }

        [Authorize]
        public async Task<IActionResult> removeFromBasket(int? bookId)
        {
            if (bookId == null)
            {
                return NotFound();
            }

            Basket basket = _context.Baskets
                .Where(x => x.UserId.ToString() == User.FindFirstValue(ClaimTypes.NameIdentifier) && x.Active == true)
                .FirstOrDefault();

            if(basket != null)
            {
                BasketItem basketItem = _context.BasketItems
                    .Where(x => x.BasketId == basket.Id && x.Active == true && x.BookId == bookId).FirstOrDefault();

                if(basketItem != null)
                {
                    basketItem.Active = false;
                    _context.Update(basketItem);
                    await _context.SaveChangesAsync();
                }
            } else
            {
                return NotFound();
            }

            return RedirectToAction(nameof(Index));
        }

        [Authorize]
        public async Task<IActionResult> BuyBooksAsync(int? basketId)
        {
            Basket basket = await _context.Baskets
                .Where(x => x.Id == basketId)
                .FirstOrDefaultAsync();

            if(basket != null)
            {
                List<BasketItem> basketItem = await _context.BasketItems
                    .Where(x => x.BasketId == basketId && x.Active == true)
                    .ToListAsync();

                for(int i = 1; i < basketItem.Count; i++)
                {
                    // stok dusmuyor
                    basketItem[i].Book.Stock -= 1;
                    _context.Update(basketItem);
                }

                basket.Active = false;
                basket.Durum = "KARGO";
                _context.Update(basket);
                await _context.SaveChangesAsync();
            }

            TempData["SiparisMesaj"] = "Siparisiniz Alindi. Siparis Numaraniz : " + basketId;
            return RedirectToAction("Index", "Baskets");
        }
    }
}
