using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repository.Migrations
{
    /// <inheritdoc />
    public partial class mig3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tags_Blogs_BlogId",
                table: "Tags");

            migrationBuilder.DropIndex(
                name: "IX_Tags_BlogId",
                table: "Tags");

            migrationBuilder.DropColumn(
                name: "BlogId",
                table: "Tags");

            migrationBuilder.CreateTable(
                name: "BlogTag",
                columns: table => new
                {
                    BlogsBlogId = table.Column<int>(type: "int", nullable: false),
                    TagsTagId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlogTag", x => new { x.BlogsBlogId, x.TagsTagId });
                    table.ForeignKey(
                        name: "FK_BlogTag_Blogs_BlogsBlogId",
                        column: x => x.BlogsBlogId,
                        principalTable: "Blogs",
                        principalColumn: "BlogId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BlogTag_Tags_TagsTagId",
                        column: x => x.TagsTagId,
                        principalTable: "Tags",
                        principalColumn: "TagId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "Email", "Image", "Password", "Roles", "UserName" },
                values: new object[] { 1, "texast5@gmail.com", "", "123456", 0, "TEXAST5" });

            migrationBuilder.InsertData(
                table: "Blogs",
                columns: new[] { "BlogId", "BlogDescription", "BlogShortDescription", "BlogTitle", "Image", "UserId" },
                values: new object[] { 1, "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Vivamus sapien sem, efficitur ut vulputate eu, efficitur eu turpis. Phasellus vel imperdiet leo. Suspendisse eu iaculis justo. Vestibulum consequat tempus ex at pulvinar. Pellentesque semper arcu nibh, id laoreet libero hendrerit vitae. Cras non luctus velit, ac rutrum odio. In mattis lobortis risus, ac vulputate urna tincidunt malesuada. Sed consequat sem sed lacus malesuada facilisis. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Curabitur arcu ligula, lobortis sed quam ac, dignissim interdum enim.", "This a first blog", "First blog", "", 1 });

            migrationBuilder.CreateIndex(
                name: "IX_BlogTag_TagsTagId",
                table: "BlogTag",
                column: "TagsTagId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BlogTag");

            migrationBuilder.DeleteData(
                table: "Blogs",
                keyColumn: "BlogId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1);

            migrationBuilder.AddColumn<int>(
                name: "BlogId",
                table: "Tags",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tags_BlogId",
                table: "Tags",
                column: "BlogId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tags_Blogs_BlogId",
                table: "Tags",
                column: "BlogId",
                principalTable: "Blogs",
                principalColumn: "BlogId");
        }
    }
}
