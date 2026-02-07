using FluentValidation;

namespace Franqueado.Application.Features.Produtos.Commands;

public sealed class CriarProdutoCommandValidator : AbstractValidator<CriarProdutoCommand>
{
    public CriarProdutoCommandValidator()
    {
        RuleFor(x => x.Nome)
            .NotEmpty()
            .MaximumLength(120);

        RuleFor(x => x.Sku)
            .NotEmpty()
            .MaximumLength(60);
    }
}
