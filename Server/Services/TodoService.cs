using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Oqtane.Enums;
using Oqtane.Infrastructure;
using Oqtane.Models;
using Oqtane.Modules;
using Oqtane.Security;
using Oqtane.Shared;
using Trifoia.Module.Todo.Repository;


namespace Trifoia.Module.Todo.Services
{
    public class ServerTodoService : ITodoService, ITransientService
    {
        
        private readonly ITodoRepository _todoRepository;
        private readonly IUserPermissions _userPermissions;
        private readonly ILogManager _logger;
        private readonly IHttpContextAccessor _accessor;
        private readonly Alias _alias;

        public ServerTodoService(ITodoRepository todoRepository,
                                                IUserPermissions userPermissions,
                                                ITenantManager tenantManager,
                                                ILogManager logger,
                                                IHttpContextAccessor accessor)
        {
            _todoRepository = todoRepository;
            _userPermissions = userPermissions;
            _logger = logger;
            _accessor = accessor;
            _alias = tenantManager.GetAlias();
        }

        public async Task<List<Models.Todo>> GetTodosAsync(int moduleId)
        {

            if (_userPermissions.IsAuthorized(_accessor.HttpContext.User, _alias.SiteId, EntityNames.Module, moduleId, PermissionNames.View))
            {
                return (await _todoRepository.GetTodosAsync(moduleId)).ToList();
            }
            else
            {
                _logger.Log(LogLevel.Error, this, LogFunction.Security, "Unauthorized Todo Get Attempt {moduleId}", moduleId);
                return null;
            }

        }

        public async Task<Models.Todo> GetTodoAsync(int todoId, int moduleId)
        {

            if (_userPermissions.IsAuthorized(_accessor.HttpContext.User, _alias.SiteId, EntityNames.Module, moduleId, PermissionNames.View))
            {
                return await _todoRepository.GetTodoAsync(todoId);
            }
            else
            {
                _logger.Log(LogLevel.Error, this, LogFunction.Security, "Unauthorized Todo Get Attempt {todoId} {ModuleId}", todoId, moduleId);
                return null;
            }

        }

        public async Task<Models.Todo> AddTodoAsync(Models.Todo todo)
        {

            if (_userPermissions.IsAuthorized(_accessor.HttpContext.User, _alias.SiteId, EntityNames.Module, todo.ModuleId, PermissionNames.Edit))
            {
                todo = await _todoRepository.AddTodoAsync(todo);
                _logger.Log(LogLevel.Information, this, LogFunction.Create, "Todo Added {Todo}", todo);
            }
            else
            {
                _logger.Log(LogLevel.Error, this, LogFunction.Security, "Unauthorized Todo Add Attempt {Todo}", todo);
                todo = null;
            }
            return todo;

        }

        public async Task<Models.Todo> UpdateTodoAsync(Models.Todo todo)
        {

            if (_userPermissions.IsAuthorized(_accessor.HttpContext.User, _alias.SiteId, EntityNames.Module, todo.ModuleId, PermissionNames.Edit))
            {
                todo = await _todoRepository.UpdateTodoAsync(todo);
                _logger.Log(LogLevel.Information, this, LogFunction.Update, "Todo Updated {Todo}", todo);
            }
            else
            {
                _logger.Log(LogLevel.Error, this, LogFunction.Security, "Unauthorized Todo Update Attempt {Todo}", todo);
                todo = null;
            }
            return todo;

        }

        public async Task DeleteTodoAsync(int todoId, int moduleId)
        {

            if (_userPermissions.IsAuthorized(_accessor.HttpContext.User, _alias.SiteId, EntityNames.Module, moduleId, PermissionNames.Edit))
            {
                await _todoRepository.DeleteTodoAsync(todoId);
                _logger.Log(LogLevel.Information, this, LogFunction.Delete, "Todo Deleted {TodoId}", todoId);
            }
            else
            {
                _logger.Log(LogLevel.Error, this, LogFunction.Security, "Unauthorized Todo Delete Attempt {todoId} {ModuleId}", todoId, moduleId);
            }

        }

    }
}