using franqueado.application.Features.Estoques.Commands.DefinirEstoque;
using Franqueado.Application.Abstractions.Repositories;
using Franqueado.Application.Features.Estoques.Commands.DecrementarEstoque;
using Franqueado.Application.Features.Estoques.Commands.IncrementarEstoque;
using Franqueado.Application.Features.Estoques.Queries.ListarEstoques;
using Franqueado.Application.Features.Estoques.Queries.ListarEstoquesComProduto;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Franqueado.Api.Controllers;

[ApiController]
[Route("api/estoques")]
public sealed class EstoquesController : ControllerBase
{
    private readonly ISender _sender;
    private readonly IEstoqueReadRepository _readRepo;

    public EstoquesController(ISender sender, IEstoqueReadRepository readRepo)
    {
        _sender = sender;
        _readRepo = readRepo;
    }

    [HttpGet("{franqueadoId:guid}/{produtoId:guid}")]
    public async Task<IActionResult> Obter(Guid franqueadoId, Guid produtoId, CancellationToken ct)
    {
        var dto = await _readRepo.GetAsync(franqueadoId, produtoId, ct);
        return dto is null ? NotFound() : Ok(dto);
    }

    [HttpPut("{franqueadoId:guid}/{produtoId:guid}")]
    public async Task<IActionResult> Definir(Guid franqueadoId, Guid produtoId, [FromBody] int quantidade, CancellationToken ct)
    {
        await _sender.Send(new DefinirEstoqueCommand(franqueadoId, produtoId, quantidade), ct);
        return NoContent();
    }

    [HttpGet]
    public async Task<IActionResult> Listar(
    [FromQuery] int page = 1,
    [FromQuery] int pageSize = 10,
    [FromQuery] Guid? franqueadoId = null,
    CancellationToken ct = default)
    {
        var result = await _sender.Send(new ListarEstoquesQuery(page, pageSize, franqueadoId), ct);
        return Ok(result);
    }

    [HttpPost("{franqueadoId:guid}/{produtoId:guid}/entrada")]
    public async Task<IActionResult> Entrada(Guid franqueadoId, Guid produtoId, [FromBody] MovimentarEstoqueRequest body, CancellationToken ct)
    {
        await _sender.Send(new IncrementarEstoqueCommand(franqueadoId, produtoId, body.Quantidade), ct);
        return NoContent();
    }

    [HttpPost("{franqueadoId:guid}/{produtoId:guid}/saida")]
    public async Task<IActionResult> Saida(Guid franqueadoId, Guid produtoId, [FromBody] MovimentarEstoqueRequest body, CancellationToken ct)
    {
        await _sender.Send(new DecrementarEstoqueCommand(franqueadoId, produtoId, body.Quantidade), ct);
        return NoContent();
    }

    [HttpGet("com-produto")]
    public async Task<IActionResult> ListarComProduto(
    [FromQuery] int page = 1,
    [FromQuery] int pageSize = 10,
    [FromQuery] Guid? franqueadoId = null,
    [FromQuery] string? search = null,
    CancellationToken ct = default)
    {
        var result = await _sender.Send(new ListarEstoquesComProdutoQuery(page, pageSize, franqueadoId, search), ct);
        return Ok(result);
    }
}
