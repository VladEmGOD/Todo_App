using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TODO_APP.GraphQL.Types.Category
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
