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
    public class LanguagesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LanguagesController(ApplicationDbContext context)
        {
            _context = context;
        }

      
        public async Task<IActionResult> Index()
        {
            return View(await _context.Languages.ToListAsync());
        }

        
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var language = await _context.Languages
                .FirstOrDefaultAsync(m => m.Id == id);
            if (language == null)
            {
                return NotFound();
            }

            return View(language);
        }

       
        public IActionResult Create()
        {
            return View();
        }

      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name_TR,Name_EN,Active")] Language language)
        {
            if (ModelState.IsValid)
            {
                language.Active = true;
                _context.Add(language);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(language);
        }

       
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var language = await _context.Languages.FindAsync(id);
            if (language == null)
            {
                return NotFound();
            }
            return View(language);
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name_TR,Name_EN,Active")] Language language)
        {
            Language currentLanguage = _context.Languages.Where(x => x.Id == id).FirstOrDefault();
            if (id != language.Id || currentLanguage==null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    currentLanguage.Name_TR = language.Name_TR;
                    currentLanguage.Name_EN = language.Name_EN;
                    _context.Update(currentLanguage);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LanguageExists(language.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(currentLanguage);
        }

        
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var language = await _context.Languages
                .FirstOrDefaultAsync(m => m.Id == id);
            if (language == null)
            {
                return NotFound();
            }

            return View(language);
        }

      
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            Language language = _context.Languages.Where(x => x.Id == id).FirstOrDefault();

            if (language != null)
            {
                if(language.Active==true)
                {
                    language.Active = false;
                    _context.Update(language);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    language.Active = true;
                    _context.Update(language);
                    await _context.SaveChangesAsync();
                }
                
            }
            else
            {
                return NotFound();
            }

            return RedirectToAction(nameof(Index));
        }

        private bool LanguageExists(int id)
        {
            return _context.Languages.Any(e => e.Id == id);
        }
    }
}
