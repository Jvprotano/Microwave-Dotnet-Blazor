using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Microwave.Api.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PredefinedProgram",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Food = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    TimeSeconds = table.Column<int>(type: "int", nullable: false),
                    Power = table.Column<int>(type: "int", nullable: false),
                    Instructions = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    LabelHeating = table.Column<string>(type: "CHAR(1)", maxLength: 1, nullable: false),
                    IsPredefined = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PredefinedProgram", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "PredefinedProgram",
                columns: new[] { "Id", "Name", "Food", "TimeSeconds", "Power", "LabelHeating", "Instructions", "IsPredefined", "CreatedAt", "UpdatedAt" },
                values: new object[,]
                {
                    { Guid.NewGuid(), "Pipoca", "Pipoca (de micro-ondas)", 180, 7, "P", @"Observar o barulho de estouros do milho, caso houver um intervalo de mais de 10 segundos entre um estouro e outro, interrompa o aquecimento.", true, DateTime.Now, DateTime.Now  },
                    { Guid.NewGuid(), "Leite", "Leite", 300, 5, "L", @"Cuidado com aquecimento de líquidos, o choque térmico aliado ao movimento do recipiente pode causar fervura imediata causando risco de queimaduras.", true, DateTime.Now, DateTime.Now },
                    { Guid.NewGuid(), "Carnes de boi", "Carne em pedaço ou fatias", 840, 4, "C", @"Interrompa o processo na metade e vire o conteúdo com a parte de baixo para cima para o descongelamento uniforme.", true, DateTime.Now, DateTime.Now },
                    { Guid.NewGuid(), "Frango", "Frango (qualquer corte)", 480, 7, "F", @"Interrompa o processo na metade e vire o conteúdo com a parte de baixo para cima para o descongelamento uniforme.", true, DateTime.Now, DateTime.Now },
                    { Guid.NewGuid(), "Feijão", "Feijão congelado", 480, 9, "B", @"Deixe o recipiente destampado e em casos de plástico, cuidado ao retirar o recipiente pois o mesmo pode perder resistência em altas temperaturas.", true, DateTime.Now, DateTime.Now }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PredefinedProgram");
        }
    }
}
