
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TVShow.Domain.Core.Interface;
using TVShow.Infrastructure.Context;

namespace TVShow.Infrastructure.Core
{
    public class AppRepository<TEntity, TKey> : IAppRespository<TEntity, TKey> where TEntity : class
    {
        protected readonly TVShowDbContext Db;
        protected readonly DbSet<TEntity> DbSet;

        public AppRepository(TVShowDbContext db)
        {
            Db = db;
            DbSet = db.Set<TEntity>();
        }

        public void Delete(TEntity entity)
        {
            DbSet.Remove(entity);
        }

        public void Delete(TKey id)
        {
            DbSet.Remove(DbSet.Find(id));
        }

        public void Dispose()
        {
            if (Db != null) { Db.Dispose(); }
            GC.SuppressFinalize(this);
        }

        public IQueryable<TEntity> GetAll()
        {
            return Db.Set<TEntity>();
        }

        public TEntity Get(TKey id)
        {
            return DbSet.Find(id);
        }

        public async Task<TEntity> GetAsync(Expression<Func<TEntity, object>>[] includes, Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken)
        {
            var query = DbSet.AsQueryable();
            foreach (var item in includes) query = query.Include(item);
            return await query.FirstOrDefaultAsync(predicate, cancellationToken);


        }

        public ValueTask<TEntity> GetAsync(TKey id)
        {
            return DbSet.FindAsync(id);
        }

        public ValueTask<TEntity> GetAsync(TKey id, CancellationToken cancellationToken)
        {
            object[] keyValues = { id };

            return DbSet.FindAsync(keyValues: keyValues, cancellationToken: cancellationToken);
        }

        public TEntity Add(TEntity entity)
        {
            DbSet.Add(entity);
            return entity;
        }

        public async ValueTask<TEntity> AddAsync(TEntity entity)
        {
            await DbSet.AddAsync(entity);
            return entity;
        }

        public int SaveChanges()
        {
            return Db.SaveChanges();
        }

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            return Db.SaveChangesAsync(cancellationToken);
        }

        public TEntity Update(TEntity entity)
        {
            Db.Entry(entity).State = EntityState.Modified;
            DbSet.Update(entity);
            return entity;
        }

        public void Attach(TEntity entity)
        {
            Db.Attach(entity);
            Db.Entry(entity).State = EntityState.Modified;
        }
        public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> expression)
        {
            return await GetAll().AnyAsync(expression);
        }
    }
}
