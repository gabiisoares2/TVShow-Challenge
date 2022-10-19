using TVShow.Infrastructure.Audit.Contracts;


namespace TVShow.Infrastructure.Audit
{
    public sealed class FunctionAuditEntrySaveChangesHandler<T> where T : IAuditDbContext
    {
        private readonly T _dbContext;

        public Guid UserId { get; private set; }
        public string UserName { get; private set; }
        public string FunctionName { get; private set; }

        public FunctionAuditEntrySaveChangesHandler(T dbContext)
        {
            _dbContext = dbContext;

            if (!dbContext.SaveChangesPrepared)
            {
                _dbContext.SaveChangesEvent += DbContext_SaveChangesEvent;
                _dbContext.SaveChangesPrepared = true;
            }
        }

        private void DbContext_SaveChangesEvent(object source, AuditEntrySaveChangesEvent args)
        {
            if (UserId == Guid.Empty)
                throw new ArgumentException("Informe o UserId", nameof(UserId));

            if (string.IsNullOrEmpty(UserName))
                throw new ArgumentException("Informe o UserName", nameof(UserName));

            args.Entry.UserId = UserId.ToString();
            args.Entry.UserName = UserName.ToString();
            args.Entry.FunctionName = FunctionName;
        }

        public void SetAuditData(Guid userId, string userName, string functionName)
        {
            UserId = userId;
            UserName = userName;
            FunctionName = functionName;
        }

      


    }
}
