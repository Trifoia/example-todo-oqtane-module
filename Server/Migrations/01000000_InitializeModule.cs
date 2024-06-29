using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Oqtane.Databases.Interfaces;
using Oqtane.Migrations;
using Trifoia.Module.Todo.Migrations.EntityBuilders;
using Trifoia.Module.Todo.Repository;

namespace Trifoia.Module.Todo.Migrations
{
    [DbContext(typeof(TodoContext))]
    [Migration("Trifoia.Module.Todo.01.00.00.00")]
    public class InitializeModule : MultiDatabaseMigration
    {

        public InitializeModule(IDatabase database) : base(database)
        {
        }

        protected override void Up(MigrationBuilder migrationBuilder)
        {

            var TodoEntityBuilder = new TodoEntityBuilder(migrationBuilder, ActiveDatabase);
            TodoEntityBuilder.Create();

            var todoEntityBuilder = new TodoEntityBuilder(migrationBuilder, ActiveDatabase);
            todoEntityBuilder.Create();
        
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

            var TodoEntityBuilder = new TodoEntityBuilder(migrationBuilder, ActiveDatabase);
            TodoEntityBuilder.Create();

            var todoEntityBuilder = new TodoEntityBuilder(migrationBuilder, ActiveDatabase);
            todoEntityBuilder.Drop();
        
        }
    }
}