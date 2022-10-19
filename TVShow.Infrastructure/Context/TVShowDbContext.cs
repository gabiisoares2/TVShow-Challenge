using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TVShow.Domain.Entity;
using TVShow.Infrastructure.Audit;
using TVShow.Infrastructure.Audit.Contracts;
using TVShow.Infrastructure.ModelConfiguration;

namespace TVShow.Infrastructure.Context
{
    public class TVShowDbContext : IdentityDbContext<IdentityUser<Guid>, IdentityRole<Guid>, Guid>, IAuditDbContext
    {
        public TVShowDbContext(DbContextOptions<TVShowDbContext> opt) : base(opt)
        {
            this.Database.EnsureCreated();
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new TvShowConfig());
            modelBuilder.ApplyConfiguration(new EpisodeConfig());
            modelBuilder.ApplyConfiguration(new PictureConfig());
        }

        public DbSet<TvShow> TvShows { get; set; }
        public DbSet<Episodes> Episodes { get; set; }
        public DbSet<Picture> Pictures { get; set; }


        public bool SaveChangesPrepared { get; set; }

        public event IAuditDbContext.SaveChangesEventHandler SaveChangesEvent;

        public override int SaveChanges()
        {
            BeforeSave();

            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            BeforeSave();

            return base.SaveChangesAsync(cancellationToken);
        }

        private void BeforeSave()
        {
            var changedEntities = ChangeTracker.Entries();

            foreach (var changedEntity in changedEntities)
            {
                if (changedEntity.State == EntityState.Detached || changedEntity.State == EntityState.Unchanged)
                    continue;

                if (changedEntity.Entity is BaseEntity)
                {
                    var entity = (BaseEntity)changedEntity.Entity;

                    switch (changedEntity.State)
                    {
                        case EntityState.Added:
                            if (entity.Id == null || entity.Id == Guid.Empty)
                                entity.Id = Guid.NewGuid();
                            entity.CreateDate = DateTime.Now;
                            entity.UpdateDate = DateTime.Now;
                            break;

                        case EntityState.Modified:
                            entity.UpdateDate = DateTime.Now;
                            break;
                    }
                }

                SaveChangesEvent?.Invoke(this, new AuditEntrySaveChangesEvent { Entry = AuditEntry.Create(changedEntity) });
            }
        }
    }
}
