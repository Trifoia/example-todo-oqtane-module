using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Oqtane.Shared;
using Oqtane.Enums;
using Oqtane.Infrastructure;
using Oqtane.Controllers;
using System.Net;
using Trifoia.Module.Todo.Repository;


namespace Trifoia.Module.Todo.Controllers
{

    [Route(ControllerRoutes.ApiRoute)]
    public class TodoController : ModuleControllerBase
    {
        
        private readonly ITodoRepository _todoRepository;

        public TodoController(ITodoRepository todoRepository,
                                                ILogManager logger,
                                                IHttpContextAccessor accessor) : base(logger, accessor)
        {
            _todoRepository = todoRepository;
        }

        [HttpGet]
        [Authorize(Policy = PolicyNames.ViewModule)]
        public IEnumerable<Models.Todo> Get(string moduleId)
        {

            int ModuleId;
            if (int.TryParse(moduleId, out ModuleId) && IsAuthorizedEntityId(EntityNames.Module, ModuleId))
            {
                return _todoRepository.GetTodos(ModuleId);
            }
            else
            {
                _logger.Log(LogLevel.Error, this, LogFunction.Security, "Unauthorized Todo Get Attempt {ModuleId}", moduleId);
                HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                return null;
            }

        }

        [HttpGet("{id}")]
        [Authorize(Policy = PolicyNames.ViewModule)]
        public Models.Todo Get(int id)
        {

            Models.Todo todo = _todoRepository.GetTodo(id);
            if (todo != null && IsAuthorizedEntityId(EntityNames.Module, todo.ModuleId))
            {
                return todo;
            }
            else
            {
                _logger.Log(LogLevel.Error, this, LogFunction.Security, "Unauthorized Todo Get Attempt {TodoId}", id);
                HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                return null;
            }

        }

        [HttpPost]
        [Authorize(Policy = PolicyNames.EditModule)]
        public Models.Todo Post([FromBody] Models.Todo todo)
        {

            if (ModelState.IsValid && IsAuthorizedEntityId(EntityNames.Module, todo.ModuleId))
            {
                todo = _todoRepository.AddTodo(todo);
                _logger.Log(LogLevel.Information, this, LogFunction.Create, "Todo Added {Todo}", todo);
            }
            else
            {
                _logger.Log(LogLevel.Error, this, LogFunction.Security, "Unauthorized Todo Post Attempt {Todo}", todo);
                HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                todo = null;
            }
            return todo;

        }

        [HttpPut("{id}")]
        public Models.Todo Put(int id, [FromBody] Models.Todo todo)
        {

            if (ModelState.IsValid && todo.TodoId == id && IsAuthorizedEntityId(EntityNames.Module, todo.ModuleId) && _todoRepository.GetTodo(todo.TodoId, false) != null)
            {
                todo = _todoRepository.UpdateTodo(todo);
                _logger.Log(LogLevel.Information, this, LogFunction.Update, "Todo Updated {Todo}", todo);
            }
            else
            {
                _logger.Log(LogLevel.Error, this, LogFunction.Security, "Unauthorized Todo Put Attempt {Todo}", todo);
                HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                todo = null;
            }
            return todo;

        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            Models.Todo todo = _todoRepository.GetTodo(id);
            if (todo != null && IsAuthorizedEntityId(EntityNames.Module, todo.ModuleId))
            {
                _todoRepository.DeleteTodo(id);
                _logger.Log(LogLevel.Information, this, LogFunction.Delete, "Todo Deleted {todoId}", id);
            }
            else
            {
                _logger.Log(LogLevel.Error, this, LogFunction.Security, "Unauthorized Todo Delete Attempt {TodoId}", id);
                HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
            }
        }

    }
}