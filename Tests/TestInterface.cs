using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecureProgrammingProject.Tests
{
    internal interface TestInterface
    {
        void RunTest(string address, StringWriter resultsStream);
    }
}
