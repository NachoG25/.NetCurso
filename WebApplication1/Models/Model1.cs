namespace WebApplication1.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Model1 : DbContext
    {
        public Model1()
            : base("name=modeloC")
        {
        }

        public virtual DbSet<notas> notas { get; set; }
        public virtual DbSet<usuarios> usuarios { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<notas>()
                .Property(e => e.texto)
                .IsUnicode(false);

            modelBuilder.Entity<usuarios>()
                .Property(e => e.nombre)
                .IsUnicode(false);

            modelBuilder.Entity<usuarios>()
                .Property(e => e.apellido)
                .IsUnicode(false);

            modelBuilder.Entity<usuarios>()
                .Property(e => e.nameuser)
                .IsUnicode(false);

            modelBuilder.Entity<usuarios>()
                .Property(e => e.pw)
                .IsUnicode(false);

            modelBuilder.Entity<usuarios>()
                .HasMany(e => e.notas)
                .WithOptional(e => e.usuarios)
                .HasForeignKey(e => e.notas_usuario_fk);
        }
    }
}
