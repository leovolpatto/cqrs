using System.Threading;
using System.Threading.Tasks;

namespace CQRS.Domain.Interfaces
{
    public interface IUnitOfWork
    {
        Task<int> CompleteAsync();
        Task<int> CompleteAsync(CancellationToken token);
  
    }
}