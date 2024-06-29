using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Microsoft.EntityFrameworkCore.Migrations.Operations.Builders;
using Oqtane.Databases.Interfaces;
using Oqtane.Migrations;
using Oqtane.Migrations.EntityBuilders;

namespace Trifoia.Module.Todo.Migrations.EntityBuilders
{
    public class TodoEntityBuilder : AuditableBaseEntityBuilder<TodoEntityBuilder>
    {

        private const string _entityTabelName = "Todo";
        private readonly PrimaryKey<TodoEntityBuilder> _primaryKey = new("PK_TrifoiaTodoTodo", x => x.TodoId);
        private readonly ForeignKey<TodoEntityBuilder> _moduleForeignKey = new("FK_TrifoiaTodoTodo_Module", x => x.ModuleId, "Module", "ModuleId", ReferentialAction.Cascade);

        public TodoEntityBuilder(MigrationBuilder migrationBuilder, IDatabase database) : base(migrationBuilder, database)
        {
            EntityTableName = _entityTabelName;
            PrimaryKey = _primaryKey;
            ForeignKeys.Add(_moduleForeignKey);
        }

        protected override TodoEntityBuilder BuildTable(ColumnsBuilder table)
        {

            TodoId = AddAutoIncrementColumn(table, "TodoId");
            ModuleId = AddIntegerColumn(table,"ModuleId");
            Topic = AddMaxStringColumn(table, "Topic", false, true);
            Done = AddBooleanColumn(table, "Done", false, false);

            AddAuditableColumns(table);

            return this;

        }

        public OperationBuilder<AddColumnOperation> TodoId { get; set; }
        public OperationBuilder<AddColumnOperation> ModuleId { get; set; }
        public OperationBuilder<AddColumnOperation> Topic { get; set; }
        public OperationBuilder<AddColumnOperation> Done { get; set; }


    }
}