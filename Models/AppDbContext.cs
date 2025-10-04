using System;
using Microsoft.EntityFrameworkCore;

namespace vSaude.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<TarefaMedica> Tarefas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var tarefa = modelBuilder.Entity<TarefaMedica>();
            
            tarefa.HasKey(t => t.Id);
            tarefa.Property(t => t.Titulo).IsRequired().HasMaxLength(100);
            tarefa.Property(t => t.Descricao).HasMaxLength(1000);
            tarefa.Property(t => t.DataCriacao).HasDefaultValueSql("GETUTCDATE()");
            tarefa.Property(t => t.IsDeleted).HasDefaultValue(false);
            tarefa.HasQueryFilter(t => !t.IsDeleted);

            base.OnModelCreating(modelBuilder);
        }

        public override int SaveChanges()
        {
            SetDataCriacao();
            return base.SaveChanges();
        }

        public override async System.Threading.Tasks.Task<int> SaveChangesAsync(System.Threading.CancellationToken cancellationToken = default)
        {
            SetDataCriacao();
            return await base.SaveChangesAsync(cancellationToken);
        }

        private void SetDataCriacao()
        {
            var entries = ChangeTracker.Entries<TarefaMedica>();
            foreach (var entry in entries)
            {
                if (entry.State == EntityState.Added && entry.Entity.DataCriacao == default)
                {
                    entry.Entity.DataCriacao = DateTime.UtcNow;
                }
            }
        }
    }
}



