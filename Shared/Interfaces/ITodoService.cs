using System.Collections.Generic;
using System.Threading.Tasks;

namespace Trifoia.Module.Todo.Services
{
    public interface ITodoService
    {

        Task<List<Models.Todo>> GetTodosAsync(int moduleId);

        Task<Models.Todo> GetTodoAsync(int TodoId, int moduleId);

        Task<Models.Todo> AddTodoAsync(Models.Todo todo);

        Task<Models.Todo> UpdateTodoAsync(Models.Todo todo);

        Task DeleteTodoAsync(int TodoId, int moduleId);

    }
}