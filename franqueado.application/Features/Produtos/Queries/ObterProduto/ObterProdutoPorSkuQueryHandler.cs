using Franqueado.Application.Abstractions.Repositories;
using Franqueado.Application.Features.Produtos.Dtos;
using MediatR;

namespace franqueado.application.Features.Produtos.Queries.ObterProduto;

public sealed class ObterProdutoPorSkuQueryHandler : IRequestHandler<ObterProdutoPorSkuQuery, ProdutoDto?>
{
    private readonly IProdutoReadRepository _readRepo;

    public ObterProdutoPorSkuQueryHandler(IProdutoReadRepository readRepo) => _readRepo = readRepo;

    public Task<ProdutoDto?> Handle(ObterProdutoPorSkuQuery request, CancellationToken ct)
        => _readRepo.GetBySkuAsync(request.Sku, ct);
}
