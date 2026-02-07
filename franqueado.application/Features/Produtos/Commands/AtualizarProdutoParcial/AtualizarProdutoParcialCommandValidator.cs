using FluentValidation;

namespace franqueado.application.Features.Produtos.Commands.AtualizarProdutoParcial;

public sealed class AtualizarProdutoParcialCommandValidator : AbstractValidator<AtualizarProdutoParcialCommand>
{
    public AtualizarProdutoParcialCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
        RuleFor(x => x.Nome).MaximumLength(120);
        RuleFor(x => x.Sku).MaximumLength(60);
    }
}
