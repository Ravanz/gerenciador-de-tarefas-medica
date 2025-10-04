using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace vSaude.Models
{
    public class TarefaRepository : ITarefaRepository
    {
        private readonly AppDbContext _db;

        public TarefaRepository(AppDbContext db)
        {
            _db = db;
        }

        public IQueryable<TarefaMedica> Query()
        {
            return _db.Tarefas.AsQueryable();
        }

        public async Task<TarefaMedica?> GetByIdAsync(int id)
        {
            return await _db.Tarefas.FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task AddAsync(TarefaMedica entity)
        {
            await _db.Tarefas.AddAsync(entity);
        }

        public void Update(TarefaMedica entity)
        {
            _db.Tarefas.Update(entity);
        }

        public void SoftDelete(TarefaMedica entity)
        {
            entity.IsDeleted = true;
            _db.Tarefas.Update(entity);
        }
    }
}


