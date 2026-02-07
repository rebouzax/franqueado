using FluentValidation;

namespace Franqueado.Application.Features.Estoques.Commands.IncrementarEstoque;

public sealed class IncrementarEstoqueCommandValidator : AbstractValidator<IncrementarEstoqueCommand>
{
    public IncrementarEstoqueCommandValidator()
    {
        RuleFor(x => x.FranqueadoId).NotEmpty();
        RuleFor(x => x.ProdutoId).NotEmpty();
        RuleFor(x => x.Quantidade).GreaterThan(0);

        RuleFor(x => x.Motivo).MaximumLength(200);
        RuleFor(x => x.Usuario).MaximumLength(120);
    }
}
