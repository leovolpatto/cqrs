using CQRS.Domain.Communication;
using CQRS.Domain.Models;
using MediatR;

namespace  CQRS.Application.Commands.Votos
{
    public class UpdateVotoCommand : IRequest<Response<Voto>>
    {
        public int Opcao { get; set; }

        public UpdateVotoCommand(int opcao)
        {
            Opcao = opcao;
        }

    }
}