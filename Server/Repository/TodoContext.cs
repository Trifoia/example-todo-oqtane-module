using Microsoft.EntityFrameworkCore;
using Oqtane.Modules;
using Oqtane.Repository;
using Oqtane.Repository.Databases.Interfaces;

namespace Trifoia.Module.Todo.Repository
{
    public class TodoContext : DBContextBase, ITransientService, IMultiDatabase
    {

        public virtual DbSet<Models.Todo> Todo{ get; set; }
        
        public virtual DbSet<Models.Todo> Todo { get; set; }

        public TodoContext(IDBContextDependencies DBContextDependencies) : base(DBContextDependencies)
        {
            // ContextBase handles multi-tenant database connections
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {

            base.OnModelCreating(builder);

            builder.Entity<Models.Todo>().ToTable(ActiveDatabase.RewriteName("AmazingTodo"));
            builder.Entity<Models.Todo>().ToTable(ActiveDatabase.RewriteName("Todo"));

        }
    }
}