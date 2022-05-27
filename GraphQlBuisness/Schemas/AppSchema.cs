using GraphQL.Types;


namespace GraphQlBuisness
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
