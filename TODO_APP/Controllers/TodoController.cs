using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TODO_APP.Models;
using TODO_APP.Repositories;
using TODO_APP.ViewModels;


namespace TODO_APP.Controllers
{
    public class TodoController : Controller
    {
        ITodoRepository todoRepo;
        ICategoriesPerository categoriesRepo;

        public TodoController(ICategoriesPerository c, ITodoRepository r)
        {
            todoRepo = r;
            categoriesRepo = c;
        }
        public async Task<IActionResult> Index()
        {
            var categories = await categoriesRepo.GetCategoriesAsync();
            var m = new TodoCreateFormViewModel { categoryModel = categories };
            return View(m);
        }


        public async Task<IActionResult> List()
        {
            var todos = await todoRepo.GetTodosAsync();
            var sortedTodo = todos.OrderBy(n => n.isDone).ThenBy(n => n.Deadline == null).ThenBy(n => n.Deadline);
            var categories = await categoriesRepo.GetCategoriesAsync();

            List<IndexTodoViewModel> indexTodos = new List<IndexTodoViewModel>();

            foreach (TodoModel t in sortedTodo)
            {
                IndexTodoViewModel item = new IndexTodoViewModel { todoModel = t };

                foreach (CategoryModel c in categories)
                {
                    if (t.CategoryId == c.id)
                    {
                        item.categoryModel = c;
                        break;
                    }
                }

                indexTodos.Add(item);
            }
            return View(indexTodos);
        }

        public async Task<IActionResult> TodoByCategory(int id)
        {
            var todos = await todoRepo.GetTodoByCategoryAsync(id);
            var sortedTodo = todos.OrderBy(n => n.Deadline == null).ThenBy(n => n.Deadline);
            var itvmList = new List<IndexTodoViewModel>();
            var category = await categoriesRepo.GetAsync(id);

            foreach (TodoModel t in sortedTodo)
            {
                itvmList.Add(new IndexTodoViewModel { todoModel = t, categoryModel = category });
            }

            return View("List", itvmList);
        }
        public async Task<IActionResult> Create()
        {
            var categories = await categoriesRepo.GetCategoriesAsync();
            var m = new TodoCreateFormViewModel { categoryModel = categories };
            return View(m);
        }

        [HttpPost]
        public async Task<IActionResult> Create(TodoCreateFormViewModel todo)
        {
            if (ModelState.IsValid)
            {
                await todoRepo.CreateAsync(todo.todoModel);
            }

            return RedirectToAction("List");
        }

        public IActionResult Update() => View();

        public async Task<IActionResult> Delete(int id)
        {
            var todo = await todoRepo.GetAsync(id);
            if (todo == null) return NotFound();
            await todoRepo.DeleteAsync(id);

            return RedirectToAction("List");
        }

        public async Task<IActionResult> Edit(int id)
        {
            var todo = await todoRepo.GetAsync(id);
            if (todo == null) return NotFound();
            var categories = await categoriesRepo.GetCategoriesAsync();
            return View(new TodoEditViewModel { todoModel = todo, categoryModels = categories });
        }

        [HttpPost]
        public async Task<IActionResult> Edit(TodoEditViewModel todoEditVM)
        {
            if (!ModelState.IsValid) return View(todoEditVM);

            var t = await todoRepo.GetAsync(todoEditVM.todoModel.id);

            if (t == null) 
            {
                ModelState.AddModelError("", "Todo not found!");
                return RedirectToAction("List");
            }
            
            await todoRepo.UpdateAsync(todoEditVM.todoModel);
            return RedirectToAction("List");
        }

        public async Task<IActionResult> ToggleTodoIsDone(int id)
        {
            var todo = await todoRepo.GetAsync(id);

            if (todo != null)
            {
                todo.isDone = !todo.isDone;
                await todoRepo.UpdateAsync(todo);
            }
            else
            {
                ModelState.AddModelError("", "Todo not found!");
            }

            return RedirectToAction("List");
        }
    }
}
