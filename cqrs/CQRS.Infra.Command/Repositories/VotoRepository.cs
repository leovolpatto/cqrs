using CQRS.Domain.Interfaces;
using CQRS.Domain.Models;

namespace CQRS.Infra.Command.Repositories
{
    public class VotoRepository : Repository<Voto>, IVotoRepository
    {
        public VotoRepository(EntityContext context) : base(context)
        {
        }
    }
}