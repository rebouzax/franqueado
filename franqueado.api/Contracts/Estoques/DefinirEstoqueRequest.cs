namespace Franqueado.Api.Contracts.Estoques;

public sealed class DefinirEstoqueRequest
{
    public int Quantidade { get; set; }
    public string RowVersion { get; set; } = default!;
}
