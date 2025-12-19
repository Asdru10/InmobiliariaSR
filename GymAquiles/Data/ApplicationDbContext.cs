using ProyectoInmobilaria.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.Numerics;
using System.Reflection.Emit;
using NuGet.Protocol;


namespace ProyectoInmobilaria.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<AplicationUser> ApplicationUsers { get; set; }

       
        public DbSet<Ubicacion> Ubicaciones { get; set; }

        public DbSet<Contacto> Contactos { get; set; }

        public DbSet<Caracteristicas> Caracteristicas { get; set; }

        public DbSet<Propiedad> Propiedades { get; set; }

        public DbSet<PropiedadCaracteristica> PropiedadCaracteristicas { get; set; }

        public DbSet<Favorito> Favorito { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<PropiedadCaracteristica>()
                .HasKey(x => new { x.CaracteristicasId, x.PropiedadId });


            builder.Entity<Propiedad>().HasOne(p => p.Ubicacion).WithOne(u => u.Propiedad).HasForeignKey<Ubicacion>(u => u.PropiedadId).OnDelete(DeleteBehavior.Restrict);
            builder.Entity<Contacto>().HasOne(d => d.User).WithMany().HasForeignKey(d => d.UserId).OnDelete(DeleteBehavior.Restrict);
            builder.Entity<Propiedad>().HasOne(d => d.User).WithMany().HasForeignKey(d => d.UserId).OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Favorito>()
            .HasKey(f => f.Id);

            builder.Entity<Favorito>()
                .HasIndex(f => new { f.UserId, f.PropiedadId })
            .IsUnique();

            builder.Entity<Favorito>()
                .HasOne(f => f.User)
                .WithMany(u => u.Favoritos)
                .HasForeignKey(f => f.UserId);

            builder.Entity<Favorito>()
                .HasOne(f => f.Propiedad)
                .WithMany(p => p.Favoritos)
                .HasForeignKey(f => f.PropiedadId);


        }
    }

}