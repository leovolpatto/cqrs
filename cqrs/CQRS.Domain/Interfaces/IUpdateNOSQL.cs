using CQRS.Domain.Models;
using System.Threading.Tasks;

namespace CQRS.Domain.Interfaces
{
    public interface IUpdateNOSQL
    {
        Task UpdateAsync(VotoResponse voto);
    }
}
