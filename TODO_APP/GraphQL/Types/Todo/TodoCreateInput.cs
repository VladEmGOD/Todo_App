using System;

namespace TODO_APP.GraphQL.Types.Todo
{
    public class TodoCreateInput
    {
        public string Title { get; set; }
        public DateTime? Deadline { get; set; }
        public int? CategoryId { get; set; }
    }
}
