using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;
using System.IO;
using Buisness.Repositories.Interfaces;
using Buisness.Models;

namespace TODO_APP.Repositories.XML
{
    public class XMLTodoReository : ITodoRepository
    {
        public string fileName = "Todos.xml";
        public string filePash = @"C:\Study\SIC\TODO_APP\TODO_APP\XMLData\Todos.xml";
        XmlSerializer xmlSerializer;

        public XMLTodoReository()
        {
            xmlSerializer = new XmlSerializer(typeof(List<TodoModel>));
        }

        public Task<TodoModel> CreateAsync(TodoModel todoModel)
        {
            List<TodoModel> todos;

            using (FileStream fs = new FileStream(filePash, FileMode.OpenOrCreate))
            {
                todos = (List<TodoModel>?)xmlSerializer.Deserialize(fs);
                if (todos == null)
                    todos = new List<TodoModel>();
            }

            todoModel.Id = todos.Last().Id + 1;

            todos.Add(todoModel);

            using (FileStream fs = new FileStream(filePash, FileMode.OpenOrCreate))
            {
                xmlSerializer.Serialize(fs, todos);
                return Task.FromResult(todoModel);
            }
        }

        public Task DeleteAsync(int id)
        {
            List<TodoModel> todos;
            TodoModel selectedTodo;

            using (FileStream fs = new FileStream(filePash, FileMode.OpenOrCreate, FileAccess.Read, FileShare.Read, 4096, true))
            {
                todos = (List<TodoModel>)xmlSerializer.Deserialize(fs);
                if (todos == null)
                    todos = new List<TodoModel>();

                selectedTodo = todos.SingleOrDefault(t => t.Id == id);
            }

            todos.Remove(selectedTodo);

            using (FileStream fs = new FileStream(filePash, FileMode.Truncate, FileAccess.Write, FileShare.Write, 4096, true))
            {
                xmlSerializer.Serialize(fs, todos);
            }
            return Task.FromResult(todos);
        }

        public Task<TodoModel> GetTodoByIdAsync(int id)
        {
            List<TodoModel> todos;

            using (FileStream fs = new FileStream(filePash, FileMode.OpenOrCreate, FileAccess.Read, FileShare.Read, 4096, true))
            {
                todos = (List<TodoModel>?)xmlSerializer.Deserialize(fs);
                if (todos == null)
                    todos = new List<TodoModel>();

                var resultTodo = todos.SingleOrDefault(t => t.Id == id);
                return Task.FromResult(resultTodo);
            }
        }

        public Task<IEnumerable<TodoModel>> GetTodoByCategoryAsync(int id)
        {
            IEnumerable<TodoModel> todos;

            using (FileStream fs = new FileStream(filePash, FileMode.OpenOrCreate, FileAccess.Read, FileShare.Read, 4096, true))
            {
                todos = (List<TodoModel>)xmlSerializer.Deserialize(fs);
                if (todos == null)
                    todos = new List<TodoModel>();

                todos = todos.Where(t => t.CategoryId == id); ;
                return Task.FromResult(todos);
            }
        }

        public Task<IEnumerable<TodoModel>> GetTodosAsync()
        {
            using (FileStream fs = new FileStream(filePash, FileMode.OpenOrCreate, FileAccess.Read, FileShare.Read, 4096, useAsync: true))
            {
                IEnumerable<TodoModel> todos = (List<TodoModel>?)xmlSerializer.Deserialize(fs);
                if (todos == null)
                    todos = new List<TodoModel>();
                return Task.FromResult(todos);
            }
        }

        public Task<IEnumerable<TodoModel>> GetTodosByPaginationAsync(int page, int pageSize)
        {
            using (FileStream fs = new FileStream(filePash, FileMode.OpenOrCreate, FileAccess.Read, FileShare.Read, 4096, useAsync: true))
            {
                IEnumerable<TodoModel> todos = (List<TodoModel>?)xmlSerializer.Deserialize(fs);
                if (todos == null)
                    todos = new List<TodoModel>();

                return Task.FromResult(todos.Skip(pageSize * page).Take(pageSize));
            }
        }

        public Task UpdateAsync(TodoModel todo)
        {
            List<TodoModel> todos;
            TodoModel selectedTodos;

            using (FileStream fs = new FileStream(filePash, FileMode.OpenOrCreate, FileAccess.Read, FileShare.Read, 4096, useAsync: true))
            {
                todos = (List<TodoModel>?)xmlSerializer.Deserialize(fs);
                if (todos == null)
                    todos = new List<TodoModel>();

                selectedTodos = todos.SingleOrDefault(t => t.Id == todo.Id);
            }

            using (FileStream fs = new FileStream(filePash, FileMode.Truncate, FileAccess.Write, FileShare.Write, 4096, true))
            {
                int categoryIndex = todos.FindIndex(c => c.Id == todo.Id);
                todos[categoryIndex] = todo;
                xmlSerializer.Serialize(fs, todos);
            }
            return Task.FromResult(todo);
        }
    }
}
