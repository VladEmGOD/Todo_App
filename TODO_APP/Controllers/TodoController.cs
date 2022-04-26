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
            var todos = await todoRepo.GetTodosAsync();
            var categories = await categoriesRepo.GetCategoriesAsync();

            List<IndexTodoViewModel> indexTodos = new List<IndexTodoViewModel>();

            foreach (TodoModel t in todos)
            {
                IndexTodoViewModel item = new IndexTodoViewModel();

                item.todoModel = t;

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

            List<IndexTodoViewModel> itvmList = new List<IndexTodoViewModel>();
            var category = await categoriesRepo.GetAsync(id);

            foreach (TodoModel t in todos)
            {
                itvmList.Add(new IndexTodoViewModel { todoModel = t, categoryModel = category });
            }

            return View("Index", itvmList);
        }
        public async Task<IActionResult> Create() 
        {
            var categories = await categoriesRepo.GetCategoriesAsync();
            TodoCreateFormViewModel m = new TodoCreateFormViewModel();
            m.categoryModel = categories;
            return View(m);
        }

        [HttpPost]
        public async Task<IActionResult> Create(TodoCreateFormViewModel todoC)
        {
            if (ModelState.IsValid)
            {
                var todo = new TodoModel
                {
                    isDone = false,
                    Tittle = todoC.todoModel.Tittle,
                    DescriptionT = todoC.todoModel.DescriptionT,
                    Deadline = todoC.todoModel.Deadline,
                    CategoryId = todoC.todoModel.CategoryId
                };
                await todoRepo.CreateAsync(todo);
            }

            return RedirectToAction("Index");
        }

        public IActionResult Update() => View();

        public async Task<IActionResult> Delete(int id)
        {
            var todo = await todoRepo.GetAsync(id);
            if (todo != null)
            {
                await todoRepo.DeleteAsync(id);
                TempData["Success"] = "Todo was deleted";
                return RedirectToAction("Index");
            }
            else
            {
                TempData["Error"] = "That todo not exist";
                return RedirectToAction("Index");
            }
        }

        public async Task<IActionResult> Edit(int id)
        {
            var todo = await todoRepo.GetAsync(id);
            if (todo == null)
            {
                return NotFound();
            }

            return View(todo);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(TodoModel todo)
        {
            var t = await todoRepo.GetAsync(todo.id);

            if (t != null)
            {
                await todoRepo.UpdateAsync(todo);
                TempData["Sucsess"] = "That todo was edited";
            }
            else
            {
                ModelState.AddModelError("", "Todo not found!");
            }

            return RedirectToAction("Index");
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

            return RedirectToAction("Index");
        }
    }
}
