using Buisness.Models;
using System.Collections.Generic;

namespace TODO_APP.ViewModels
{
    public class IndexPageViewModel
    {
        public IndexPageViewModel()
        {

        }

        public IndexPageViewModel(IEnumerable<IndexTodoViewModel> todos, IEnumerable<CategoryModel> categories)
        {
            Todos = todos;
            Categories = categories;
        }
        public TodoModel TodoModel { get; set; }
        public IEnumerable<IndexTodoViewModel> Todos { get; set; }
        public IEnumerable<CategoryModel> Categories { get; set; }
    }
}
 