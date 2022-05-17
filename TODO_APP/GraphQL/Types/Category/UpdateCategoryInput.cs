using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TODO_APP.GraphQL.Types.Category
{
    public class UpdateCategoryInput
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
