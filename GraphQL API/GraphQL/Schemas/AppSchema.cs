using GraphQL.Types;


namespace GraphQL_API
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
