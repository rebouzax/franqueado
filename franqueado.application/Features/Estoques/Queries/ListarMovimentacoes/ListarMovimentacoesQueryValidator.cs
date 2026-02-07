using FluentValidation;
using franqueado.application.Features.Estoques.Queries.ListarMovimentacoes;

namespace Franqueado.Application.Features.Estoques.Queries.ListarMovimentacoes;

public sealed class ListarMovimentacoesQueryValidator : AbstractValidator<ListarMovimentacoesQuery>
{
    public ListarMovimentacoesQueryValidator()
    {
        RuleFor(x => x.FranqueadoId).NotEmpty();
        RuleFor(x => x.ProdutoId).NotEmpty();
        RuleFor(x => x.Page).GreaterThan(0);
        RuleFor(x => x.PageSize).InclusiveBetween(1, 100);

        RuleFor(x => x).Must(x =>
        {
            if (!x.De.HasValue || !x.Ate.HasValue) return true;
            return x.De.Value <= x.Ate.Value;
        }).WithMessage("'De' deve ser menor ou igual a 'Ate'.");
    }
}
