using Buisness.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TODO_APP.Repositories.Infrastructure
{
    public delegate ICategoriesPerository CategoryReslover(string repositorySource);
    public delegate ITodoRepository TodoReslover(string repositorySource);
}
