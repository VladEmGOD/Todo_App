using Buisness.Models;
using System;

namespace TODO_APP.ViewModels
{
    public class IndexTodoViewModel
    {
        public IndexTodoViewModel()
        {

        }

        public IndexTodoViewModel(TodoModel todoModel)
        {
            Title = todoModel.Title;
            Id = todoModel.Id;
            Deadline = todoModel.Deadline;
            IsDone = todoModel.IsDone;
        }
        public int Id { get; set; }
        public string Category { get; set; }
        public string Title { get; set; }
        public DateTime? Deadline { get; set; }
        public bool IsDone { get; set; }
    }
}
