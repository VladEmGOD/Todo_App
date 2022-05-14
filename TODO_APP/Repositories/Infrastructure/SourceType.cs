using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TODO_APP.Repositories.Infrastructure
{
    public class SourceType
    {
        public SourceType(string value)
        {
            Value = value;
        }
        public string Value { get; set; }
    }
}
