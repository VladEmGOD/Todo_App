using Buisness.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TODO_APP.Models;

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
 