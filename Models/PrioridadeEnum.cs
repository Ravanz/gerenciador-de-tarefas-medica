using System.ComponentModel.DataAnnotations;

namespace vSaude.Models
{
    public enum PrioridadeEnum
    {
        [Display(Name = "Baixa")]
        Baixa = 0,

        [Display(Name = "Média")]
        Media = 1,

        [Display(Name = "Alta")]
        Alta = 2,

        [Display(Name = "Crítica")]
        Critica = 3
    }
}