using System;

namespace TODO_APP.GraphQL.Types.Todo
{
    public class TodoUpdateInput
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime? Deadline { get; set; }
        public int? CategoryId { get; set; }
    }
}
