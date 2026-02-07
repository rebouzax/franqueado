using Franqueado.Application.Abstractions;
using Franqueado.Domain.Repositories;
using MediatR;

namespace Franqueado.Application.Features.Estoques.Commands.DecrementarEstoque;

public sealed class DecrementarEstoqueCommandHandler : IRequestHandler<DecrementarEstoqueCommand>
{
    private readonly IEstoqueRepository _repo;
    private readonly IUnitOfWork _uow;

    public DecrementarEstoqueCommandHandler(IEstoqueRepository repo, IUnitOfWork uow)
    {
        _repo = repo;
        _uow = uow;
    }

    public async Task Handle(DecrementarEstoqueCommand request, CancellationToken ct)
    {
        var estoque = await _repo.GetAsync(request.FranqueadoId, request.ProdutoId, ct)
            ?? throw new KeyNotFoundException("Registro de estoque não encontrado para este franqueado/produto.");

        estoque.Decrementar(request.Quantidade);

        await _uow.SaveChangesAsync(ct);
    }
}
