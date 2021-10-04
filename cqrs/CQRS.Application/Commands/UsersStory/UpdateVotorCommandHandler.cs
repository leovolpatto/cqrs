using System.Threading;
using System.Threading.Tasks;
using CQRS.Domain.Communication;
using CQRS.Domain.Interfaces;
using CQRS.Domain.Models;
using MediatR;

namespace CQRS.Application.Commands.Votos
{
    public class UpdateVotorCommandHandler : IRequestHandler<UpdateVotoCommand, Response<Voto>>
    {
        private readonly IVotoRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateVotorCommandHandler(IVotoRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Response<Voto>> Handle(UpdateVotoCommand request, CancellationToken cancellationToken)
        {
            return await Task<Response<Voto>>.FromResult<Response<Voto>>(new Response<Voto>(new Voto()));
        }
    }
}