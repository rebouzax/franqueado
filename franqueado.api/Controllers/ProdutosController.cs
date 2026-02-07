using franqueado.application.Features.Produtos.Commands.AtualizarProdouto;
using franqueado.application.Features.Produtos.Commands.AtualizarProdutoParcial;
using franqueado.application.Features.Produtos.Commands.CriarProduto;
using franqueado.application.Features.Produtos.Commands.RemoverProduto;
using franqueado.application.Features.Produtos.Queries.ListarProdutos;
using franqueado.application.Features.Produtos.Queries.ObterProduto;
using Franqueado.Api.Contracts.Produtos;
using Franqueado.Application.Abstractions.Sorting;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Franqueado.Api.Controllers;

[ApiController]
[Route("api/produtos")]
public sealed class ProdutosController : ControllerBase
{
    private readonly ISender _sender;

    public ProdutosController(ISender sender) => _sender = sender;

    [HttpGet]
    public async Task<IActionResult> Listar(
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10,
        [FromQuery] string? search = null,
        [FromQuery] ProdutoSortBy sortBy = ProdutoSortBy.Nome,
        [FromQuery] SortDirection sortDir = SortDirection.Asc,
        CancellationToken ct = default)
    {
        var result = await _sender.Send(new ListarProdutosQuery(page, pageSize, search, sortBy, sortDir), ct);
        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> ObterPorId([FromRoute] Guid id, CancellationToken ct)
    {
        var produto = await _sender.Send(new ObterProdutoPorIdQuery(id), ct);
        return produto is null ? NotFound() : Ok(produto);
    }

    [HttpPost]
    public async Task<IActionResult> Criar([FromBody] CriarProdutoCommand command, CancellationToken ct)
    {
        var result = await _sender.Send(command, ct);
        return CreatedAtAction(nameof(ObterPorId), new { id = result.Id }, result);
    }

    [HttpGet("sku/{sku}")]
    public async Task<IActionResult> ObterPorSku([FromRoute] string sku, CancellationToken ct)
    {
        var produto = await _sender.Send(new ObterProdutoPorSkuQuery(sku), ct);
        return produto is null ? NotFound() : Ok(produto);
    }


    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Atualizar([FromRoute] Guid id, [FromBody] AtualizarProdutoCommand body, CancellationToken ct)
    {
        var cmd = body with { Id = id };
        await _sender.Send(cmd, ct);
        return NoContent();
    }

    [HttpPatch("{id:guid}")]
    public async Task<IActionResult> AtualizarParcial([FromRoute] Guid id, [FromBody] AtualizarProdutoPatchRequest body, CancellationToken ct)
    {
        await _sender.Send(new AtualizarProdutoParcialCommand(id, body.Nome, body.Sku), ct);
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Remover([FromRoute] Guid id, CancellationToken ct)
    {
        await _sender.Send(new RemoverProdutoCommand(id), ct);
        return NoContent();
    }
}
