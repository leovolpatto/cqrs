using System.Threading;
using System.Threading.Tasks;
using CQRS.Domain.Interfaces;
using MediatR;

namespace CQRS.Application.Queries.Votos
{
    public class ListVotosQueryHandler : IRequestHandler<ListVotosQuery, object>
    {
        private readonly IVotoListRepository _votosRepository;

        public ListVotosQueryHandler(IVotoListRepository votosRepository)
        {
            _votosRepository = votosRepository;
        }

        public async Task<object> Handle(ListVotosQuery request, CancellationToken cancellationToken)
        {
            return await _votosRepository.ListAsync();
        }
    }
}