using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MovieApp.Web.Areas.BackOffice.Models;
using MovieApp.Web.Models;

namespace MovieApp.Web.Areas.BackOffice.Controllers
{
    
    public class ComingsoonController : Controller
    {
        private readonly ApplicationRegisterModel _context;

        public ComingsoonController(ApplicationRegisterModel context)
        {
            _context = context;
        }

        // GET: BackOffice/ComingSoon
        

        public async Task<IActionResult> Index(string searchString)
        {
            var comingSoon = from m in _context.ComingSoon select m;
            comingSoon = comingSoon.Include(f => f.DivertismentType).Include(f => f.Genre);
            if (!String.IsNullOrEmpty(searchString))
            {
                comingSoon = comingSoon.Where(s => s.Title.Contains(searchString));

            }
            return View(await comingSoon.ToListAsync());
        }
        // GET: BackOffice/ComingSoon/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comingSoon = await _context.ComingSoon
                .Include(w => w.DivertismentType)
                .Include(w => w.Genre)
                .Include(w => w.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (comingSoon == null)
            {
                return NotFound();
            }

            return View(comingSoon);
        }

        // GET: BackOffice/WatchLists/Create
        public IActionResult Create()
        {
            ViewData["DivertismentTypeId"] = new SelectList(_context.DivertismentTypes, "Id", "DivertismentType");
            ViewData["GenreId"] = new SelectList(_context.Genres, "Id", "Genre");
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Username");
            return View();
        }

        // POST: BackOffice/WatchLists/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,DivertismentTypeId,Title,GenreId,UserId,Description,Trailer,ImagePath")] ComingSoon comingSoon,IFormFile image)
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
                    comingSoon.ImagePath = fileName;
                }
                _context.Add(comingSoon);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DivertismentTypeId"] = new SelectList(_context.DivertismentTypes, "Id", "Id", comingSoon.DivertismentTypeId);
            ViewData["GenreId"] = new SelectList(_context.Genres, "Id", "Id", comingSoon.GenreId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", comingSoon.UserId);
            return View(comingSoon);
        }

        // GET: BackOffice/WatchLists/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comingSoon = await _context.ComingSoon.FindAsync(id);
            if (comingSoon == null)
            {
                return NotFound();
            }
            ViewData["DivertismentTypeId"] = new SelectList(_context.DivertismentTypes, "Id", "DivertismentType", comingSoon.DivertismentTypeId);
            ViewData["GenreId"] = new SelectList(_context.Genres, "Id", "Genre", comingSoon.GenreId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Username", comingSoon.UserId);
            return View(comingSoon);
        }

        // POST: BackOffice/WatchLists/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,DivertismentTypeId,Title,GenreId,UserId,Description,Trailer,ImagePath")] ComingSoon comingSoon)
        {
            if (id != comingSoon.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(comingSoon);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WatchListExists(comingSoon.Id))
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
            ViewData["DivertismentTypeId"] = new SelectList(_context.DivertismentTypes, "Id", "DivertismentType", comingSoon.DivertismentTypeId);
            ViewData["GenreId"] = new SelectList(_context.Genres, "Id", "Genre", comingSoon.GenreId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Username", comingSoon.UserId);
            return View(comingSoon);
        }

        // GET: BackOffice/WatchLists/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comingSoon = await _context.ComingSoon
                .Include(w => w.DivertismentType)
                .Include(w => w.Genre)
                .Include(w => w.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (comingSoon == null)
            {
                return NotFound();
            }

            return View(comingSoon);
        }

        // POST: BackOffice/WatchLists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var watchList = await _context.ComingSoon.FindAsync(id);
            _context.ComingSoon.Remove(watchList);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WatchListExists(int id)
        {
            return _context.ComingSoon.Any(e => e.Id == id);
        }
    }
}
