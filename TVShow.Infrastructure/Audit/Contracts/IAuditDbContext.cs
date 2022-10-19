using System;

namespace TVShow.Infrastructure.Audit.Contracts
{
    public interface IAuditDbContext
    {
        delegate void SaveChangesEventHandler(object source, AuditEntrySaveChangesEvent args);
        event SaveChangesEventHandler SaveChangesEvent;
        bool SaveChangesPrepared { get; set; }
    }
}

