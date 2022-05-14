using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TODO_APP.Models;

namespace TODO_APP.ViewModels
{
    public class IndexTodoViewModel
    {
        public IndexTodoViewModel()
        {

        }

        public IndexTodoViewModel(TodoModel todoModel)
        {
            Tittle = todoModel.Tittle;
            Id = todoModel.Id;
            Deadline = todoModel.Deadline;
            IsDone = todoModel.IsDone;
        }
        public int Id { get; set; }
        public string Category { get; set; }
        public string Tittle { get; set; }
        public DateTime? Deadline { get; set; }
        public bool IsDone { get; set; }
    }
}
