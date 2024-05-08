<<<<<<< HEAD
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecureProgrammingProject.Tests
{
    public abstract class BaseTester : TestInterface
    {
        protected BaseTester()
        {
            report = new StringWriter();
            report.WriteLine($"Report summary: {testName}");
        }


        abstract protected String testName { get; }
        protected bool failed = false;
        protected bool passed = true;
        private int totalTests = 0;
        private int totalFailures = 0;

        private StringWriter report;
        protected void Print(StringWriter writer, String text)
        {
            writer.WriteLine($"{testName} {text}");
        }
        protected void testRan(bool passed_)
        {
            totalTests++;
            if (!passed_)
            {
                totalFailures++;
            }
        }
        protected void FailureReport(String text)
        {
            report.WriteLine($"Failure at: {text}, {DateTime.Now}");
        }
        protected void FinalReport()
        {
            report.WriteLine("");
            report.WriteLine($"Total tests done: {totalTests}");
            report.WriteLine($"Tests failed: {totalFailures}");
            report.WriteLine();
            var file = File.OpenWrite($"{testName}-{DateTime.Now}.txt");
            var bytes = Encoding.UTF8.GetBytes(report.ToString());
            file.Write(bytes, 0, bytes.Length);
            file.Close();
        }
        public abstract void RunTest(string address, StringWriter resultsStream);
    }

=======
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecureProgrammingProject.Tests
{
    public abstract class BaseTester : TestInterface
    {
        protected BaseTester()
        {
            report = new StringWriter();
            report.WriteLine($"Report summary: {testName}");
        }


        abstract protected String testName { get; }
        protected bool failed = false;
        protected bool passed = true;
        private int totalTests = 0;
        private int totalFailures = 0;

        private StringWriter report;
        protected void Print(StringWriter writer, String text)
        {
            writer.WriteLine($"{testName} {text}");
        }
        protected void testRan(bool passed_)
        {
            totalTests++;
            if (!passed_)
            {
                totalFailures++;
            }
        }
        protected void FailureReport(String text)
        {
            report.WriteLine($"Failure at: {text}, {DateTime.Now}");
        }
        protected void FinalReport()
        {
            report.WriteLine("");
            report.WriteLine($"Total tests done: {totalTests}");
            report.WriteLine($"Tests failed: {totalFailures}");
            report.WriteLine();
            var file = File.OpenWrite($"{testName}-{DateTime.Now}.txt");
            var bytes = Encoding.UTF8.GetBytes(report.ToString());
            file.Write(bytes, 0, bytes.Length);
            file.Close();
        }
        public abstract void RunTest(string address, StringWriter resultsStream);
    }

>>>>>>> 534183718ef6d07849b46bbcf4c532876b779d53
}