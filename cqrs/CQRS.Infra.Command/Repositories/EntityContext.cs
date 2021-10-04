using CQRS.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace CQRS.Infra.Command.Repositories
{
    public class EntityContext : DbContext
    {
        public DbSet<Voto> Votos { get; set; }

        public EntityContext(DbContextOptions<EntityContext> options) : base(options)
        {

        }


    }
}