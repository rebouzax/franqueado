using Franqueado.Application.Abstractions;
using Franqueado.Domain.Entities;
using Franqueado.Domain.Repositories;
using MediatR;

namespace franqueado.application.Features.Estoques.Commands.DefinirEstoque;

public sealed class DefinirEstoqueCommandHandler : IRequestHandler<DefinirEstoqueCommand>
{
    private readonly IEstoqueRepository _repo;
    private readonly IUnitOfWork _uow;

    public DefinirEstoqueCommandHandler(IEstoqueRepository repo, IUnitOfWork uow)
    {
        _repo = repo;
        _uow = uow;
    }

    public async Task Handle(DefinirEstoqueCommand request, CancellationToken ct)
    {
        var estoque = await _repo.GetAsync(request.FranqueadoId, request.ProdutoId, ct);

        if (estoque is null)
        {
            estoque = new Estoque(request.FranqueadoId, request.ProdutoId, request.Quantidade);
            await _repo.AddAsync(estoque, ct);
        }
        else
        {
            estoque.DefinirQuantidade(request.Quantidade);
        }

        await _uow.SaveChangesAsync(ct);
    }
}
