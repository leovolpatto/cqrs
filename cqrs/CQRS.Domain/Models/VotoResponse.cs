using System;

namespace CQRS.Domain.Models
{
    public class VotoResponse
    {
        public Guid Guid { get; set; }
        public int Id { get; set; }
        public string Data { get; set; }
        public int Opcao { get; set; }



    }
}