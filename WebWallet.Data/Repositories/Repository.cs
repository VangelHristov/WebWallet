using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using WebWallet.Data.Contracts;
using WebWallet.Models.Contracts;

namespace WebWallet.Data.Repositories
{
    public class Repository<TEntity> : BaseRepository, IRepository<TEntity>
    where TEntity : class, IEntity
    {
        private readonly WebWalletDBContext _dbContext;

        public Repository(WebWalletDBContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public async Task<bool> Create(TEntity entity)
        {
            await this._dbContext.Set<TEntity>().AddAsync(entity);
            return (await this._dbContext.SaveChangesAsync()) > 0;
        }

        public async Task<bool> Delete(string id)
        {
            var entity = await this.GetById(id);
            ThrowIfIsNull(entity);
            this._dbContext.Set<TEntity>().Remove(entity);
            return (await this._dbContext.SaveChangesAsync()) > 0;
        }

        public IQueryable<TEntity> GetAll()
        {
            return _dbContext.Set<TEntity>().AsNoTracking();
        }

        public async Task<TEntity> GetById(string id)
        {
            var entity = await _dbContext.Set<TEntity>()
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.Id == id);

            ThrowIfIsNull(entity);
            return entity;
        }

        public async Task<bool> Update(TEntity entity)
        {
            this._dbContext.Set<TEntity>().Update(entity);
            return (await this._dbContext.SaveChangesAsync()) > 0;
        }
    }
}