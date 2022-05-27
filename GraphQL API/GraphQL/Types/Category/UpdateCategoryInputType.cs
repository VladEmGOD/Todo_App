using GraphQL.Types;

namespace GraphQL_API
{
    public class UpdateCategoryInputType : InputObjectGraphType<UpdateCategoryInput>
    {
        public UpdateCategoryInputType()
        {
            Field<NonNullGraphType<IntGraphType>, int>()
                .Name("Id")
                .Resolve(context => context.Source.Id);

            Field<NonNullGraphType<StringGraphType>, string>()
                .Name("Name")
                .Resolve(context => context.Source.Name);
        }
    }
}
