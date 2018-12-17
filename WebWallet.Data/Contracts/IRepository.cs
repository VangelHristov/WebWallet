using System.Linq;
using System.Threading.Tasks;
using WebWallet.Models.Contracts;

namespace WebWallet.Data.Contracts
{
    public interface IRepository<TEntity> where TEntity : class, IEntity
    {
        IQueryable<TEntity> GetAll();

        Task<TEntity> GetById(string id);

        Task<bool> Create(TEntity entity);

        Task<bool> Update(TEntity entity);

        Task<bool> Delete(string id);
    }
}