using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Buisness.Models
{
    public class TodoModel
    {
        [Required]
        public int Id { get; set; }
        public int? CategoryId { get; set; }
        [Required]
        public string Tittle { get; set; }
        public DateTime? Deadline { get; set; }
        public bool IsDone { get; set; }
    }
}
