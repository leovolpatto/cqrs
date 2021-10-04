using api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Linq;
using System;

namespace api.Controllers
{
    [ApiController]
    [Route("api/v1/votos")]
    public class VotosController : ControllerBase
    {
        private readonly ILogger<VotosController> _logger;
        private readonly EntityContext _entityContext;

        public VotosController(ILogger<VotosController> logger, EntityContext entityContext)
        {
            _logger = logger;
            _entityContext = entityContext;
        }


        [HttpPost("simulate")]
        public async Task SimularMuitos()
        {
            Random r = new Random();
            const int million = 1000000;
            for (int i = 0; i < 10 * million; i++)
            {
                for (int opcaoVotada = 0; opcaoVotada < 10; opcaoVotada++)
                {
                    _entityContext.Add(new Voto()
                    {
                        Data = System.DateTime.Now,
                        Valor = opcaoVotada
                    });

                    _entityContext.Add(new Voto()
                    {
                        Data = System.DateTime.Now,
                        Valor = r.Next(0, 4)
                    });
                }

                await _entityContext.SaveChangesAsync();
            }
        }

        [HttpPost]
        public IActionResult PostVoto([FromBody] VotoRequestModel votoRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Voto voto = votoRequest.ToVotoModel();
            _entityContext.Add(voto);
            _entityContext.SaveChanges();

            return Ok(voto);
        }


        [HttpGet("resultados")]
        public object GetVotosStats()
        {
            var total = _entityContext.Votos.OrderBy(votos => votos.Data).Count();

            var todos = _entityContext.Votos.Where(v => v.Id != 0).ToList();

            var x = (from voto in todos
                    group voto by voto.Valor into valores
                    select new
                    {
                        totalDeVotos = total,
                        opcaoVotada = valores.Key,
                        count = valores.Count(),
                        votos = valores.Take(10)
                    }).ToArray();

            return Ok(x);
        }
    }
}
