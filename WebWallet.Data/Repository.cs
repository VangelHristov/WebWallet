using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using WebWallet.Data.Contracts;
using WebWallet.Models.Contracts;

namespace WebWallet.Data
{
    public class Repository<TEntity> : IRepository<TEntity>
    where TEntity : class, IEntity
    {
        private readonly WebWalletDBContext _dbContext;

        public Repository(WebWalletDBContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public async Task Create(TEntity entity)
        {
            await this._dbContext.Set<TEntity>().AddAsync(entity);
            await this._dbContext.SaveChangesAsync();
        }

        public async Task Delete(string id)
        {
            var entity = await this.GetById(id);
            this._dbContext.Set<TEntity>().Remove(entity);
            await this._dbContext.SaveChangesAsync();
        }

        public IQueryable<TEntity> GetAll()
        {
            return _dbContext.Set<TEntity>().AsNoTracking();
        }

        public async Task<TEntity> GetById(string id)
        {
            return await _dbContext.Set<TEntity>()
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task Update(string id, TEntity entity)
        {
            this._dbContext.Set<TEntity>().Update(entity);
            await this._dbContext.SaveChangesAsync();
        }
    }
}