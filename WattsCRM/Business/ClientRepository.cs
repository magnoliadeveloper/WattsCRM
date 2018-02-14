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
using WattsCRM.Interfaces;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace WattsCRM.Business
{
    public class ClientRepository : IClientRepository
    {

        private readonly IApplicationDbContext _context;

        public ClientRepository()
        {
        }
        public ClientRepository(IApplicationDbContext context)
        {
            _context = context;
        }

        public Task<List<ClientViewModel>> GetClients(string id)
        {
            return _context.ClientViewModel.Where(x => x.OwnerId == id).ToListAsync();
        }

        public Task<ClientViewModel> GetClientById(Guid? id)
        {
            return _context.ClientViewModel.SingleOrDefaultAsync(m => m.ClientId == id);
        }

        public EntityEntry<ClientViewModel> AddClient(ClientViewModel clientViewModel, string id)
        {
            clientViewModel.OwnerId = id;
            clientViewModel.ClientId = Guid.NewGuid();
            clientViewModel.Changed = DateTime.UtcNow;
            return _context.AddClient(clientViewModel);
        }

        public void UpdateClient(ClientViewModel clientViewModel, string id)
        {
            clientViewModel.OwnerId = id;
            clientViewModel.Changed = DateTime.UtcNow;
            _context.UpdateClientModel(clientViewModel);
        }

        public void DeleteClient(ClientViewModel clientViewModel)
        {
            _context.RemoveClient(clientViewModel);
        }

        public async Task<int> SaveClient()
        {
            return await _context.Save();
        }

        public bool ClientExists(Guid id)
        {
            return _context.ClientViewModel.Any(e => e.ClientId == id);
        }
    }
}
