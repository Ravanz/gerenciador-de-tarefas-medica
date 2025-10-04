using System;

namespace vSaude.Models
{
    public class TarefaCreateDto
    {
        public required string Titulo { get; set; }
        public string? Descricao { get; set; }
        public PrioridadeEnum Prioridade { get; set; }
        public CategoriaEnum Categoria { get; set; }
    }

    public class TarefaUpdateDto
    {
        public required string Titulo { get; set; }
        public string? Descricao { get; set; }
        public PrioridadeEnum Prioridade { get; set; }
        public CategoriaEnum Categoria { get; set; }
        public StatusEnum Status { get; set; }
    }

    public class TarefaViewDto
    {
        public int Id { get; set; }
        public required string Titulo { get; set; }
        public string? Descricao { get; set; }
        public PrioridadeEnum Prioridade { get; set; }
        public string PrioridadeDescricao { get; set; } = string.Empty;
        public CategoriaEnum Categoria { get; set; }
        public string CategoriaDescricao { get; set; } = string.Empty;
        public StatusEnum Status { get; set; }
        public string StatusDescricao { get; set; } = string.Empty;
        public DateTime DataCriacao { get; set; }
    }
}


