using Buisness.Models;
using Buisness.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TODO_APP.Infrastructure;
using TODO_APP.Repositories;
using TODO_APP.Repositories.Infrastructure;
using TODO_APP.ViewModels;

namespace TODO_APP.Controllers
{
    public class TodoController : Controller
    {
        ICategoriesRerository categoriesRepository;
        ITodoRepository todoRepository;
        DataSource dataSource;
        public TodoController(RepositoryResolver repositoryReslover, IHttpContextAccessor httpContextAccesor)
        {
            string dataSourceStr = httpContextAccesor.HttpContext.Request.Cookies["DataSource"];
            if (Enum.TryParse(dataSourceStr, out dataSource))
            {
                categoriesRepository = repositoryReslover.ResolveCategoryRepository(dataSource);
                todoRepository = repositoryReslover.ResolveTodoRepository(dataSource);
            }
            else
            {
                categoriesRepository = repositoryReslover.ResolveCategoryRepository(DataSource.MsSql);
                todoRepository = repositoryReslover.ResolveTodoRepository(DataSource.MsSql);
            }
        }
        public async Task<IActionResult> Index()
        {
            var categories = await categoriesRepository.GetCategoriesAsync();
            var todos = await todoRepository.GetTodosAsync();

            var sortedTodosIndexViewModels = MapTodosAndCategoriesToIndexTodoViewModels(todos, categories)
                .OrderBy(n => n.IsDone).ThenBy(n => n.Deadline == null).ThenBy(n => n.Deadline);

            var indexPageViewModel = new IndexPageViewModel(sortedTodosIndexViewModels, categories);
            
            HttpContext.Session.SetJson("DataSource", dataSource);
            return View(indexPageViewModel);
        }

        public async Task<IActionResult> TodoByCategory(int id)
        {
            var todos = await todoRepository.GetTodoByCategoryAsync(id);
            var categories = await categoriesRepository.GetCategoriesAsync();

            var sortedTodosIndexViewModels = MapTodosAndCategoriesToIndexTodoViewModels(todos, categories)
                .OrderBy(n => n.IsDone).ThenBy(n => n.Deadline == null).ThenBy(n => n.Deadline);

            var indexPageViewModel = new IndexPageViewModel(sortedTodosIndexViewModels, categories);
            
            HttpContext.Session.SetJson("DataSource", dataSource);
            return View("Index", indexPageViewModel);
        }
        public async Task<IActionResult> Create()
        {
            var categories = await categoriesRepository.GetCategoriesAsync();
            HttpContext.Session.SetJson("DataSource", dataSource);
            return View(new TodoCreateFormViewModel { CategoryModels = categories.ToList() });
        }

        [HttpPost]
        public async Task<IActionResult> Create(TodoCreateFormViewModel todo)
        {
            if (ModelState.IsValid)
            {
                await todoRepository.CreateAsync(todo.TodoModel);
            }
            return RedirectToAction("Index");
        }

        public IActionResult Update() => View();

        public async Task<IActionResult> Delete(int id)
        {
            var todo = await todoRepository.GetTodoByIdAsync(id);
            if (todo == null) return NotFound();
            await todoRepository.DeleteAsync(id);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int id)
        {
            var todo = await todoRepository.GetTodoByIdAsync(id);
            if (todo == null) return NotFound();

            var categories = await categoriesRepository.GetCategoriesAsync();
            return View(new TodoEditViewModel { TodoModel = todo, CategoryModels = categories.ToList() });
        }

        [HttpPost]
        public async Task<IActionResult> Edit(TodoEditViewModel todoEditVM)
        {
            if (!ModelState.IsValid) return View(todoEditVM);

            var todo = await todoRepository.GetTodoByIdAsync(todoEditVM.TodoModel.Id);

            if (todo == null)
            {
                ModelState.AddModelError("", "Todo not found!");
                return RedirectToAction("Index");
            }

            await todoRepository.UpdateAsync(todoEditVM.TodoModel);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> ToggleTodoIsDone(int id)
        {
            var todo = await todoRepository.GetTodoByIdAsync(id);

            if (todo != null)
            {
                todo.IsDone = !todo.IsDone;
                await todoRepository.UpdateAsync(todo);
            }
            else
            {
                ModelState.AddModelError("", "Todo not found!");
            }

            return RedirectToAction("Index");
        }

        private IEnumerable<IndexTodoViewModel> MapTodosAndCategoriesToIndexTodoViewModels(IEnumerable<TodoModel> todos, IEnumerable<CategoryModel> categories)
        {
            var indexTodos = new List<IndexTodoViewModel>();

            foreach (TodoModel t in todos)
            {
                var item = new IndexTodoViewModel(t);

                foreach (CategoryModel c in categories)
                {
                    if (t.CategoryId == c.Id)
                    {
                        item.Category = c.Name;
                        break;
                    }
                }
                indexTodos.Add(item);
            }
            return indexTodos;
        }
    }
}
