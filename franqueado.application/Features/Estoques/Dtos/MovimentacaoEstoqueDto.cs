using Franqueado.Domain.Enums;

namespace Franqueado.Application.Features.Estoques.Dtos;

public sealed record MovimentacaoEstoqueDto(
    Guid Id,
    Guid FranqueadoId,
    Guid ProdutoId,
    TipoMovimentacaoEstoque Tipo,
    int Quantidade,
    string? Motivo,
    string? Usuario,
    DateTimeOffset CriadoEm
);
