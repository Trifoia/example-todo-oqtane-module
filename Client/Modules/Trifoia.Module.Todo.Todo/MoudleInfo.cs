using Oqtane.Models;
using Oqtane.Modules;

namespace Trifoia.Module.Todo.Todo
{

    public class ModuleInfo : IModule
    {
        public ModuleDefinition ModuleDefinition => new ModuleDefinition
        {
        
            Name = "TodoExample Todo",
            Description = "This is an example of an Oqtane Module using Amazing Oqtane Studio",
            Version = "1.0.0",
            ServerManagerType = "Trifoia.Module.Todo.Manager.TodoManager, Trifoia.Module.Todo.Server.Oqtane",
            ReleaseVersions = "1.0.0",
            Dependencies = "Trifoia.Module.Todo.Shared.Oqtane",
            PackageName = "Trifoia.Module.Todo"

        };
    }
}