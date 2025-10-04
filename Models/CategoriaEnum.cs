using System.ComponentModel.DataAnnotations;

namespace vSaude.Models
{
    public enum CategoriaEnum
    {
        [Display(Name = "Consulta Médica")]
        Consulta = 1,

        [Display(Name = "Procedimento Cirúrgico")]
        Cirurgia = 2,

        [Display(Name = "Exame Laboratorial/Imagem")]
        Exame = 3,

        [Display(Name = "Administração de Medicamento")]
        Medicacao = 4,

        [Display(Name = "Atendimento de Emergência")]
        Emergencia = 5,

        [Display(Name = "Internação Hospitalar")]
        Internacao = 6
    }
}