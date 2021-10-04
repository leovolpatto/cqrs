using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace api.Models
{
    public class Voto
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public DateTime Data { get; set; }
        public int Valor { get; set; }
    }
}
