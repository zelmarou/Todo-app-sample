using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Todo.Services
{
    public class MyBusinessLogicService : IMyBusinessLogicService
    {
        public int DoSomeMath(int y, int x)
        {
            return y + x;
        }
    }
}
