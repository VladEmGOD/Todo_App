using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TODO_APP.Models
{
    public class CategoryModel
    {
        [Required]
        public int id { get; set; }
        [Required]
        public string NameC { get; set; }
    }
}
