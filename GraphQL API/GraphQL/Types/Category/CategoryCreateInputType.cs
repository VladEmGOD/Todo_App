using GraphQL.Types;

namespace GraphQL_API
{
    public class CreateCategoryInputType : InputObjectGraphType<CategoryCreateInput>
    {
        public CreateCategoryInputType()
        {
            Field<NonNullGraphType<StringGraphType>, string>()
               .Name("Name")
               .Resolve(context => context.Source.Name);
        }
    }
}
