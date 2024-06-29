using System.Collections.Generic;
using System.Threading.Tasks;

namespace Trifoia.Module.Todo.Repository
{
    public interface ITodoRepository
    {

        IEnumerable<Models.Todo> GetTodos(int moduleId);
        Models.Todo GetTodo(int todoId);
        Models.Todo GetTodo(int todoId, bool tracking);
        Models.Todo AddTodo(Models.Todo todo);
        Models.Todo UpdateTodo(Models.Todo todo);
        void DeleteTodo(int todoId);

        Task<IEnumerable<Models.Todo>> GetTodosAsync(int moduleId);
        Task<Models.Todo> GetTodoAsync(int todoId);
        Task<Models.Todo> GetTodoAsync(int todoId, bool tracking);
        Task<Models.Todo> AddTodoAsync(Models.Todo todo);
        Task<Models.Todo> UpdateTodoAsync(Models.Todo todo);
        Task DeleteTodoAsync(int todoId);

    }
}