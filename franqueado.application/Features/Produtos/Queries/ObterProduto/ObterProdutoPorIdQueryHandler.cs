using Franqueado.Application.Abstractions.Repositories;
using Franqueado.Application.Features.Produtos.Dtos;
using MediatR;

namespace franqueado.application.Features.Produtos.Queries.ObterProduto;

public sealed class ObterProdutoPorIdQueryHandler : IRequestHandler<ObterProdutoPorIdQuery, ProdutoDto?>
{
    private readonly IProdutoReadRepository _readRepo;

    public ObterProdutoPorIdQueryHandler(IProdutoReadRepository readRepo) => _readRepo = readRepo;

    public Task<ProdutoDto?> Handle(ObterProdutoPorIdQuery request, CancellationToken ct)
        => _readRepo.GetByIdAsync(request.Id, ct);
}
