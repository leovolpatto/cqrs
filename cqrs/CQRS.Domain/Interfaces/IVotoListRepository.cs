using CQRS.Domain.Models;
using System.Threading.Tasks;

namespace CQRS.Domain.Interfaces
{
    public interface IVotoListRepository 
    {

        Task<VotoResponse> FindByIdAsync(int Id);

        Task<object> ListAsync();


    }
}
