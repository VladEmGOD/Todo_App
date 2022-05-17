using GraphQL.Types;
using Microsoft.Extensions.DependencyInjection;
using System;
using TODO_APP.GraphQL.Mutations;
using TODO_APP.GraphQL.Queries;

namespace TODO_APP.GraphQL.Schemas
{
    public class AppSchema : Schema
    {
        public AppSchema(TodoAppQuery query, TodoAppMutation mutation )
        {
            Query = query;
            Mutation = mutation;
        }
    }
}
