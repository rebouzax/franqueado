using FluentValidation;

namespace Franqueado.Application.Features.Estoques.Queries.ListarEstoques;

public sealed class ListarEstoquesQueryValidator : AbstractValidator<ListarEstoquesQuery>
{
    public ListarEstoquesQueryValidator()
    {
        RuleFor(x => x.Page).GreaterThan(0);
        RuleFor(x => x.PageSize).InclusiveBetween(1, 100);
    }
}
