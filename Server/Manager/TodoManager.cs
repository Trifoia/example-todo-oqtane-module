using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using Oqtane.Modules;
using Oqtane.Models;
using Oqtane.Infrastructure;
using Oqtane.Enums;
using Oqtane.Repository;
using Trifoia.Module.Todo.Repository;

namespace Trifoia.Module.Todo.Manager
{

    public class TodoManager : MigratableModuleBase, IInstallable, IPortable
    {
        
        private readonly ITodoRepository _todoRepository;
        private readonly IDBContextDependencies _dbContextDependencies;

        public TodoManager(ITodoRepository todoRepository, IDBContextDependencies dbContextDependencies)
        {
            _todoRepository = todoRepository;
            _dbContextDependencies = dbContextDependencies;
        }

        public bool Install(Tenant tenant, string version)
        {
            return Migrate(new TodoContext(_dbContextDependencies), tenant, MigrationType.Up);
        }

        public bool Uninstall(Tenant tenant)
        {
            return Migrate(new TodoContext(_dbContextDependencies), tenant, MigrationType.Down);
        }

        public string ExportModule(Oqtane.Models.Module module)
        {

            string content = "";
            List<Models.Todo> todos = _todoRepository.GetTodos(module.ModuleId).ToList();
            if (todos != null)
            {
                content = JsonSerializer.Serialize(todos);
            }
            return content;

        }

        public void ImportModule(Oqtane.Models.Module module, string content, string version)
        {
            List<Models.Todo> todos = null;
            if (!string.IsNullOrEmpty(content))
            {
                todos = JsonSerializer.Deserialize<List<Models.Todo>>(content);
            }
            if (todos != null)
            {
                foreach(var todo in todos)
                {
                    _todoRepository.AddTodo(new Models.Todo {
                        ModuleId = module.ModuleId,
                        Topic = todo.Topic,
                        Done = todo.Done
                    });
                }
            }
        }

    }
}