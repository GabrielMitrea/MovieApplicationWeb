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
    public class FilmsController : Controller
    {
        private readonly ApplicationRegisterModel _context;

        public FilmsController(ApplicationRegisterModel context)
        {
            _context = context;
        }

        // GET: Films
        public async Task<IActionResult> Index(string searchString)
        {
            var films = from m in _context.Filmss select m;
            films = films.Include(f => f.DivertismentType).Include(f => f.Genre);
            if (!String.IsNullOrEmpty(searchString))
            {
                films = films.Where(s => s.Title.Contains(searchString));

            }
            return View(await films.ToListAsync());
        }

        // GET: Films/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var film = await _context.Filmss
                .Include(f => f.DivertismentType)
                .Include(f => f.Genre)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (film == null)
            {
                return NotFound();
            }

            return View(film);
        }

        // GET: Films/Create
        public IActionResult Create()
        {
            ViewData["DivertismentTypeId"] = new SelectList(_context.DivertismentTypes, "Id", "DivertismentType");
            ViewData["GenreId"] = new SelectList(_context.Genres, "Id", "Genre");
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Username");
           
            return View();
        }

        // POST: Films/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,DivertismentTypeId,Title,GenreId,Duration,DateReleased,Director,Description,UserId,Trailer,ImagePath")] Film film,IFormFile image)
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
                    film.ImagePath = fileName;
                }
                _context.Add(film);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DivertismentTypeId"] = new SelectList(_context.DivertismentTypes, "Id", "Id", film.DivertismentTypeId);
            ViewData["GenreId"] = new SelectList(_context.Genres, "Id", "Id", film.GenreId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", film.UserId);
            return View(film);
        }

        // GET: Films/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var film = await _context.Filmss.FindAsync(id);
            if (film == null)
            {
                return NotFound();
            }
            ViewData["DivertismentTypeId"] = new SelectList(_context.DivertismentTypes, "Id", "DivertismentType", film.DivertismentTypeId);
            ViewData["GenreId"] = new SelectList(_context.Genres, "Id", "Genre", film.GenreId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Username", film.UserId);
            return View(film);
        }

        // POST: Films/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,DivertismentTypeId,Title,GenreId,Duration,DateReleased,Director,Description,UserId,Trailer,ImagePath")] Film film)
        {
            if (id != film.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(film);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FilmExists(film.Id))
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
            ViewData["DivertismentTypeId"] = new SelectList(_context.DivertismentTypes, "Id", "Id", film.DivertismentTypeId);
            ViewData["GenreId"] = new SelectList(_context.Genres, "Id", "Id", film.GenreId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", film.UserId);
            return View(film);
        }

        // GET: Films/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var film = await _context.Filmss
                .Include(f => f.DivertismentType)
                .Include(f => f.Genre)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (film == null)
            {
                return NotFound();
            }

            return View(film);
        }

        // POST: Films/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var film = await _context.Filmss.FindAsync(id);
            _context.Filmss.Remove(film);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FilmExists(int id)
        {
            return _context.Filmss.Any(e => e.Id == id);
        }
    }
}
