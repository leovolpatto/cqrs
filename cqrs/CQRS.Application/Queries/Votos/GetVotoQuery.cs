using CQRS.Domain.Models;
using MediatR;

namespace CQRS.Application.Queries.Votos
{
    public class GetVotoQuery : IRequest<VotoResponse>
    {
        public int Id { get; private set; }

        public GetVotoQuery(int userId)
        {
            this.Id = userId;
        }
    }
}