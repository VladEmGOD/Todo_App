using Buisness.Models;
using System.Collections.Generic;

namespace TODO_APP.ViewModels
{
    public class TodoCreateFormViewModel
    {
        public TodoModel TodoModel { get; set; }
        public List<CategoryModel> CategoryModels { get; set; }

    }
}
