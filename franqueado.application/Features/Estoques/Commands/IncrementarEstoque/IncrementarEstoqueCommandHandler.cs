using Franqueado.Application.Abstractions;
using Franqueado.Domain.Entities;
using Franqueado.Domain.Enums;
using Franqueado.Domain.Repositories;
using MediatR;

namespace Franqueado.Application.Features.Estoques.Commands.IncrementarEstoque;

public sealed class IncrementarEstoqueCommandHandler : IRequestHandler<IncrementarEstoqueCommand>
{
    private readonly IEstoqueRepository _repo;
    private readonly IMovimentacaoEstoqueRepository _movRepo;
    private readonly IUnitOfWork _uow;
    private readonly ICurrentUser _currentUser;

    public IncrementarEstoqueCommandHandler(IEstoqueRepository repo, IMovimentacaoEstoqueRepository movRepo, IUnitOfWork uow, ICurrentUser currentUser)
    {
        _repo = repo;
        _movRepo = movRepo;
        _uow = uow;
        _currentUser = currentUser;
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

        // auditoria
        var mov = new MovimentacaoEstoque(
            request.FranqueadoId,
            request.ProdutoId,
            TipoMovimentacaoEstoque.Entrada,
            request.Quantidade,
            motivo: request.Motivo,
            usuario: _currentUser.Username);


        await _movRepo.AddAsync(mov, ct);

        await _uow.SaveChangesAsync(ct);
    }
}
