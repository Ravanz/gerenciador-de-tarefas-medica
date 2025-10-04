using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace vSaude.Models
{
    public interface ITarefaRepository
    {
        Task<TarefaMedica?> GetByIdAsync(int id);
        Task AddAsync(TarefaMedica entity);
        void Update(TarefaMedica entity);
        void SoftDelete(TarefaMedica entity);
        IQueryable<TarefaMedica> Query();
    }
}


