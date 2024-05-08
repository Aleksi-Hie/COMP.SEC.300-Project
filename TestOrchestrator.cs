<<<<<<< HEAD
=======
<<<<<<<< HEAD:TestOrchestrator.cs
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SecureProgrammingProject.Tests;

namespace SecureProgrammingProject
{
    internal class TestOrchestrator
    {
        private List<TestInterface> testers;
        private String address;
        public TestOrchestrator(String address)
        {
            this.address = address;

            testers = new List<TestInterface>
            { 
                new SwaggerTest("C:\\Users\\aleks\\Documents\\clever-name\\backend\\server-a\\api\\swagger.yaml")
            };
        }
        public void Run()
        {
            foreach (TestInterface tester in testers)
            {
                tester.RunTest(address, new StringWriter());
            }
        }
    }
}
========
>>>>>>> 534183718ef6d07849b46bbcf4c532876b779d53
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SecureProgrammingProject.Tests;

namespace SecureProgrammingProject
{
    internal class TestOrchestrator
    {
<<<<<<< HEAD


        private String address;



        public TestOrchestrator(String address, List<char> tests)
        {
            this.address = address;
            Dictionary<char, TestInterface> testers = new Dictionary<char, TestInterface>();
            foreach (char testKey in tests)
            {
                TestInterface testObject = null;

                switch (testKey)
                {
                    case '1':
                        Console.WriteLine("Give password list filepath or leave empty for default file in cwd: ");
                        string path =  Console.ReadLine();
                        Console.WriteLine("Path of login portal: ");
                        string portal = Console.ReadLine();
                        if (path == "")
                        {
                            testObject = new CommonPasswordsTest(portal);
                        }
                        else
                        {
                            Console.WriteLine("Give delimiter for the file username / password combinations: ");
                            string delimiter = Console.ReadLine();
                            testObject = new CommonPasswordsTest(path, delimiter, portal);
                        }
                        break;
                    case '2':
                        testObject = new InjectionTest();
                        break;
                    case '3':
                        Console.WriteLine("Give path to swagger file: ");
                        string swaggerPath = Console.ReadLine();
                        try
                        {
                            testObject = new SwaggerTest(swaggerPath);
                        }
                        catch(Exception e)
                        {
                            Console.WriteLine($"Swagger file not found: '{swaggerPath}'");
                        };
                        break;
                    default:
                        Console.WriteLine($"Invalid test key '{testKey}'.");
                        break;
                }

                // Add initialized test object to the dictionary if it's not null
                if (testObject != null)
                {
                    testers.Add(testKey, testObject);
                }
            }

            foreach (char c in tests)
            {
                if (testers.ContainsKey(c))
                {
                    testers[c].RunTest(address, new StringWriter());
                }
                else
                {
                    Console.WriteLine("Unrecognised tests: " + c);
                }
            }

        }
    }
}
=======
        private List<TestInterface> testers;
        private String address;
        public TestOrchestrator(String address)
        {
            this.address = address;

            testers = new List<TestInterface>
            {
                new SwaggerTest("C:\\Users\\aleksi\\Documents\\webdev2 projekti\\webdev2\\backend\\server-a\\api\\swagger.yaml")
            };
        }
        public void Run()
        {
            foreach (TestInterface tester in testers)
            {
                tester.RunTest(address, new StringWriter());
            }
        }
    }
}
>>>>>>>> 534183718ef6d07849b46bbcf4c532876b779d53:SecureProgrammingProject/TestOrchestrator.cs
>>>>>>> 534183718ef6d07849b46bbcf4c532876b779d53
