using System.Threading.Tasks;

namespace vSaude.Models
{
    public interface IUnitOfWork
    {
        ITarefaRepository Tarefas { get; }
        Task<int> SaveChangesAsync();
    }
}



