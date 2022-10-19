using TVShow.Infrastructure.Audit.Contracts;

namespace TVShow.Infrastructure.Audit
{
    public sealed class AuditEntrySaveChangesHandler<T> where T : IAuditDbContext
    {
        private readonly T _dbContext;
        private readonly Guid _scopeId;

        public AuditEntrySaveChangesHandler(T dbContext)
        {
            _dbContext = dbContext;
        }
        
    }

   
}

