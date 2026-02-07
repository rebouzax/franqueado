using Franqueado.Application.Abstractions;
using Franqueado.Application.Features.Produtos.Dtos;
using Franqueado.Domain.Entities;
using Franqueado.Domain.Repositories;
using MediatR;

namespace franqueado.application.Features.Produtos.Commands.CriarProduto;

public sealed class CriarProdutoCommandHandler : IRequestHandler<CriarProdutoCommand, ProdutoDto>
{
    private readonly IProdutoRepository _repo;
    private readonly IUnitOfWork _uow;

    public CriarProdutoCommandHandler(IProdutoRepository repo, IUnitOfWork uow)
    {
        _repo = repo;
        _uow = uow;
    }

    public async Task<ProdutoDto> Handle(CriarProdutoCommand request, CancellationToken ct)
    {
        var skuNormalizado = request.Sku.Trim().ToUpperInvariant();

        if (await _repo.ExistsBySkuAsync(skuNormalizado, ct))
            throw new InvalidOperationException("Já existe produto com este SKU.");

        var produto = new Produto(request.Nome, request.Sku);

        await _repo.AddAsync(produto, ct);
        await _uow.SaveChangesAsync(ct);

        return new ProdutoDto(produto.Id, produto.Nome, produto.Sku);
    }
}
