using System;

namespace CQRS.Domain.Models
{
    public class Voto 
    {
        public int Id { get; set; }
        public DateTime Data { get; set; }
        public int Opcao { get; set; }
 
    }

}