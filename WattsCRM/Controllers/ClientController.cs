using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WattsCRM.Data;
using WattsCRM.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using WattsCRM.Interfaces;
using WattsCRM.Business;

namespace WattsCRM.Controllers
{
    [Authorize]
    public class ClientController : Controller
    {
        private readonly IApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private ClientViewModel _clientViewModel;
        private HttpContext _httpContext;

        public IClientRepository ClientRepository
        {
            get
            {
                return clientRepository ?? new ClientRepository();
            }
        }

        IClientRepository clientRepository;

        public Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);

        public ClientController(UserManager<ApplicationUser> userManager, IApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;

            clientRepository = new ClientRepository(context);
        }

        // GET: Client
        public async Task<IActionResult> Index()
        {            
            
            ApplicationUser usr = await GetCurrentUserAsync();
            return View(await ClientRepository.GetClients(usr.Id));
        }

        // GET: Client/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clientViewModel = await ClientRepository.GetClientById(id);
            if (clientViewModel == null)
            {
                return NotFound();
            }

            _clientViewModel = clientViewModel;

            return View(clientViewModel);
        }

        // GET: Client/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Client/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ClientId,FirstName,LastName,CompanyName,EmailAddress,Address,City,State,Zip")] ClientViewModel clientViewModel)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser usr = await GetCurrentUserAsync();
                //clientViewModel.OwnerId = usr.Id;
                //clientViewModel.ClientId = Guid.NewGuid();
                //clientViewModel.Changed = DateTime.UtcNow;
                //_context.Add(clientViewModel);
                //await _context.SaveChangesAsync();
                ClientRepository.AddClient(clientViewModel, usr.Id);
                await ClientRepository.SaveClient();
                return RedirectToAction(nameof(Index));
            }
            return View(clientViewModel);
        }

        // GET: Client/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)

            {
                return NotFound();
            }

            var clientViewModel = await ClientRepository.GetClientById(id);
            if (clientViewModel == null)
            {
                return NotFound();
            }
            return View(clientViewModel);
        }

        // POST: Client/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("ClientId,FirstName,LastName,CompanyName,EmailAddress,Address,City,State,Zip")] ClientViewModel clientViewModel)
        {
            if (id != clientViewModel.ClientId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    ApplicationUser usr = await GetCurrentUserAsync();
                    ClientRepository.UpdateClient(clientViewModel, usr.Id);
                    await ClientRepository.SaveClient();
                    //clientViewModel.OwnerId = usr.Id;
                    //clientViewModel.Changed = DateTime.UtcNow;
                    //_context.Update(clientViewModel);                    
                    //await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClientViewModelExists(clientViewModel.ClientId))
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
            return View(clientViewModel);
        }

        // GET: Client/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {            
            if (id == null)
            {
                return NotFound();
            }

            var clientViewModel = await ClientRepository.GetClientById(id);
                //_context.ClientViewModel
                //.SingleOrDefaultAsync(m => m.ClientId == id);

            _clientViewModel = clientViewModel;

            if (clientViewModel == null)
            {
                return NotFound();
            }

            return View(clientViewModel);
        }

        // POST: Client/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var notes = await _context.ClientNoteViewModel.CountAsync(a => a.ClientId == id);

            // Before deleting the client, check to see if there are any notes.
            if (notes == 0)
            {
                var clientViewModel = await ClientRepository.GetClientById(id);
                ClientRepository.DeleteClient(clientViewModel);
                await ClientRepository.SaveClient();
                //_context.ClientViewModel.Remove(clientViewModel);
                //await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            else
            {
                //ViewData["Error"] = "true";
                return Forbid();
            }
        }

        private bool ClientViewModelExists(Guid id)
        {
            return ClientRepository.ClientExists(id);
        }
    }
}
