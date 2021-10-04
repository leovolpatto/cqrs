using System;
using System.Threading.Tasks;
using CQRS.Application.Commands.Votos;
using CQRS.Application.Queries.Votos;
using CQRS.Domain.Models;
using CQRSLaqus.Controllers.Resources;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CQRSLaqus.Controllers
{
    [Route("api/v1/votos")]
    [Produces("application/json")]
    [ApiController]
    public class VotosController : ControllerBase
    {
        private readonly IMediator _mediator;

        public VotosController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("resultados")]
        public async Task<object> ListAsync()
        {
            return await _mediator.Send(new ListVotosQuery());
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(VotoResponse), 200)]
        public async Task<IActionResult> GetAsync(int id)
        {
            var voto = await _mediator.Send(new GetVotoQuery(id));
            if (voto == null)
            {
                return NotFound();
            }

            return Ok(voto);
        }
   
        [HttpPost]
        [ProducesResponseType(typeof(Voto), 201)]
        [ProducesResponseType(typeof(ErrorResource), 400)]
        public async Task<IActionResult> PostAsync([FromBody] VotoResource resource)
        {
            var voto = await _mediator.Send(new CreateVotoCommand(resource.Valor, DateTime.Now));
            return Created($"/api/votos/{voto.Id}", voto);
        }

        [HttpPost("simulate")]
        [ProducesResponseType(typeof(Voto), 200)]
        [ProducesResponseType(typeof(ErrorResource), 400)]
        public async Task SimularMuitos()
        {
            const int million = 1000000;
            for (int i = 0; i < 10 * million; i++)
            {
                for (int opcaoVotada = 0; opcaoVotada < 10; opcaoVotada++)
                {
                    await _mediator.Send(new CreateVotoCommand(opcaoVotada, DateTime.Now));
                }
            }
        }
    }
}