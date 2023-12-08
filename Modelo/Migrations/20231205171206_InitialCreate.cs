using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Modelo.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "productos",
                columns: table => new
                {
                    productoid = table.Column<Guid>(type: "uuid", nullable: false),
                    nombre = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    descripcion = table.Column<string>(type: "text", nullable: true),
                    precio = table.Column<decimal>(type: "numeric(10,2)", precision: 10, scale: 2, nullable: false),
                    stock = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("productos_pkey", x => x.productoid);
                });

            migrationBuilder.CreateTable(
                name: "usuarios",
                columns: table => new
                {
                    usuarioid = table.Column<Guid>(type: "uuid", nullable: false),
                    nombre = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    correoelectronico = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    contraseña = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    rol = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("usuarios_pkey", x => x.usuarioid);
                });

            migrationBuilder.CreateTable(
                name: "carritocompras",
                columns: table => new
                {
                    carritoid = table.Column<Guid>(type: "uuid", nullable: false),
                    usuarioid = table.Column<Guid>(type: "uuid", nullable: false),
                    productoid = table.Column<Guid>(type: "uuid", nullable: false),
                    cantidad = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("carritocompras_pkey", x => x.carritoid);
                    table.ForeignKey(
                        name: "carritocompras_productoid_fkey",
                        column: x => x.productoid,
                        principalTable: "productos",
                        principalColumn: "productoid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "carritocompras_usuarioid_fkey",
                        column: x => x.usuarioid,
                        principalTable: "usuarios",
                        principalColumn: "usuarioid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "pedidos",
                columns: table => new
                {
                    pedidoid = table.Column<Guid>(type: "uuid", nullable: false),
                    usuarioid = table.Column<Guid>(type: "uuid", nullable: false),
                    fechapedido = table.Column<DateTime>(type: "timestamp without time zone", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP"),
                    estado = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pedidos_pkey", x => x.pedidoid);
                    table.ForeignKey(
                        name: "pedidos_usuarioid_fkey",
                        column: x => x.usuarioid,
                        principalTable: "usuarios",
                        principalColumn: "usuarioid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "detallespedido",
                columns: table => new
                {
                    detallepedidoid = table.Column<Guid>(type: "uuid", nullable: false),
                    Pedidoid = table.Column<Guid>(type: "uuid", nullable: false),
                    Productoid = table.Column<Guid>(type: "uuid", nullable: false),
                    Cantidad = table.Column<int>(type: "integer", nullable: false),
                    Preciounitario = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("detallespedido_pkey", x => x.detallepedidoid);
                    table.ForeignKey(
                        name: "detallespedido_pedidoid_fkey",
                        column: x => x.Pedidoid,
                        principalTable: "pedidos",
                        principalColumn: "pedidoid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "detallespedido_productoid_fkey",
                        column: x => x.Productoid,
                        principalTable: "productos",
                        principalColumn: "productoid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "historialcambios",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    Pedidoid = table.Column<Guid>(type: "uuid", nullable: false),
                    Usuarioid = table.Column<Guid>(type: "uuid", nullable: false),
                    Motivo = table.Column<string>(type: "text", nullable: true),
                    Fechadecambio = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("historialcambios_pkey", x => x.id);
                    table.ForeignKey(
                        name: "historialcambios_pedidoid_fkey",
                        column: x => x.Pedidoid,
                        principalTable: "pedidos",
                        principalColumn: "pedidoid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "historialcambios_usuarioid_fkey",
                        column: x => x.Usuarioid,
                        principalTable: "usuarios",
                        principalColumn: "usuarioid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_carritocompras_productoid",
                table: "carritocompras",
                column: "productoid");

            migrationBuilder.CreateIndex(
                name: "IX_carritocompras_usuarioid",
                table: "carritocompras",
                column: "usuarioid");

            migrationBuilder.CreateIndex(
                name: "IX_detallespedido_Pedidoid",
                table: "detallespedido",
                column: "Pedidoid");

            migrationBuilder.CreateIndex(
                name: "IX_detallespedido_Productoid",
                table: "detallespedido",
                column: "Productoid");

            migrationBuilder.CreateIndex(
                name: "IX_historialcambios_Pedidoid",
                table: "historialcambios",
                column: "Pedidoid");

            migrationBuilder.CreateIndex(
                name: "IX_historialcambios_Usuarioid",
                table: "historialcambios",
                column: "Usuarioid");

            migrationBuilder.CreateIndex(
                name: "IX_pedidos_usuarioid",
                table: "pedidos",
                column: "usuarioid");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "carritocompras");

            migrationBuilder.DropTable(
                name: "detallespedido");

            migrationBuilder.DropTable(
                name: "historialcambios");

            migrationBuilder.DropTable(
                name: "productos");

            migrationBuilder.DropTable(
                name: "pedidos");

            migrationBuilder.DropTable(
                name: "usuarios");
        }
    }
}
