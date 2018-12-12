using System.Linq;
using System.Threading.Tasks;
using WebWallet.Models.Contracts;

namespace WebWallet.Data.Contracts
{
    public interface IRepository<TEntity> where TEntity : class, IEntity
    {
        IQueryable<TEntity> GetAll();

        Task<TEntity> GetById(string id);

        Task Create(TEntity entity);

        Task Update(string id, TEntity entity);

        Task Delete(string id);
    }
}