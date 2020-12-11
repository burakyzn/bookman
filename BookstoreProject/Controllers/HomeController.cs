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

        public IActionResult Index()
        {
            return View();
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
    }
}
