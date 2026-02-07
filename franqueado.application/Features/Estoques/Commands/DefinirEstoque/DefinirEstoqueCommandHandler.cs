using Franqueado.Application.Abstractions;
using Franqueado.Domain.Repositories;
using MediatR;

namespace Franqueado.Application.Features.Estoques.Commands.DefinirEstoque;

public sealed class DefinirEstoqueCommandHandler : IRequestHandler<DefinirEstoqueCommand>
{
    private readonly IEstoqueRepository _repo;
    private readonly IUnitOfWork _uow;
    private readonly IConcurrencyService _concurrency;

    public DefinirEstoqueCommandHandler(IEstoqueRepository repo, IUnitOfWork uow, IConcurrencyService concurrency)
    {
        _repo = repo;
        _uow = uow;
        _concurrency = concurrency;
    }

    public async Task Handle(DefinirEstoqueCommand request, CancellationToken ct)
    {
        var estoque = await _repo.GetAsync(request.FranqueadoId, request.ProdutoId, ct)
            ?? throw new KeyNotFoundException("Registro de estoque não encontrado para este franqueado/produto.");

        var expected = Convert.FromBase64String(request.RowVersion);
        _concurrency.SetOriginalRowVersion(estoque, expected);

        estoque.DefinirQuantidade(request.Quantidade);

        await _uow.SaveChangesAsync(ct);
    }
}
