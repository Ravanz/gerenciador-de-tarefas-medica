using System.ComponentModel.DataAnnotations;

namespace vSaude.Models
{
    public enum StatusEnum
    {
        [Display(Name = "Aguardando Atendimento")]
        Pendente = 0,

        [Display(Name = "Em Atendimento")]
        EmAndamento = 1,

        [Display(Name = "Atendimento Concluído")]
        Concluida = 2,

        [Display(Name = "Cancelada pelo Paciente")]
        Cancelada = 3
    }
}
