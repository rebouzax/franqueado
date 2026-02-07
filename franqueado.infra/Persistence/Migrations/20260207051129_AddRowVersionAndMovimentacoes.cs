using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace franqueado.infra.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddRowVersionAndMovimentacoes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "Estoques",
                type: "rowversion",
                rowVersion: true,
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.CreateTable(
                name: "MovimentacoesEstoque",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FranqueadoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProdutoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Tipo = table.Column<int>(type: "int", nullable: false),
                    Quantidade = table.Column<int>(type: "int", nullable: false),
                    Motivo = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Usuario = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: true),
                    CriadoEm = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovimentacoesEstoque", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MovimentacoesEstoque_FranqueadoId_ProdutoId_CriadoEm",
                table: "MovimentacoesEstoque",
                columns: new[] { "FranqueadoId", "ProdutoId", "CriadoEm" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MovimentacoesEstoque");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "Estoques");
        }
    }
}
