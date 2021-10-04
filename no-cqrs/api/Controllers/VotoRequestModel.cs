using api.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace api.Controllers
{
    public sealed class VotoRequestModel
    {
        [Required]
        [DataType(DataType.DateTime)]
        public DateTime data { get; set; }

        [Required]
        public int valor { get; set; }

        public Voto ToVotoModel()
        {
            Voto voto = new Voto()
            {
                Data = this.data,
                Valor = this.valor
            };

            return voto;
        }
    }
}
