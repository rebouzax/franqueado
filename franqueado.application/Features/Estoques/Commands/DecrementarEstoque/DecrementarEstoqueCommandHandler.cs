using Franqueado.Application.Abstractions;
using Franqueado.Domain.Entities;
using Franqueado.Domain.Enums;
using Franqueado.Domain.Repositories;
using MediatR;

namespace Franqueado.Application.Features.Estoques.Commands.DecrementarEstoque;

public sealed class DecrementarEstoqueCommandHandler : IRequestHandler<DecrementarEstoqueCommand>
{
    private readonly IEstoqueRepository _repo;
    private readonly IMovimentacaoEstoqueRepository _movRepo;
    private readonly IUnitOfWork _uow;

    public DecrementarEstoqueCommandHandler(IEstoqueRepository repo, IMovimentacaoEstoqueRepository movRepo, IUnitOfWork uow)
    {
        _repo = repo;
        _movRepo = movRepo;
        _uow = uow;
    }

    public async Task Handle(DecrementarEstoqueCommand request, CancellationToken ct)
    {
        var estoque = await _repo.GetAsync(request.FranqueadoId, request.ProdutoId, ct)
            ?? throw new KeyNotFoundException("Registro de estoque não encontrado para este franqueado/produto.");

        estoque.Decrementar(request.Quantidade);

        var mov = new MovimentacaoEstoque(
            request.FranqueadoId,
            request.ProdutoId,
            TipoMovimentacaoEstoque.Saida,
            request.Quantidade,
            motivo: request.Motivo,
            usuario: request.Usuario);

        await _movRepo.AddAsync(mov, ct);

        await _uow.SaveChangesAsync(ct);
    }
}
