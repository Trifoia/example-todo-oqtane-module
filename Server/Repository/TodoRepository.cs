using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Collections.Generic;
using Oqtane.Modules;
using System.Threading.Tasks;

namespace Trifoia.Module.Todo.Repository
{
    public class TodoRepository : ITodoRepository, ITransientService
    {
        
        private readonly IDbContextFactory<TodoContext> _factory;

        public TodoRepository(IDbContextFactory<TodoContext> factory)
        {
            _factory = factory;
        }

        public IEnumerable<Models.Todo> GetTodos(int moduleId)
        {
            using var db = _factory.CreateDbContext();
            return db.Todo.Where(item => item.ModuleId == moduleId).ToList();
        }

        public Models.Todo GetTodo(int todoId)
        {
            return GetTodo(todoId, true);
        }

        public Models.Todo GetTodo(int todoId, bool tracking)
        {
            using var db = _factory.CreateDbContext();
            if (tracking)
            {
                return db.Todo.Find(todoId);
            }
            else
            {
                return db.Todo.AsNoTracking().FirstOrDefault(item => item.TodoId == todoId);
            }
        }

        public Models.Todo AddTodo(Models.Todo todo)
        {
            using var db = _factory.CreateDbContext();
            db.Todo.Add(todo);
            db.SaveChanges();
            return todo;
        }

        public Models.Todo UpdateTodo(Models.Todo todo)
        {
            using var db = _factory.CreateDbContext();
            db.Entry(todo).State = EntityState.Modified;
            db.SaveChanges();
            return todo;
        }

        public void DeleteTodo(int todoId)
        {
            using var db = _factory.CreateDbContext();
            Models.Todo todo = db.Todo.Find(todoId);
            db.Todo.Remove(todo);
            db.SaveChanges();
        }

        public async Task<IEnumerable<Models.Todo>> GetTodosAsync(int moduleId)
        {
            using var db = _factory.CreateDbContext();
            return await db.Todo.Where(item => item.ModuleId == moduleId).ToListAsync();
        }

        public async Task<Models.Todo> GetTodoAsync(int todoId)
        {
            return await GetTodoAsync(todoId, true);
        }

        public async Task<Models.Todo> GetTodoAsync(int todoId, bool tracking)
        {
            using var db = _factory.CreateDbContext();
            if (tracking)
            {
                return await db.Todo.FindAsync(todoId);
            }
            else
            {
                return await db.Todo.AsNoTracking().FirstOrDefaultAsync(item => item.TodoId == todoId);
            }
        }

        public async Task<Models.Todo> AddTodoAsync(Models.Todo todo)
        {
            using var db = _factory.CreateDbContext();
            db.Todo.Add(todo);
            await db.SaveChangesAsync();
            return todo;
        }

        public async Task<Models.Todo> UpdateTodoAsync(Models.Todo todo)
        {
            using var db = _factory.CreateDbContext();
            db.Entry(todo).State = EntityState.Modified;
            await db.SaveChangesAsync();
            return todo;
        }

        public async Task DeleteTodoAsync(int todoId)
        {
            using var db = _factory.CreateDbContext();
            Models.Todo todo = await db.Todo.FindAsync(todoId);
            db.Todo.Remove(todo);
            await db.SaveChangesAsync();
        }

    }
}