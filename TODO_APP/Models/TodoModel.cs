using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace TODO_APP.Models
{
    public class TodoModel
    {
        [Required]
        public int id { get; set; }
        public int? CategoryId { get; set; }
        [Required]
        public string Tittle { get; set; }
        public string DescriptionT { get; set; }
        public DateTime? Deadline { get; set; }
        public bool isDone { get; set; }
    }
}
