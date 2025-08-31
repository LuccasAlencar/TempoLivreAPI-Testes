using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TempoLivreAPI.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Abrigos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    Nome = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    Endereco = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true),
                    Latitude = table.Column<double>(type: "BINARY_DOUBLE", nullable: true),
                    Longitude = table.Column<double>(type: "BINARY_DOUBLE", nullable: true),
                    CapacidadeMax = table.Column<int>(type: "NUMBER(10)", nullable: true),
                    DisponibilidadeAtual = table.Column<int>(type: "NUMBER(10)", nullable: true),
                    Contato = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Abrigos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sensors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    TipoSensor = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true),
                    LocalizacaoLat = table.Column<double>(type: "BINARY_DOUBLE", nullable: true),
                    LocalizacaoLong = table.Column<double>(type: "BINARY_DOUBLE", nullable: true),
                    Status = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true),
                    DataInstalacao = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sensors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    Nome = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    Email = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    Senha = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    DataCadastro = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Leituras",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    SensorId = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    ValorLido = table.Column<double>(type: "BINARY_DOUBLE", nullable: false),
                    DataHora = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    UnidadeMedida = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Leituras", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Leituras_Sensors_SensorId",
                        column: x => x.SensorId,
                        principalTable: "Sensors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Alertas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    UsuarioId = table.Column<int>(type: "NUMBER(10)", nullable: true),
                    TipoEvento = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    NivelAlerta = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    Mensagem = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    Latitude = table.Column<double>(type: "BINARY_DOUBLE", nullable: true),
                    Longitude = table.Column<double>(type: "BINARY_DOUBLE", nullable: true),
                    DataEmissao = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    Status = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Alertas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Alertas_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Localizacoes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    UsuarioId = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    Latitude = table.Column<double>(type: "BINARY_DOUBLE", nullable: false),
                    Longitude = table.Column<double>(type: "BINARY_DOUBLE", nullable: false),
                    DataHoraRegistro = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Localizacoes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Localizacoes_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Ocorrencias",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    UsuarioId = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    TipoOcorrencia = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true),
                    Descricao = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true),
                    Latitude = table.Column<double>(type: "BINARY_DOUBLE", nullable: true),
                    Longitude = table.Column<double>(type: "BINARY_DOUBLE", nullable: true),
                    DataEnvio = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    Status = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ocorrencias", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ocorrencias_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RotasSeguras",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    UsuarioId = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    AbrigoDestinoId = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    OrigemLatitude = table.Column<double>(type: "BINARY_DOUBLE", nullable: true),
                    OrigemLongitude = table.Column<double>(type: "BINARY_DOUBLE", nullable: true),
                    DestinoLatitude = table.Column<double>(type: "BINARY_DOUBLE", nullable: true),
                    DestinoLongitude = table.Column<double>(type: "BINARY_DOUBLE", nullable: true),
                    TipoRota = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RotasSeguras", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RotasSeguras_Abrigos_AbrigoDestinoId",
                        column: x => x.AbrigoDestinoId,
                        principalTable: "Abrigos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RotasSeguras_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Alertas_UsuarioId",
                table: "Alertas",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Leituras_SensorId",
                table: "Leituras",
                column: "SensorId");

            migrationBuilder.CreateIndex(
                name: "IX_Localizacoes_UsuarioId",
                table: "Localizacoes",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Ocorrencias_UsuarioId",
                table: "Ocorrencias",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_RotasSeguras_AbrigoDestinoId",
                table: "RotasSeguras",
                column: "AbrigoDestinoId");

            migrationBuilder.CreateIndex(
                name: "IX_RotasSeguras_UsuarioId",
                table: "RotasSeguras",
                column: "UsuarioId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Alertas");

            migrationBuilder.DropTable(
                name: "Leituras");

            migrationBuilder.DropTable(
                name: "Localizacoes");

            migrationBuilder.DropTable(
                name: "Ocorrencias");

            migrationBuilder.DropTable(
                name: "RotasSeguras");

            migrationBuilder.DropTable(
                name: "Sensors");

            migrationBuilder.DropTable(
                name: "Abrigos");

            migrationBuilder.DropTable(
                name: "Usuarios");
        }
    }
}
