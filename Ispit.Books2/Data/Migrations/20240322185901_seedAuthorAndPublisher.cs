using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ispit.Books2.Data.Migrations
{
    /// <inheritdoc />
    public partial class seedAuthorAndPublisher : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
           table: "Authors",
           columns: new[] { "AuthorId", "Name" },
           values: new object[,]
           {
            { 1, "J.K. Rowling" },
            { 2, "George R.R. Martin" },
            { 3, "Agatha Christie" },
            { 4, "Stephen King" },
            { 5, "Jane Austen" }
           });

            migrationBuilder.InsertData(
                table: "Publishers",
                columns: new[] { "PublisherId", "Name" },
                values: new object[,]
                {
            { 1, "Penguin Random House" },
            { 2, "HarperCollins" },
            { 3, "Simon & Schuster" }
                });

            // You can also seed Books here if necessary
        }
    

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
