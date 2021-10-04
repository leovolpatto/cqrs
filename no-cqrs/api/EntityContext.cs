using api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace api
{
    public sealed class EntityContext : DbContext
    {
        public DbSet<Voto> Votos { get; set; }

        public EntityContext(DbContextOptions<EntityContext> context) : base(context) { }
    }
}
