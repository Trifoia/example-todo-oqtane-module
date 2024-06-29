using System.Collections.Generic;
using System.Threading.Tasks;

namespace Trifoia.Module.Todo.Repository
{
    public interface ITodoRepository
    {
        IEnumerable<Models.Todo> GetTodos(int ModuleId);
        Models.Todo GetTodo(int TodoId);
        Models.Todo GetTodo(int TodoId, bool tracking);
        Models.Todo AddTodo(Models.Todo Todo);
        Models.Todo UpdateTodo(Models.Todo Todo);
        void DeleteTodo(int TodoId);

        Task<IEnumerable<Models.Todo>> GetTodosAsync(int ModuleId);
        Task<Models.Todo> GetTodoAsync(int TodoId);
        Task<Models.Todo> GetTodoAsync(int TodoId, bool tracking);
        Task<Models.Todo> AddTodoAsync(Models.Todo Todo);
        Task<Models.Todo> UpdateTodoAsync(Models.Todo Todo);
        Task DeleteTodoAsync(int TodoId);
    }
}
