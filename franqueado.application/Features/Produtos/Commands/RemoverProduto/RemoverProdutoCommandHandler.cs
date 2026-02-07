using Franqueado.Application.Abstractions;
using Franqueado.Domain.Repositories;
using MediatR;

namespace franqueado.application.Features.Produtos.Commands.RemoverProduto;

public sealed class RemoverProdutoCommandHandler : IRequestHandler<RemoverProdutoCommand>
{
    private readonly IProdutoRepository _repo;
    private readonly IUnitOfWork _uow;

    public RemoverProdutoCommandHandler(IProdutoRepository repo, IUnitOfWork uow)
    {
        _repo = repo;
        _uow = uow;
    }

    public async Task Handle(RemoverProdutoCommand request, CancellationToken ct)
    {
        var produto = await _repo.GetByIdAsync(request.Id, ct)
            ?? throw new KeyNotFoundException("Produto não encontrado.");

        _repo.Remove(produto);
        await _uow.SaveChangesAsync(ct);
    }
}
