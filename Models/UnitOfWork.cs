using System.Threading.Tasks;

namespace vSaude.Models
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _db;

        public UnitOfWork(AppDbContext db, ITarefaRepository tarefas)
        {
            _db = db;
            Tarefas = tarefas;
        }

        public ITarefaRepository Tarefas { get; }

        public async Task<int> SaveChangesAsync()
        {
            return await _db.SaveChangesAsync();
        }
    }
}



