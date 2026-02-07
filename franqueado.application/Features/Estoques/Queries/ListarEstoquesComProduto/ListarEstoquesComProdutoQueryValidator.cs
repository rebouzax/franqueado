using FluentValidation;

namespace Franqueado.Application.Features.Estoques.Queries.ListarEstoquesComProduto;

public sealed class ListarEstoquesComProdutoQueryValidator : AbstractValidator<ListarEstoquesComProdutoQuery>
{
    public ListarEstoquesComProdutoQueryValidator()
    {
        RuleFor(x => x.Page).GreaterThan(0);
        RuleFor(x => x.PageSize).InclusiveBetween(1, 100);
        RuleFor(x => x.Search).MaximumLength(120);
    }
}
