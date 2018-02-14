using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using WattsCRM.Models;
using WattsCRM.Interfaces;

namespace WattsCRM.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>, IApplicationDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ClientViewModel>()
           .ToTable("client");

            builder.Entity<ClientNoteViewModel>()
            .ToTable("clientnote");

            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }

        public EntityEntry<ClientViewModel> AddClient(ClientViewModel clientViewModel)
        {
            return base.Add(clientViewModel);
        }
        public EntityEntry<ClientViewModel> RemoveClient(ClientViewModel clientViewModel)
        {
            return base.Remove(clientViewModel);
        }

        public void UpdateClientModel(ClientViewModel clientViewModel)
        {
            this.Update(clientViewModel);
        }

        public Task<int> Save()
        {
            return base.SaveChangesAsync();
        }

        public DbSet<WattsCRM.Models.ClientViewModel> ClientViewModel { get; set; }

        public DbSet<WattsCRM.Models.ClientNoteViewModel> ClientNoteViewModel { get; set; }
    }
}
