using SecureProgrammingProject.Tests;

namespace SecureProgrammingProject
{
    internal class Program
    {
        static void Main()
        {

            Console.WriteLine("Select tests: 1 = Password tester, 2 = Injection test, 3 = Swagger test");
            string? tests = Console.ReadLine();
            if(tests == null || tests == "")
            {
                Console.WriteLine("No tests selected");
                return;
            }
            tests = tests.Trim();
            List<char> test_ = [];
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

            Console.WriteLine("Give address to server, it should include http:// or https://");
            string? serveraddress = Console.ReadLine();
            if(serveraddress == null || serveraddress == "")
            {
                Console.WriteLine("No server address given");
                return;
            }
            TestOrchestrator orchestrator = new TestOrchestrator(serveraddress, test_);
        }
    }
}