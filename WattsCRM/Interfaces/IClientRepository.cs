using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WattsCRM.Models;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace WattsCRM.Interfaces
{
    public interface IClientRepository
    {
        Task<List<ClientViewModel>> GetClients(string id);

        Task<ClientViewModel> GetClientById(Guid? id);

        EntityEntry<ClientViewModel> AddClient(ClientViewModel clientViewModel, string id);

        Task<int> SaveClient();

        void UpdateClient(ClientViewModel clientViewModel, string id);

        void DeleteClient(ClientViewModel clientViewModel);

        bool ClientExists(Guid id);       
    }
}
