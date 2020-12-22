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
            List<Basket> _basket = await _context.Baskets.Where(x => x.UserId.ToString() == User.FindFirstValue(ClaimTypes.NameIdentifier) && x.Active == true).Include(b => b.Book).ToListAsync();

            ViewData["ToplamFiyat"] = _basket.Sum(x=> x.Book.Price);

            return View(_basket);
        }

        [Authorize]
        public async Task<IActionResult> removeFromBasket(int? bookId)
        {
            if (bookId == null)
            {
                return NotFound();
            }

            Basket basket = await _context.Baskets.Where(x => x.UserId.ToString() == User.FindFirstValue(ClaimTypes.NameIdentifier) && x.Active == true && x.Book.Id == bookId).Include(b => b.Book).FirstOrDefaultAsync();

            if (basket != null)
            {
                basket.Active = false;
                _context.Update(basket);
                await _context.SaveChangesAsync();
            }
            else
            {
                return NotFound();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
