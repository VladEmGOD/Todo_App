using Buisness.Models;
using GraphQL.Types;

namespace TODO_APP.GraphQL.Types
{
    public class CategoryType : ObjectGraphType<CategoryModel>
    {
        public CategoryType()
        {
            Field<IntGraphType>()
               .Name("Id")
               .Description("Category id")
               .Resolve(ctx => ctx.Source.Id);

            Field<StringGraphType>()
               .Name("Name")
               .Description("Category name")
               .Resolve(ctx => ctx.Source.Name);
        }
    }
}
