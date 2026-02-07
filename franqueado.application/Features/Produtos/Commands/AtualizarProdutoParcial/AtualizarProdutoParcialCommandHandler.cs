using Franqueado.Application.Abstractions;
using Franqueado.Domain.Repositories;
using MediatR;

namespace franqueado.application.Features.Produtos.Commands.AtualizarProdutoParcial;

public sealed class AtualizarProdutoParcialCommandHandler : IRequestHandler<AtualizarProdutoParcialCommand>
{
    private readonly IProdutoRepository _repo;
    private readonly IUnitOfWork _uow;

    public AtualizarProdutoParcialCommandHandler(IProdutoRepository repo, IUnitOfWork uow)
    {
        _repo = repo;
        _uow = uow;
    }

    public async Task Handle(AtualizarProdutoParcialCommand request, CancellationToken ct)
    {
        var produto = await _repo.GetByIdAsync(request.Id, ct)
            ?? throw new KeyNotFoundException("Produto não encontrado.");

        var novoNome = request.Nome ?? produto.Nome;
        var novoSku = request.Sku ?? produto.Sku;

        var normalizedSku = novoSku.Trim().ToUpperInvariant();
        if (!string.Equals(produto.Sku, normalizedSku, StringComparison.OrdinalIgnoreCase))
        {
            if (await _repo.ExistsBySkuAsync(normalizedSku, ct))
                throw new InvalidOperationException("Já existe produto com este SKU.");
        }

        produto.Atualizar(novoNome, novoSku);

        await _uow.SaveChangesAsync(ct);
    }
}
