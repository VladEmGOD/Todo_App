﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TODO_APP.Models;

namespace TODO_APP.ViewModels
{
    public class TodoCreateFormViewModel
    {
        public TodoModel todoModel { get; set; }
        public List<CategoryModel> categoryModel { get; set; }

    }
}
