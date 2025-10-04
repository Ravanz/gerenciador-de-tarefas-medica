using FluentValidation;

namespace vSaude.Models
{
    public class TarefaCreateValidator : AbstractValidator<TarefaCreateDto>
    {
        public TarefaCreateValidator()
        {
            RuleFor(x => x.Titulo)
                .NotEmpty()
                .MaximumLength(100);

            RuleFor(x => x.Descricao)
                .MaximumLength(1000);

            RuleFor(x => x.Prioridade)
                .IsInEnum();

            RuleFor(x => x.Categoria)
                .IsInEnum();
        }
    }

    public class TarefaUpdateValidator : AbstractValidator<TarefaUpdateDto>
    {
        public TarefaUpdateValidator()
        {
            RuleFor(x => x.Titulo)
                .NotEmpty()
                .MaximumLength(100);

            RuleFor(x => x.Descricao)
                .MaximumLength(1000);

            RuleFor(x => x.Prioridade)
                .IsInEnum();

            RuleFor(x => x.Categoria)
                .IsInEnum();

            RuleFor(x => x.Status)
                .IsInEnum();
        }
    }
}



