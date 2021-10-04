using System.Threading;
using System.Threading.Tasks;
using CQRS.Domain.Interfaces;
using CQRS.Domain.Models;
using CQRS.Publisher;
using MediatR;

namespace CQRS.Application.Commands.Votos
{
    public class CreateVotoCommandHandler : IRequestHandler<CreateVotoCommand, Voto>
    {
        private readonly IVotoRepository _votoRepository;
        private readonly IVotoPublisher _voto;
        private readonly IUnitOfWork _unitOfWork;

        public CreateVotoCommandHandler(
            IUnitOfWork unitOfWork,
            IVotoRepository votoRepository,
            IVotoPublisher votoPublisher)
        {

            _unitOfWork = unitOfWork;
            _voto = votoPublisher;
            _votoRepository = votoRepository;
        }

        public async Task<Voto> Handle(CreateVotoCommand request, CancellationToken cancellationToken)
        {
            var voto = new Voto
            {
                Opcao = request.Valor,
                Data = request.Data,
            };

            await _votoRepository.AddAsync(voto);
            await _unitOfWork.CompleteAsync();

            await _voto.Send(voto.Transform());

            return voto;
        }
    }
}