using FluentValidation;

namespace franqueado.application.Features.Produtos.Commands.AtualizarProdouto;

public sealed class AtualizarProdutoCommandValidator : AbstractValidator<AtualizarProdutoCommand>
{
    public AtualizarProdutoCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
        RuleFor(x => x.Nome).NotEmpty().MaximumLength(120);
        RuleFor(x => x.Sku).NotEmpty().MaximumLength(60);
    }
}
