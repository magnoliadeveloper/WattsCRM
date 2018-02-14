using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WattsCRM.Data;
using WattsCRM.Models;

namespace WattsCRM.Controllers
{
    [Authorize]
    public class ClientNoteController : Controller
    {
        private readonly ApplicationDbContext _context;
        private static Guid clientId;

        public ClientNoteController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ClientNote
        public async Task<IActionResult> Index(string id)
        {
            if (!string.IsNullOrWhiteSpace(id))
            {
                clientId = new Guid(id);
                ViewData["ClientId"] = clientId;                    
            }
            else
            {
                if (clientId == Guid.Empty || clientId == null)
                {
                    return NotFound();
                }

                ViewData["ClientId"] = clientId;
            }
            
            return View(await _context.ClientNoteViewModel.Where(a => a.ClientId == clientId).ToListAsync());
        }

        // GET: ClientNote/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clientNoteViewModel = await _context.ClientNoteViewModel
                .SingleOrDefaultAsync(m => m.ClientNoteId == id);
            if (clientNoteViewModel == null)
            {
                return NotFound();
            }

            return View(clientNoteViewModel);
        }

        // GET: ClientNote/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ClientNote/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ClientNoteId,ClientId,Note")] ClientNoteViewModel clientNoteViewModel)
        {
            if (ModelState.IsValid)
            {
                clientNoteViewModel.ClientNoteId = Guid.NewGuid();
                clientNoteViewModel.ClientId = clientId;
                clientNoteViewModel.Created = DateTime.UtcNow;
                _context.Add(clientNoteViewModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(clientNoteViewModel);
        }

        // GET: ClientNote/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clientNoteViewModel = await _context.ClientNoteViewModel.SingleOrDefaultAsync(m => m.ClientNoteId == id);
            if (clientNoteViewModel == null)
            {
                return NotFound();
            }
            return View(clientNoteViewModel);
        }

        // POST: ClientNote/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("ClientNoteId,ClientId,Note")] ClientNoteViewModel clientNoteViewModel)
        {
            if (id != clientNoteViewModel.ClientNoteId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(clientNoteViewModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClientNoteViewModelExists(clientNoteViewModel.ClientNoteId))
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
            return View(clientNoteViewModel);
        }

        // GET: ClientNote/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clientNoteViewModel = await _context.ClientNoteViewModel
                .SingleOrDefaultAsync(m => m.ClientNoteId == id);
            if (clientNoteViewModel == null)
            {
                return NotFound();
            }

            return View(clientNoteViewModel);
        }

        // POST: ClientNote/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var clientNoteViewModel = await _context.ClientNoteViewModel.SingleOrDefaultAsync(m => m.ClientNoteId == id);
            _context.ClientNoteViewModel.Remove(clientNoteViewModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClientNoteViewModelExists(Guid id)
        {
            return _context.ClientNoteViewModel.Any(e => e.ClientNoteId == id);
        }
    }
}
