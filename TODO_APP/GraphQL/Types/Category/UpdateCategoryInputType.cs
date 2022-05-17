﻿using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TODO_APP.GraphQL.Types.Category
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
