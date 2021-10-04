using System;
using System.Globalization;

namespace CQRS.Domain.Models
{
    public static class VotoToVotoResponse
    {

        public static VotoResponse Transform(this Voto voto)
        {
            CultureInfo culture = new CultureInfo("pt-BR");
            return new VotoResponse
            {
                Guid = Guid.NewGuid(),
                Id = voto.Id,
                Data = Convert.ToDateTime(voto.Data, culture).ToString(),
                Opcao = voto.Opcao

            };
        }


    }
}

