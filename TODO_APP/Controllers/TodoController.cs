using Buisness.Models;
using Buisness.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TODO_APP.Repositories;
using TODO_APP.Repositories.Infrastructure;
using TODO_APP.ViewModels;


namespace TODO_APP.Controllers
{
    public class TodoController : Controller
    {
        CategoryReslover categoriesResolver;
        TodoReslover todoResolver;
        ICategoriesPerository categoriesRepository;
        ITodoRepository todoRepository;

        public TodoController(CategoryReslover c, TodoReslover t)
        {
            todoResolver = t;
            categoriesResolver = c;
        }
        public async Task<IActionResult> Index()
        {
            string dataSource = HttpContext.Session.GetJson<string>("DataSource") ?? "MsSql";
            ChangeRepositoriesSources(dataSource);

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
            string dataSource = HttpContext.Session.GetJson<string>("DataSource") ?? "MsSql";
            ChangeRepositoriesSources(dataSource);

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
            string dataSource = HttpContext.Session.GetJson<string>("DataSource") ?? "MsSql";
            ChangeRepositoriesSources(dataSource);

            var categories = await categoriesRepository.GetCategoriesAsync();
            HttpContext.Session.SetJson("DataSource", dataSource);
            return View(new TodoCreateFormViewModel { CategoryModels = categories.ToList() });
        }

        [HttpPost]
        public async Task<IActionResult> Create(TodoCreateFormViewModel todo)
        {
            string dataSource = HttpContext.Session.GetJson<string>("DataSource") ?? "MsSql";
            ChangeRepositoriesSources(dataSource);

            if (ModelState.IsValid)
            {
                await todoRepository.CreateAsync(todo.TodoModel);
            }
            return RedirectToAction("Index");
        }

        public IActionResult Update() => View();

        public async Task<IActionResult> Delete(int id)
        {
            string dataSource = HttpContext.Session.GetJson<string>("DataSource") ?? "MsSql";
            ChangeRepositoriesSources(dataSource);

            var todo = await todoRepository.GetTodoByIdAsync(id);
            if (todo == null) return NotFound();
            await todoRepository.DeleteAsync(id);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int id)
        {
            string dataSource = HttpContext.Session.GetJson<string>("DataSource") ?? "MsSql";
            ChangeRepositoriesSources(dataSource);

            var todo = await todoRepository.GetTodoByIdAsync(id);
            if (todo == null) return NotFound();

            var categories = await categoriesRepository.GetCategoriesAsync();
            return View(new TodoEditViewModel { TodoModel = todo, CategoryModels = categories.ToList() });
        }

        [HttpPost]
        public async Task<IActionResult> Edit(TodoEditViewModel todoEditVM)
        {
            string dataSource = HttpContext.Session.GetJson<string>("DataSource") ?? "MsSql";
            ChangeRepositoriesSources(dataSource);

            if (!ModelState.IsValid) return View(todoEditVM);

            var t = await todoRepository.GetTodoByIdAsync(todoEditVM.TodoModel.Id);

            if (t == null)
            {
                ModelState.AddModelError("", "Todo not found!");
                return RedirectToAction("Index");
            }

            await todoRepository.UpdateAsync(todoEditVM.TodoModel);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> ToggleTodoIsDone(int id)
        {
            string dataSource = HttpContext.Session.GetJson<string>("DataSource") ?? "MsSql";
            ChangeRepositoriesSources(dataSource);

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

        private void ChangeRepositoriesSources(string source)
        {
            categoriesRepository = categoriesResolver(source);
            todoRepository = todoResolver(source);
        }

        public IActionResult ChangeDataSource(string source)
        {
            if (source == "XML")
            {
                HttpContext.Session.SetJson("DataSource", "XML");
                return RedirectToAction("Index");
            }

            if (source == "MsSql")
            {
                HttpContext.Session.SetJson("DataSource", "MsSql");
                return RedirectToAction("Index");
            }

            return BadRequest();
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
