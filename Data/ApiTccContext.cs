using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ApiTcc;
using ApiTcc.Entidades;

namespace ApiTcc.Data
{
    public class ApiTccContext : DbContext
    {
        public ApiTccContext (DbContextOptions<ApiTccContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Pratos_Ingredientes>()
                .HasKey(c => new { c.pratoID, c.ingredienteID });

            modelBuilder.Entity<Pratos>()
                .Ignore(x => x.Ingredientes)
                .HasMany(c => c.pratos_Ingredientes)
                .WithOne()
                .HasForeignKey(x => x.pratoID);

            modelBuilder.Entity<Ingredientes>()
                .HasMany(c => c.pratos_Ingredientes)
                .WithOne()
                .HasForeignKey(x => x.ingredienteID);
        }

        public DbSet<ApiTcc.Usuarios> Usuarios { get; set; }

        public DbSet<ApiTcc.Entidades.Ingredientes> Ingredientes { get; set; }

        public DbSet<ApiTcc.Entidades.Pratos> Pratos { get; set; }

        public DbSet<ApiTcc.Entidades.Pedidos> Pedidos { get; set; }

        public DbSet<ApiTcc.Entidades.Pratos_DiaSemana> Pratos_DiaSemana { get; set; }
    }
}
