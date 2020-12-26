using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BookstoreProject.Data;
using BookstoreProject.Models;
using Microsoft.AspNetCore.Authorization;

namespace BookstoreProject.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {

        private readonly ApplicationDbContext _context;

        public AdminController(ApplicationDbContext context)
        {
            _context = context;
        }

        
        public IActionResult Index()
        {
            
            return View();
        }
        public async Task<IActionResult> OrderList()
        {
            List<UserDetails> users = _context.Users.ToList();
            var applicationDbContext = _context.Baskets.Where(b => b.Active == false&& b.Durum=="KARGO");
            
            return View(await applicationDbContext.ToListAsync());
            
        }
        public async Task<IActionResult> BasketDetails(int? id)
        {
            var applicationDbContext = _context.BasketItems.Where(b => b.BasketId==id).Include(b=>b.Book);
            return View(await applicationDbContext.ToListAsync());
        }


        public async Task<IActionResult> ChangeSituation(int id)
        {
            Basket basket = _context.Baskets.Where(x => x.Id == id).FirstOrDefault();

            if (basket != null)
            {
                basket.Durum = "TAMAMLANDI";
                _context.Update(basket);
                await _context.SaveChangesAsync();
               
               
            }
            else
            {
                return NotFound();
            }

            return RedirectToAction(nameof(OrderList));
        }


    }
}
