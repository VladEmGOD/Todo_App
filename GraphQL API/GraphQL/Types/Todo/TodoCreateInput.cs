using System;

namespace GraphQL_API
{
    public class TodoCreateInput
    {
        public string Title { get; set; }
        public DateTime? Deadline { get; set; }
        public int? CategoryId { get; set; }
    }
}
