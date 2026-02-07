using Franqueado.Application.Abstractions.Sorting;
using Franqueado.Application.Features.Produtos.Commands;
using Franqueado.Application.Features.Produtos.Queries;
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
}
