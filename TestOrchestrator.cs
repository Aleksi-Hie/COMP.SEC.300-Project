using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SecureProgrammingProject.Tests;

namespace SecureProgrammingProject
{
    internal class TestOrchestrator
    {


        private String address;



        public TestOrchestrator(String address, List<char> tests)
        {
            this.address = address;
            Dictionary<char, TestInterface> testers = new Dictionary<char, TestInterface>();
            foreach (char testKey in tests)
            {
                TestInterface testObject = null;
                if (testers.TryGetValue(testKey, out testObject)) {
                    Console.WriteLine("Multiple copies of test detected only 1 will be initialized");
                    continue;
                }
                switch (testKey)
                {
                    
                    case '1':
                        Console.WriteLine("Give password list filepath or leave empty for default file in cwd: ");
                        string path =  Console.ReadLine();
                        Console.WriteLine("Path of login portal: ");
                        string portal = Console.ReadLine();
                        if(portal == null)
                        {
                            portal = "";
                        }
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

                if (testObject != null)
                {
                    try
                    {
                        testers.Add(testKey, testObject);
                    }
                    catch (Exception e) { Console.WriteLine("Multiple copies of test detected");}
                    
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
                    Console.WriteLine("Failed to initialize test: " + c);
                }
            }

        }
    }
}