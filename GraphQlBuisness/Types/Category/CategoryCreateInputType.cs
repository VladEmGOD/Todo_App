using GraphQL.Types;

namespace GraphQlBuisness
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
