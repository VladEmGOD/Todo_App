using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TODO_APP.Repositories.Infrastructure
{
    public class RepositorySourceTypes
    {
        public SourceType MSSQL { get; }
        public SourceType XML { get; }

        public RepositorySourceTypes()
        {
            MSSQL = new SourceType("MSSQL");
            XML = new SourceType("XML");
        }
    }
}
