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
    public class WatchlistsController : Controller
    {
        private readonly ApplicationRegisterModel _context;

        public WatchlistsController(ApplicationRegisterModel context)
        {
            _context = context;
        }

        // GET: Watchlists
        public async Task<IActionResult> Index(string searchString)
        {
            var watchlist = from m in _context.WatchLists select m;
            watchlist = watchlist.Include(f => f.DivertismentType).Include(f => f.Genre);
            if (!String.IsNullOrEmpty(searchString))
            {
                watchlist = watchlist.Where(s => s.Title.Contains(searchString));

            }
            return View(await watchlist.ToListAsync());
        }


        // GET: Watchlists/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var watchlist = await _context.WatchLists
                .Include(w => w.DivertismentType)
                .Include(w => w.Genre)
                .Include(w => w.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (watchlist == null)
            {
                return NotFound();
            }

            return View(watchlist);
        }

        // GET: Watchlists/Create
        public IActionResult Create()
        {
            ViewData["DivertismentTypeId"] = new SelectList(_context.DivertismentTypes, "Id", "DivertismentType");
            ViewData["GenreId"] = new SelectList(_context.Genres, "Id", "Genre");
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Username");
            return View();
        }

        // POST: Watchlists/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,DivertismentTypeId,Title,GenreId,Duration,DateReleased,Director,Description,UserId,Trailer,ImagePath")] Watchlist watchlist,IFormFile image)
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
                    watchlist.ImagePath = fileName;
                }
                _context.Add(watchlist);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DivertismentTypeId"] = new SelectList(_context.DivertismentTypes, "Id", "Id", watchlist.DivertismentTypeId);
            ViewData["GenreId"] = new SelectList(_context.Genres, "Id", "Id", watchlist.GenreId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", watchlist.UserId);
            return View(watchlist);
        }

        // GET: Watchlists/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var watchlist = await _context.WatchLists.FindAsync(id);
            if (watchlist == null)
            {
                return NotFound();
            }
            ViewData["DivertismentTypeId"] = new SelectList(_context.DivertismentTypes, "Id", "DivertismentType", watchlist.DivertismentTypeId);
            ViewData["GenreId"] = new SelectList(_context.Genres, "Id", "Genre", watchlist.GenreId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Username", watchlist.UserId);
            return View(watchlist);
        }

        // POST: Watchlists/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,DivertismentTypeId,Title,GenreId,Duration,DateReleased,Director,Description,UserId,Trailer,ImagePath")] Watchlist watchlist)
        {
            if (id != watchlist.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(watchlist);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WatchlistExists(watchlist.Id))
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
            ViewData["DivertismentTypeId"] = new SelectList(_context.DivertismentTypes, "Id", "Id", watchlist.DivertismentTypeId);
            ViewData["GenreId"] = new SelectList(_context.Genres, "Id", "Id", watchlist.GenreId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "EmailAddress", watchlist.UserId);
            return View(watchlist);
        }

        // GET: Watchlists/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var watchlist = await _context.WatchLists
                .Include(w => w.DivertismentType)
                .Include(w => w.Genre)
                .Include(w => w.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (watchlist == null)
            {
                return NotFound();
            }

            return View(watchlist);
        }

        // POST: Watchlists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var watchlist = await _context.WatchLists.FindAsync(id);
            _context.WatchLists.Remove(watchlist);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WatchlistExists(int id)
        {
            return _context.WatchLists.Any(e => e.Id == id);
        }
    }
}
