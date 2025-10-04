using System;
using vSaude.Models;

namespace vSaude.Models
{
    public class TarefaMedica
    {
        public int Id { get; set; }
        public required string Titulo { get; set; }
        public string? Descricao { get; set; }
        public PrioridadeEnum Prioridade { get; set; }
        public CategoriaEnum Categoria { get; set; }
        public StatusEnum Status { get; set; }
        public DateTime DataCriacao { get; set; }
        public bool IsDeleted { get; set; }
    }
}
