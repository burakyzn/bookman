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
            List<Kitap> _kitaplar = await _context.Kitaplar.OrderBy(x => x.Stok).Take(10).ToListAsync();
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

        public IActionResult BookDetails(int searchId)
        {
            Kitap _kitap = _context.Kitaplar.Where(x => x.Id == searchId).FirstOrDefault();
            return _kitap == null ? View("ErrorPage") : View(_kitap);
        } 

        public async Task<IActionResult> SearchBooks(string searchItem)
        {
            List<Kitap> kitaplar = await _context.Kitaplar.Where(x => x.Ad_TR.ToLower().Contains(searchItem.ToLower())).ToListAsync();
            return View(kitaplar);
        }
    }
}
