using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebWallet.Data.Contracts;
using WebWallet.Models.Entities;

namespace WebWallet.Tests.Mocks
{
    public class InvestmentRepositoryMock : IRepository<Investment>
    {
        private readonly IList<Investment> _context;

        public InvestmentRepositoryMock(IList<Investment> context)
        {
            _context = context;
        }

        public IList<Investment> Context => _context;

        public IQueryable<Investment> GetAll()
        {
            return _context.AsQueryable();
        }

        public Task<Investment> GetById(string id)
        {
            var entity = _context.FirstOrDefault(x => x.Id == id);
            return Task.Factory.StartNew(() => entity);
        }

        public Task<bool> Create(Investment entity)
        {
            _context.Add(entity);
            return Task.FromResult(true);
        }

        public Task<bool> Update(Investment entity)
        {
            var updated = false;
            for (int i = 0; i < _context.Count; i++)
            {
                if (_context[i].Id == entity.Id)
                {
                    _context[i] = entity;
                    updated = true;
                    break;
                }
            }

            return Task.FromResult(updated);
        }

        public Task<bool> Delete(string id)
        {
            var deleted = false;
            for (int i = 0; i < _context.Count; i++)
            {
                if (_context[i].Id == id)
                {
                    _context.RemoveAt(i);
                    deleted = true;
                    break;
                }
            }

            return Task.FromResult(deleted);
        }
    }
}