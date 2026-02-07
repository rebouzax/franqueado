using FluentValidation;

namespace franqueado.application.Features.Estoques.Commands.DefinirEstoque;

public sealed class DefinirEstoqueCommandValidator : AbstractValidator<DefinirEstoqueCommand>
{
    public DefinirEstoqueCommandValidator()
    {
        RuleFor(x => x.FranqueadoId).NotEmpty();
        RuleFor(x => x.ProdutoId).NotEmpty();
        RuleFor(x => x.Quantidade).GreaterThanOrEqualTo(0);
    }
}
