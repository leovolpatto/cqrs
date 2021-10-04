using CQRS.Domain.Communication;
using CQRS.Domain.Models;
using MediatR;

namespace CQRS.Application.Commands.Votos
{
    public class DeleteVotoCommand : IRequest<Response<Voto>>
    {
        public int Id { get; private set; }

        public DeleteVotoCommand(int id)
        {
            this.Id = id;
        }
    }
}