using Buisness.Models;
using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TODO_APP.GraphQL.Types.Todo
{
    public class TodoCreateInputType : InputObjectGraphType<TodoCreateInput>
    {
        public TodoCreateInputType()
        {
            
            Field<NonNullGraphType<StringGraphType>, string>()
                .Name("Title")
                .Resolve(context => context.Source.Title);

            Field<DateTimeGraphType, DateTime?>()
                .Name("Deadline")
                .Resolve(context => context.Source.Deadline);

            Field<IntGraphType, int?>()
                .Name("CategoryId")
                .Resolve(context => context.Source.CategoryId);
        }
    }
}
