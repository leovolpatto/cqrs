using CQRS.Domain.Models;
using MediatR;
using System;

namespace CQRS.Application.Commands.Votos
{
    public class CreateVotoCommand : IRequest<Voto>
    {    
        public int Valor { get; private set; }
        public DateTime Data { get; set; }

        public CreateVotoCommand(int valor, DateTime data)
        {
            this.Valor = valor;
            this.Data = data;
        }
    }
}