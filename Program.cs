<<<<<<< HEAD
=======
<<<<<<<< HEAD:Program.cs
﻿using SecureProgrammingProject.Tests;

namespace SecureProgrammingProject
{
    internal class Program
    {
        static void Main(string[] args)
        {
        
            TestOrchestrator orchestrator = new TestOrchestrator("http://127.0.0.1:8080/v1");
            orchestrator.Run();
        }
    }
}
========
>>>>>>> 534183718ef6d07849b46bbcf4c532876b779d53
﻿using SecureProgrammingProject.Tests;

namespace SecureProgrammingProject
{
    internal class Program
    {
        static void Main(string[] args)
        {

<<<<<<< HEAD
            //Read numbers from 123

            Console.WriteLine("Select tests: 1 = Password tester, 2 = Injection test, 3 = Swagger test");

            string tests = Console.ReadLine();
            tests = tests.Trim();
            List<char> test_ = new List<char>();
            foreach(char c in tests)
            {
                if(c == ' ')
                {
                    continue;
                }
                else if (c == '1' || c== '2' || c== '3')
                {
                    test_.Add(c);
                }
                else
                {
                    Console.WriteLine("Unrecognised tests: " +  c);
                }
            }

            Console.WriteLine("Give address to server");
            string serveraddress = Console.ReadLine();
            TestOrchestrator orchestrator = new TestOrchestrator(serveraddress, test_);
        }
    }
}
=======
            TestOrchestrator orchestrator = new TestOrchestrator("http://127.0.0.1:8080/v1");
            orchestrator.Run();
        }
    }
}
>>>>>>>> 534183718ef6d07849b46bbcf4c532876b779d53:SecureProgrammingProject/Program.cs
>>>>>>> 534183718ef6d07849b46bbcf4c532876b779d53
