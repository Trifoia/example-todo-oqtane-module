using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Oqtane.Modules;
using Oqtane.Services;
using Oqtane.Shared;


namespace Trifoia.Module.Todo.Services
{

    public class TodoService : ServiceBase, ITodoService, IService
    {
        
        public TodoService(IHttpClientFactory http, SiteState siteState) : base(http, siteState) { }

        private string Apiurl => CreateApiUrl("Todo");

        public async Task<List<Models.Todo>> GetTodosAsync(int moduleId)
        {
            List<Models.Todo> todos = await GetJsonAsync<List<Models.Todo>>(CreateAuthorizationPolicyUrl($"{Apiurl}?moduleid={moduleId}", EntityNames.Module, moduleId), Enumerable.Empty<Models.Todo>().ToList());
            return todos.OrderBy(item => item.Topic).ToList();
        }

        public async Task<Models.Todo> GetTodoAsync(int todoId, int moduleId)
        {
            return await GetJsonAsync<Models.Todo>(CreateAuthorizationPolicyUrl($"{Apiurl}/{todoId}", EntityNames.Module, moduleId));
        }

        public async Task<Models.Todo> AddTodoAsync(Models.Todo todo)
        {
            return await PostJsonAsync<Models.Todo>(CreateAuthorizationPolicyUrl($"{Apiurl}", EntityNames.Module, todo.ModuleId), todo);
        }

        public async Task<Models.Todo> UpdateTodoAsync(Models.Todo todo)
        {
            return await PutJsonAsync<Models.Todo>(CreateAuthorizationPolicyUrl($"{Apiurl}/{todo.TodoId}", EntityNames.Module, todo.ModuleId), todo);
        }

        public async Task DeleteTodoAsync(int todoId, int moduleId)
        {
            await DeleteAsync(CreateAuthorizationPolicyUrl($"{Apiurl}/{todoId}", EntityNames.Module, moduleId));
        }

    }
}