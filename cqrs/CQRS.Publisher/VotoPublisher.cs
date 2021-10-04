using CQRS.Domain.Interfaces;
using CQRS.Domain.Models;
using MassTransit;
using System.Threading.Tasks;

namespace CQRS.Publisher
{
    public class VotoPublisher : IVotoPublisher
    {
        private readonly IBus _bus;
        public VotoPublisher(IBus bus)
        {
            _bus = bus;
        }
        public async Task Send(VotoResponse voto)
        {
            await _bus.Send(voto);
        }
    
    }
}
