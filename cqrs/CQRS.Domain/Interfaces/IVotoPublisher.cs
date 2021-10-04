using CQRS.Domain.Models;
using System.Threading.Tasks;

namespace CQRS.Domain.Interfaces
{
    public interface IVotoPublisher
    {
        Task Send(VotoResponse updateAudit);
    }
}
