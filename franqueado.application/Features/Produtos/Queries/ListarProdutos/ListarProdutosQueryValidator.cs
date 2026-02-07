using FluentValidation;

namespace franqueado.application.Features.Produtos.Queries.ListarProdutos;

public sealed class ListarProdutosQueryValidator : AbstractValidator<ListarProdutosQuery>
{
    public ListarProdutosQueryValidator()
    {
        RuleFor(x => x.Page).GreaterThan(0);
        RuleFor(x => x.PageSize).InclusiveBetween(1, 100);
    }
}
