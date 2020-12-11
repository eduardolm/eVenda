using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace eVendas.Warehouse.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Produtos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    codigo = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false),
                    nome = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    preco = table.Column<decimal>(type: "decimal(2)", precision: 2, nullable: false),
                    quantidade = table.Column<int>(type: "int", nullable: false),
                    data_cadastro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    data_atualizacao = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Produtos", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Produtos");
        }
    }
}
