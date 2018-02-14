using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using WattsCRM.Data;
using WattsCRM.Models;
using Microsoft.AspNetCore.Identity;

namespace WattsCRM.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<WattsCRM.Models.ClientViewModel> ClientViewModel { get; set; }

        DbSet<WattsCRM.Models.ClientNoteViewModel> ClientNoteViewModel { get; set; }

        void UpdateClientModel(ClientViewModel clientViewModel);

        EntityEntry<ClientViewModel> AddClient(ClientViewModel clientViewModel);

        EntityEntry<ClientViewModel> RemoveClient(ClientViewModel clientViewModel);

        Task<int> Save();
    }
}
