using Franqueado.Application.Abstractions;
using Franqueado.Domain.Repositories;
using MediatR;

namespace franqueado.application.Features.Produtos.Commands.AtualizarProdouto;

public sealed class AtualizarProdutoCommandHandler : IRequestHandler<AtualizarProdutoCommand>
{
    private readonly IProdutoRepository _repo;
    private readonly IUnitOfWork _uow;

    public AtualizarProdutoCommandHandler(IProdutoRepository repo, IUnitOfWork uow)
    {
        _repo = repo;
        _uow = uow;
    }

    public async Task Handle(AtualizarProdutoCommand request, CancellationToken ct)
    {
        var produto = await _repo.GetByIdAsync(request.Id, ct)
            ?? throw new KeyNotFoundException("Produto não encontrado.");

        var normalizedSku = request.Sku.Trim().ToUpperInvariant();
        if (!string.Equals(produto.Sku, normalizedSku, StringComparison.OrdinalIgnoreCase))
        {
            if (await _repo.ExistsBySkuAsync(normalizedSku, ct))
                throw new InvalidOperationException("Já existe produto com este SKU.");
        }

        produto.Atualizar(request.Nome, request.Sku);

        await _uow.SaveChangesAsync(ct);
    }
}
