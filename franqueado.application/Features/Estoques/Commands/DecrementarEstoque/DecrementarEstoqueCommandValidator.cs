using FluentValidation;

namespace Franqueado.Application.Features.Estoques.Commands.DecrementarEstoque;

public sealed class DecrementarEstoqueCommandValidator : AbstractValidator<DecrementarEstoqueCommand>
{
    public DecrementarEstoqueCommandValidator()
    {
        RuleFor(x => x.FranqueadoId).NotEmpty();
        RuleFor(x => x.ProdutoId).NotEmpty();
        RuleFor(x => x.Quantidade).GreaterThan(0);
        RuleFor(x => x.Motivo).MaximumLength(200);
    }
}
