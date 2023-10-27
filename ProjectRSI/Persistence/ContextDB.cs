using Microsoft.EntityFrameworkCore;
using ProjectRSI.Domain;

namespace ProjectRSI.Persistence
{
    public class ContextDB : DbContext
    {
        public ContextDB(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<ActivoFijo>()
             .HasOne(af => af.Categoria)
             .WithMany(c => c.ActivosFijos)
             .HasForeignKey(af => af.IdCategoria);
        }

        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<ActivoFijo> ActivosFijos { get; set; }
    }
}
