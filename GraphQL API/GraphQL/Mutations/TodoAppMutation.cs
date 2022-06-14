using Buisness;
using Buisness.Models;
using Buisness.Repositories.Interfaces;
using GraphQL;
using GraphQL.Types;
using GraphQL_API.Infrastructure;
using Microsoft.AspNetCore.Http;
using System;

namespace GraphQL_API
{
    public class TodoAppMutation : ObjectGraphType
    {
        DataSource dataSource;
        ITodoRepository todoRepository;
        ICategoriesRerository categoriesRerository;
        public TodoAppMutation(RepositoryResolver repositoryReslover, IHttpContextAccessor httpContextAccesor)
        {
            string dataSourceStr = httpContextAccesor.HttpContext.Request.Headers["dataSource"];
            if (Enum.TryParse(dataSourceStr, out dataSource))
            {
                todoRepository = repositoryReslover.ResolveTodoRepository(dataSource);
                categoriesRerository = repositoryReslover.ResolveCategoryRepository(dataSource);
            }
            else
            {
                todoRepository = repositoryReslover.ResolveTodoRepository(DataSource.MsSql);
                categoriesRerository = repositoryReslover.ResolveCategoryRepository(DataSource.MsSql);
            }

            //Todo mutations
            Field<TodoType, TodoModel>()
              .Name("CreateTodo")
              .Argument<NonNullGraphType<TodoCreateInputType>, TodoCreateInput>("TodoCreateInput", "Argument for create new Todo")
              .ResolveAsync(async context =>
              {
                  var inputTodo = context.GetArgument<TodoCreateInput>("TodoCreateInput");
                  var todo = new TodoModel() { Title = inputTodo.Title, CategoryId = inputTodo.CategoryId, Deadline = inputTodo.Deadline };
                  await todoRepository.CreateAsync(todo);
                  return todo;
              });

            Field<TodoType, TodoModel>()
              .Name("UpdateTodo")
              .Argument<NonNullGraphType<TodoUpdateInputType>, TodoUpdateInput>("TodoUpdateInput", "Argument for update a Todo")
              .ResolveAsync(async context =>
              {
                  var inputTodo = context.GetArgument<TodoUpdateInput>("TodoUpdateInput");
                  var todo = await todoRepository.GetTodoByIdAsync(inputTodo.Id);

                  if (todo == null) throw new ExecutionError($"Todo with id {inputTodo.Id} not found!");

                  var newTodo = new TodoModel { Id = inputTodo.Id, Title = inputTodo.Title, CategoryId = inputTodo.CategoryId, Deadline = inputTodo.Deadline };
                  await todoRepository.UpdateAsync(newTodo);

                  return newTodo;
              });

            Field<TodoType, TodoModel>()
              .Name("DeleteTodo")
              .Argument<NonNullGraphType<IntGraphType>, int>("Id", "Argument for delete a Todo by id")
              .ResolveAsync(async context =>
              {
                  var todoId = context.GetArgument<int>("Id");
                  var todo = await todoRepository.GetTodoByIdAsync(todoId);

                  if (todo == null) throw new ExecutionError($"Category with id {todoId} not found!");

                  await todoRepository.DeleteAsync(todoId);
                  return todo;
              });

            Field<TodoType, TodoModel>()
              .Name("TogleIsDone")
              .Argument<NonNullGraphType<IntGraphType>, int>("Id", "Argument for change 'IsDone' for Todo with that id")
              .ResolveAsync(async context =>
              {
                  var todoId = context.GetArgument<int>("Id");
                  var todo = await todoRepository.GetTodoByIdAsync(todoId);

                  if (todo == null) throw new ExecutionError($"Todo with id {todoId} not found!");

                  todo.IsDone = !todo.IsDone;

                  await todoRepository.UpdateAsync(todo);
                  return todo;
              });

            //Categories mutation
            Field<CategoryType, CategoryModel>()
              .Name("CreateCategory")
              .Argument<NonNullGraphType<CreateCategoryInputType>, CategoryCreateInput>("CategoryCreateInput", "Argument for create new category")
              .ResolveAsync(async context =>
              {
                  var inputCategory = context.GetArgument<CategoryCreateInput>("CategoryCreateInput");
                  var category = new CategoryModel() { Name = inputCategory.Name };
                  await categoriesRerository.CreateAsync(category);
                  return category;
              });

            Field<CategoryType, CategoryModel>()
              .Name("UpdateCategory")
              .Argument<NonNullGraphType<UpdateCategoryInputType>, UpdateCategoryInput>("UpdateCategoryInput", "Argument for update a category")
              .ResolveAsync(async context =>
              {
                  var inputCategory = context.GetArgument<UpdateCategoryInput>("UpdateCategoryInput");
                  var category = await categoriesRerository.GetCategoryByIdAsync(inputCategory.Id);

                  if (category == null) throw new ExecutionError($"Todo with id {inputCategory.Id} not found!");

                  var newCategory = new CategoryModel() { Id = inputCategory.Id, Name = inputCategory.Name };
                  await categoriesRerository.EditAsync(newCategory);
                  return newCategory;
              });

            Field<CategoryType, CategoryModel>()
              .Name("DeleteCategory")
              .Argument<NonNullGraphType<IntGraphType>, int>("Id", "Argument for delete a category by id")
              .ResolveAsync(async context =>
              {
                  var categoryId = context.GetArgument<int>("Id");
                  var category = await categoriesRerository.GetCategoryByIdAsync(categoryId);

                  if (category == null) throw new ExecutionError($"Todo with id {categoryId} not found!");

                  await categoriesRerository.DeleteAsync(categoryId);
                  return category;
              });


        }
    }
}
