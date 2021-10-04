using CQRS.Domain.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace CQRS.Infra.Command.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly EntityContext _context;


        public UnitOfWork(EntityContext context)
        {
            _context = context;
        }

        public async Task<int> CompleteAsync()
        {

            return await _context.SaveChangesAsync();
        }

        public async Task<int> CompleteAsync(CancellationToken token)
        {

            return await _context.SaveChangesAsync(token);
        }

    }
}