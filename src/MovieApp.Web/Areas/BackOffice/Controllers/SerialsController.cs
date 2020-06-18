using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MovieApp.Web.Models;
using MovieApp.Web.Areas.BackOffice.Models;

namespace MovieApp.Web.Areas.BackOffice.Controllers
{
    public class SerialsController : Controller
    {
        private readonly ApplicationRegisterModel _context;

        public SerialsController(ApplicationRegisterModel context)
        {
            _context = context;
        }

        // GET: Serials
      
        public async Task<IActionResult> Index(string searchString)
        {
            var serials = from m in _context.Serialss select m;
            serials = serials.Include(f => f.DivertismentType).Include(f => f.Genre);
            if (!String.IsNullOrEmpty(searchString))
            {
                serials = serials.Where(s => s.Title.Contains(searchString)) ;

            }
            return View(await serials.ToListAsync());
        }
        // GET: Serials/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var serials = await _context.Serialss
                .Include(s => s.DivertismentType)
                .Include(s => s.Genre)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (serials == null)
            {
                return NotFound();
            }

            return View(serials);
        }

        // GET: Serials/Create
        public IActionResult Create()
        {
            ViewData["DivertismentTypeId"] = new SelectList(_context.DivertismentTypes, "Id", "DivertismentType");
            ViewData["GenreId"] = new SelectList(_context.Genres, "Id", "Genre");
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Username");
            return View();
        }

        // POST: Serials/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,DivertismentTypeId,Title,GenreId,NumberOfSeasons,NumberOfEpisodes,DateReleased,Director,Description,UserId,Trailer,ImagePath")] Serials serials, IFormFile image)
        {
            if (ModelState.IsValid)
            {
                if (image != null && image.Length > 0)
                {
                    var fileName = Path.GetFileName(image.FileName);
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images\\items", fileName);
                    using (var fileSteam = new FileStream(filePath, FileMode.Create))
                    {
                        await image.CopyToAsync(fileSteam);
                    }
                    serials.ImagePath = fileName;
                }
                _context.Add(serials);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DivertismentTypeId"] = new SelectList(_context.DivertismentTypes, "Id", "Id", serials.DivertismentTypeId);
            ViewData["GenreId"] = new SelectList(_context.Genres, "Id", "Id", serials.GenreId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id",serials.UserId);
            return View(serials);
        }

        // GET: Serials/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var serials = await _context.Serialss.FindAsync(id);
            if (serials == null)
            {
                return NotFound();
            }
            ViewData["DivertismentTypeId"] = new SelectList(_context.DivertismentTypes, "Id", "Id", serials.DivertismentTypeId);
            ViewData["GenreId"] = new SelectList(_context.Genres, "Id", "Id", serials.GenreId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id",serials.UserId);
            return View(serials);
        }

        // POST: Serials/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,DivertismentTypeId,Title,GenreId,NumberOfSeasons,NumberOfEpisodes,DateReleased,Director,Description,UserId,Trailer,ImagePath")] Serials serials)
        {
            if (id != serials.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(serials);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SerialsExists(serials.Id))
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
            ViewData["DivertismentTypeId"] = new SelectList(_context.DivertismentTypes, "Id", "Id", serials.DivertismentTypeId);
            ViewData["GenreId"] = new SelectList(_context.Genres, "Id", "Id", serials.GenreId);
            ViewData["UserId"] = new SelectList(_context.Genres, "Id", "Id", serials.UserId);
            return View(serials);
        }

        // GET: Serials/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var serials = await _context.Serialss
                .Include(s => s.DivertismentType)
                .Include(s => s.Genre)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (serials == null)
            {
                return NotFound();
            }

            return View(serials);
        }

        // POST: Serials/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var serials = await _context.Serialss.FindAsync(id);
            _context.Serialss.Remove(serials);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SerialsExists(int id)
        {
            return _context.Serialss.Any(e => e.Id == id);
        }
    }
}
