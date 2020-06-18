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
    public class FavFilmsController : Controller
    {
        private readonly ApplicationRegisterModel _context;

        public FavFilmsController(ApplicationRegisterModel context)
        {
            _context = context;
        }

        // GET: FavFilms
        public async Task<IActionResult> Index(string searchString)
        {
            var favfilms = from m in _context.FavFilms select m;
            favfilms = favfilms.Include(f => f.DivertismentType).Include(f => f.Genre);
            if (!String.IsNullOrEmpty(searchString))
            {
                favfilms = favfilms.Where(s => s.Title.Contains(searchString));

            }
            return View(await favfilms.ToListAsync());
        }

        // GET: FavFilms/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var favFilm = await _context.FavFilms
                .Include(f => f.DivertismentType)
                .Include(f => f.Genre)
                .Include(f => f.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (favFilm == null)
            {
                return NotFound();
            }

            return View(favFilm);
        }

        // GET: FavFilms/Create
        public IActionResult Create()
        {
            ViewData["DivertismentTypeId"] = new SelectList(_context.DivertismentTypes, "Id", "DivertismentType");
            ViewData["GenreId"] = new SelectList(_context.Genres, "Id", "Genre");
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Username");
            return View();
        }

        // POST: FavFilms/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,DivertismentTypeId,Title,GenreId,Duration,DateReleased,Director,Description,UserId,Trailer,ImagePath")] FavFilm favFilm, IFormFile image )
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
                    favFilm.ImagePath = fileName;
                }
                _context.Add(favFilm);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DivertismentTypeId"] = new SelectList(_context.DivertismentTypes, "Id", "Id", favFilm.DivertismentTypeId);
            ViewData["GenreId"] = new SelectList(_context.Genres, "Id", "Id", favFilm.GenreId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", favFilm.UserId);
            return View(favFilm);
        }

        // GET: FavFilms/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var favFilm = await _context.FavFilms.FindAsync(id);
            if (favFilm == null)
            {
                return NotFound();
            }
            ViewData["DivertismentTypeId"] = new SelectList(_context.DivertismentTypes, "Id", "DivertismentType", favFilm.DivertismentTypeId);
            ViewData["GenreId"] = new SelectList(_context.Genres, "Id", "Genre", favFilm.GenreId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Username", favFilm.UserId);
            return View(favFilm);
        }

        // POST: FavFilms/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,DivertismentTypeId,Title,GenreId,Duration,DateReleased,Director,Description,UserId,Trailer,ImagePath")] FavFilm favFilm)
        {
            if (id != favFilm.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(favFilm);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FavFilmExists(favFilm.Id))
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
            ViewData["DivertismentTypeId"] = new SelectList(_context.DivertismentTypes, "Id", "Id", favFilm.DivertismentTypeId);
            ViewData["GenreId"] = new SelectList(_context.Genres, "Id", "Id", favFilm.GenreId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", favFilm.UserId);
            return View(favFilm);
        }

        // GET: FavFilms/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var favFilm = await _context.FavFilms
                .Include(f => f.DivertismentType)
                .Include(f => f.Genre)
                .Include(f => f.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (favFilm == null)
            {
                return NotFound();
            }

            return View(favFilm);
        }

        // POST: FavFilms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var favFilm = await _context.FavFilms.FindAsync(id);
            _context.FavFilms.Remove(favFilm);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FavFilmExists(int id)
        {
            return _context.FavFilms.Any(e => e.Id == id);
        }
    }
}
