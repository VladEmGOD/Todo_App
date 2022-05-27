using GraphQL.Types;
using System;

namespace GraphQL_API
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
