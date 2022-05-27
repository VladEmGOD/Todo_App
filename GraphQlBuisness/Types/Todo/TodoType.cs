using Buisness.Models;
using GraphQL.Types;

namespace GraphQlBuisness
{
    public class TodoType : ObjectGraphType<TodoModel>
    {
        public TodoType()
        {
            Field<IntGraphType>()
                .Name("Id")
                .Description("Todo id")
                .Resolve(ctx => ctx.Source.Id);

            Field<IntGraphType>()
                .Name("CategoryId")
                .Description("Category id of")
                .Resolve(ctx => ctx.Source.CategoryId);

            Field<StringGraphType>()
                .Name("Title")
                .Description("Title of todo")
                .Resolve(ctx => ctx.Source.Title);

            Field<DateTimeGraphType>()
                .Name("Deadline")
                .Description("Deadline of todo")
                .Resolve(ctx => ctx.Source.Deadline);

            Field<BooleanGraphType>()
                .Name("IsDone")
                .Description("Is Done todo")
                .Resolve(ctx => ctx.Source.IsDone);
        }
    }
}
