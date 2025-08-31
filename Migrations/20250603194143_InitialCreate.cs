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
                name: "Abrigo",
                columns: table => new
                {
                    id_abrigo = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    nome = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false),
                    endereco = table.Column<string>(type: "NVARCHAR2(200)", maxLength: 200, nullable: true),
                    latitude = table.Column<double>(type: "BINARY_DOUBLE", nullable: true),
                    longitude = table.Column<double>(type: "BINARY_DOUBLE", nullable: true),
                    capacidade_max = table.Column<int>(type: "NUMBER(10)", nullable: true),
                    disponibilidade_atual = table.Column<int>(type: "NUMBER(10)", nullable: true),
                    contato = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Abrigo", x => x.id_abrigo);
                });

            migrationBuilder.CreateTable(
                name: "Sensor",
                columns: table => new
                {
                    id_sensor = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    tipo_sensor = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: true),
                    localizacao_lat = table.Column<double>(type: "BINARY_DOUBLE", nullable: true),
                    localizacao_long = table.Column<double>(type: "BINARY_DOUBLE", nullable: true),
                    status = table.Column<string>(type: "NVARCHAR2(20)", maxLength: 20, nullable: true),
                    data_instalacao = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sensor", x => x.id_sensor);
                });

            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    id_usuario = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    nome = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false),
                    email = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false),
                    senha = table.Column<string>(type: "NVARCHAR2(200)", maxLength: 200, nullable: false),
                    data_cadastro = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario", x => x.id_usuario);
                });

            migrationBuilder.CreateTable(
                name: "LeituraSensor",
                columns: table => new
                {
                    id_leitura = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    id_sensor = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    valor_lido = table.Column<double>(type: "BINARY_DOUBLE", nullable: false),
                    data_hora = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    unidade_medida = table.Column<string>(type: "NVARCHAR2(20)", maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeituraSensor", x => x.id_leitura);
                    table.ForeignKey(
                        name: "FK_LeituraSensor_Sensor_id_sensor",
                        column: x => x.id_sensor,
                        principalTable: "Sensor",
                        principalColumn: "id_sensor",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Alerta",
                columns: table => new
                {
                    id_alerta = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    id_usuario = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    tipo_evento = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false),
                    nivel_alerta = table.Column<string>(type: "NVARCHAR2(20)", maxLength: 20, nullable: false),
                    mensagem = table.Column<string>(type: "NVARCHAR2(255)", maxLength: 255, nullable: true),
                    latitude = table.Column<double>(type: "BINARY_DOUBLE", nullable: true),
                    longitude = table.Column<double>(type: "BINARY_DOUBLE", nullable: true),
                    data_emissao = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    status = table.Column<string>(type: "NVARCHAR2(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Alerta", x => x.id_alerta);
                    table.ForeignKey(
                        name: "FK_Alerta_Usuario_id_usuario",
                        column: x => x.id_usuario,
                        principalTable: "Usuario",
                        principalColumn: "id_usuario",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LocalizacaoUsuario",
                columns: table => new
                {
                    id_localizacao = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    id_usuario = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    latitude = table.Column<double>(type: "BINARY_DOUBLE", nullable: false),
                    longitude = table.Column<double>(type: "BINARY_DOUBLE", nullable: false),
                    data_hora_registro = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LocalizacaoUsuario", x => x.id_localizacao);
                    table.ForeignKey(
                        name: "FK_LocalizacaoUsuario_Usuario_id_usuario",
                        column: x => x.id_usuario,
                        principalTable: "Usuario",
                        principalColumn: "id_usuario",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OcorrenciaColaborativa",
                columns: table => new
                {
                    id_ocorrencia = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    id_usuario = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    tipo_ocorrencia = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: true),
                    descricao = table.Column<string>(type: "NVARCHAR2(255)", maxLength: 255, nullable: true),
                    latitude = table.Column<double>(type: "BINARY_DOUBLE", nullable: true),
                    longitude = table.Column<double>(type: "BINARY_DOUBLE", nullable: true),
                    data_envio = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    status = table.Column<string>(type: "NVARCHAR2(20)", maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OcorrenciaColaborativa", x => x.id_ocorrencia);
                    table.ForeignKey(
                        name: "FK_OcorrenciaColaborativa_Usuario_id_usuario",
                        column: x => x.id_usuario,
                        principalTable: "Usuario",
                        principalColumn: "id_usuario",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RotasSeguras",
                columns: table => new
                {
                    id_rota = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    id_usuario = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    id_abrigo_destino = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    origem_latitude = table.Column<double>(type: "BINARY_DOUBLE", nullable: true),
                    origem_longitude = table.Column<double>(type: "BINARY_DOUBLE", nullable: true),
                    destino_latitude = table.Column<double>(type: "BINARY_DOUBLE", nullable: true),
                    destino_longitude = table.Column<double>(type: "BINARY_DOUBLE", nullable: true),
                    tipo_rota = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RotasSeguras", x => x.id_rota);
                    table.ForeignKey(
                        name: "FK_RotasSeguras_Abrigo_id_abrigo_destino",
                        column: x => x.id_abrigo_destino,
                        principalTable: "Abrigo",
                        principalColumn: "id_abrigo",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RotasSeguras_Usuario_id_usuario",
                        column: x => x.id_usuario,
                        principalTable: "Usuario",
                        principalColumn: "id_usuario",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Alerta_id_usuario",
                table: "Alerta",
                column: "id_usuario");

            migrationBuilder.CreateIndex(
                name: "IX_LeituraSensor_id_sensor",
                table: "LeituraSensor",
                column: "id_sensor");

            migrationBuilder.CreateIndex(
                name: "IX_LocalizacaoUsuario_id_usuario",
                table: "LocalizacaoUsuario",
                column: "id_usuario");

            migrationBuilder.CreateIndex(
                name: "IX_OcorrenciaColaborativa_id_usuario",
                table: "OcorrenciaColaborativa",
                column: "id_usuario");

            migrationBuilder.CreateIndex(
                name: "IX_RotasSeguras_id_abrigo_destino",
                table: "RotasSeguras",
                column: "id_abrigo_destino");

            migrationBuilder.CreateIndex(
                name: "IX_RotasSeguras_id_usuario",
                table: "RotasSeguras",
                column: "id_usuario");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Alerta");

            migrationBuilder.DropTable(
                name: "LeituraSensor");

            migrationBuilder.DropTable(
                name: "LocalizacaoUsuario");

            migrationBuilder.DropTable(
                name: "OcorrenciaColaborativa");

            migrationBuilder.DropTable(
                name: "RotasSeguras");

            migrationBuilder.DropTable(
                name: "Sensor");

            migrationBuilder.DropTable(
                name: "Abrigo");

            migrationBuilder.DropTable(
                name: "Usuario");
        }
    }
}
