using Franqueado.Application.Abstractions;
using Franqueado.Domain.Entities;
using Franqueado.Domain.Repositories;
using MediatR;

namespace Franqueado.Application.Features.Estoques.Commands.IncrementarEstoque;

public sealed class IncrementarEstoqueCommandHandler : IRequestHandler<IncrementarEstoqueCommand>
{
    private readonly IEstoqueRepository _repo;
    private readonly IUnitOfWork _uow;

    public IncrementarEstoqueCommandHandler(IEstoqueRepository repo, IUnitOfWork uow)
    {
        _repo = repo;
        _uow = uow;
    }

    public async Task Handle(IncrementarEstoqueCommand request, CancellationToken ct)
    {
        var estoque = await _repo.GetAsync(request.FranqueadoId, request.ProdutoId, ct);

        if (estoque is null)
        {
            estoque = new Estoque(request.FranqueadoId, request.ProdutoId, request.Quantidade);
            await _repo.AddAsync(estoque, ct);
        }
        else
        {
            estoque.Incrementar(request.Quantidade);
        }

        await _uow.SaveChangesAsync(ct);
    }
}
