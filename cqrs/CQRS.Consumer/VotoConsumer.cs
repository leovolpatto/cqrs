using CQRS.Domain.Interfaces;
using CQRS.Domain.Models;
using MassTransit;
using System.Threading.Tasks;

namespace CQRS.Consumer
{
    public class VotoConsumer : IConsumer<VotoResponse>
    {
        private IUpdateNOSQL _updateNOSQL;
        public VotoConsumer(IUpdateNOSQL updateNOSQL)
        {
            _updateNOSQL = updateNOSQL;
        }
        public async Task Consume(ConsumeContext<VotoResponse> context)
        {
            await _updateNOSQL.UpdateAsync(context.Message);
        }
    }
}
