using System;

namespace GraphQL_API
{
    public class TodoUpdateInput
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime? Deadline { get; set; }
        public int? CategoryId { get; set; }
    }
}
