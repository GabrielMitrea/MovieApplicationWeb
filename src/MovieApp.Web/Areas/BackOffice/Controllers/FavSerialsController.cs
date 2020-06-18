using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MovieApp.Web.Models;
using MovieApp.Web.Areas.BackOffice.Models;

namespace MovieApp.Web.Areas.BackOffice.Controllers
{
    public class FavSerialsController : Controller
    {
        private readonly ApplicationRegisterModel _context;

        public FavSerialsController(ApplicationRegisterModel context)
        {
            _context = context;
        }

        // GET: FavSerials
        public async Task<IActionResult> Index(string searchString)
        {
            var favserials = from m in _context.FavSerials select m;
            favserials = favserials.Include(f => f.DivertismentType).Include(f => f.Genre);
            if (!String.IsNullOrEmpty(searchString))
            {
                favserials = favserials.Where(s => s.Title.Contains(searchString));

            }
            return View(await favserials.ToListAsync());
        }

        // GET: FavSerials/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var favSerial = await _context.FavSerials
                .Include(f => f.DivertismentType)
                .Include(f => f.Genre)
                .Include(f => f.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (favSerial == null)
            {
                return NotFound();
            }

            return View(favSerial);
        }

        // GET: FavSerials/Create
        public IActionResult Create()
        {
            ViewData["DivertismentTypeId"] = new SelectList(_context.DivertismentTypes, "Id", "DivertismentType");
            ViewData["GenreId"] = new SelectList(_context.Genres, "Id", "Genre");
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Username");
            return View();
        }

        // POST: FavSerials/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,DivertismentTypeId,Title,GenreId,NumberOfSeasons,NumberOfEpisodes,DateReleased,Director,Description,UserId,Trailer,ImagePath")] FavSerial favSerial)
        {
            if (ModelState.IsValid)
            {
                _context.Add(favSerial);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DivertismentTypeId"] = new SelectList(_context.DivertismentTypes, "Id", "Id", favSerial.DivertismentTypeId);
            ViewData["GenreId"] = new SelectList(_context.Genres, "Id", "Id", favSerial.GenreId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", favSerial.UserId);
            return View(favSerial);
        }

        // GET: FavSerials/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var favSerial = await _context.FavSerials.FindAsync(id);
            if (favSerial == null)
            {
                return NotFound();
            }
            ViewData["DivertismentTypeId"] = new SelectList(_context.DivertismentTypes, "Id", "DivertismentType", favSerial.DivertismentTypeId);
            ViewData["GenreId"] = new SelectList(_context.Genres, "Id", "Genre", favSerial.GenreId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Username", favSerial.UserId);
            return View(favSerial);
        }

        // POST: FavSerials/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,DivertismentTypeId,Title,GenreId,NumberOfSeasons,NumberOfEpisodes,DateReleased,Director,Description,UserId,Trailer,ImagePath")] FavSerial favSerial)
        {
            if (id != favSerial.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(favSerial);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FavSerialExists(favSerial.Id))
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
            ViewData["DivertismentTypeId"] = new SelectList(_context.DivertismentTypes, "Id", "Id", favSerial.DivertismentTypeId);
            ViewData["GenreId"] = new SelectList(_context.Genres, "Id", "Id", favSerial.GenreId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", favSerial.UserId);
            return View(favSerial);
        }

        // GET: FavSerials/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var favSerial = await _context.FavSerials
                .Include(f => f.DivertismentType)
                .Include(f => f.Genre)
                .Include(f => f.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (favSerial == null)
            {
                return NotFound();
            }

            return View(favSerial);
        }

        // POST: FavSerials/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var favSerial = await _context.FavSerials.FindAsync(id);
            _context.FavSerials.Remove(favSerial);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FavSerialExists(int id)
        {
            return _context.FavSerials.Any(e => e.Id == id);
        }
    }
}
