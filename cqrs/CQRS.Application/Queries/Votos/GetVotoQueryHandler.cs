using System;
using System.Threading;
using System.Threading.Tasks;
using CQRS.Application.Queries.Votos;
using CQRS.Domain.Interfaces;
using CQRS.Domain.Models;
using MediatR;

namespace CQRS.Application.Command
{
    public class GetVotoQueryHandler : IRequestHandler<GetVotoQuery, VotoResponse>
    {
        private readonly IVotoListRepository _votosRepository;

        public GetVotoQueryHandler(IVotoListRepository votosRepository)
        {
            _votosRepository = votosRepository;
        }

        public async Task<VotoResponse> Handle(GetVotoQuery request, CancellationToken cancellationToken)
        {
            if (request.Id <= 0)
            {
                throw new ArgumentException(nameof(request.Id));
            }

            return await _votosRepository.FindByIdAsync(request.Id);
             
        }
    }
}