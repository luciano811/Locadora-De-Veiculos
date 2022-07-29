﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LocadoraDeVeiculos.Infra.Orm.Migrations
{
    public partial class AddValidacaoExclusaoTaxaPlanoCobranca : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TBLocacao_TBPlanoCobranca_PlanoCobrancaId",
                table: "TBLocacao");

            migrationBuilder.DropTable(
                name: "LocacaoTaxa");

            migrationBuilder.AddColumn<Guid>(
                name: "LocacaoId",
                table: "TBTaxa",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "TaxaId",
                table: "TBLocacao",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TBTaxa_LocacaoId",
                table: "TBTaxa",
                column: "LocacaoId");

            migrationBuilder.CreateIndex(
                name: "IX_TBLocacao_TaxaId",
                table: "TBLocacao",
                column: "TaxaId");

            migrationBuilder.AddForeignKey(
                name: "FK_TBLocacao_TBPlanoCobranca_PlanoCobrancaId",
                table: "TBLocacao",
                column: "PlanoCobrancaId",
                principalTable: "TBPlanoCobranca",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TBLocacao_TBTaxa_TaxaId",
                table: "TBLocacao",
                column: "TaxaId",
                principalTable: "TBTaxa",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TBTaxa_TBLocacao_LocacaoId",
                table: "TBTaxa",
                column: "LocacaoId",
                principalTable: "TBLocacao",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TBLocacao_TBPlanoCobranca_PlanoCobrancaId",
                table: "TBLocacao");

            migrationBuilder.DropForeignKey(
                name: "FK_TBLocacao_TBTaxa_TaxaId",
                table: "TBLocacao");

            migrationBuilder.DropForeignKey(
                name: "FK_TBTaxa_TBLocacao_LocacaoId",
                table: "TBTaxa");

            migrationBuilder.DropIndex(
                name: "IX_TBTaxa_LocacaoId",
                table: "TBTaxa");

            migrationBuilder.DropIndex(
                name: "IX_TBLocacao_TaxaId",
                table: "TBLocacao");

            migrationBuilder.DropColumn(
                name: "LocacaoId",
                table: "TBTaxa");

            migrationBuilder.DropColumn(
                name: "TaxaId",
                table: "TBLocacao");

            migrationBuilder.CreateTable(
                name: "LocacaoTaxa",
                columns: table => new
                {
                    LocacoesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TaxasSelecionadasId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LocacaoTaxa", x => new { x.LocacoesId, x.TaxasSelecionadasId });
                    table.ForeignKey(
                        name: "FK_LocacaoTaxa_TBLocacao_LocacoesId",
                        column: x => x.LocacoesId,
                        principalTable: "TBLocacao",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LocacaoTaxa_TBTaxa_TaxasSelecionadasId",
                        column: x => x.TaxasSelecionadasId,
                        principalTable: "TBTaxa",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LocacaoTaxa_TaxasSelecionadasId",
                table: "LocacaoTaxa",
                column: "TaxasSelecionadasId");

            migrationBuilder.AddForeignKey(
                name: "FK_TBLocacao_TBPlanoCobranca_PlanoCobrancaId",
                table: "TBLocacao",
                column: "PlanoCobrancaId",
                principalTable: "TBPlanoCobranca",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
