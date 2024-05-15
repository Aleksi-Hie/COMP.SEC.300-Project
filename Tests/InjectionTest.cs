using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.Extensions;
using OpenQA.Selenium.Support.UI;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SecureProgrammingProject.Tests
{



    internal class InjectionTest : BaseSeleniumTester
    {
        List<string> INJECTIONS = new List<string>
    {
        "<script>alert()</script>"
    };
        List<string> searchedSites = new List<string>();
        protected override string testName => "Injection Test";


        public override void RunTest(string address, StringWriter resultsStream)
        {
            if (!navigateToURl(address))
            {
                return;
            }
            pageMapSearch();
            FinalReport();
            driver.Quit();
        }


        private void pageMapSearch()
        {

            testUrl(driver.Url);
            InputsTest();

            List<string> links = new List<string>();
            var linkelems = driver.FindElements(By.TagName("a"));

            foreach(IWebElement link in linkelems)
            {
                links.Add(link.GetAttribute("href"));
            }

            foreach (string link in links)
            {
                if (!searchedSites.Contains(link) && isInDomain(link))
                {
                    searchedSites.Add(link);
                    navigateToURl(link);
                    pageMapSearch();
                    driver.Navigate().Back();
                }
                

            }

        }

        private void testUrl(string address)
        {
            foreach(string injection in INJECTIONS)
            {
                try
                {
                    navigateToURl(address+ injection);
                    
                    if (isDialogPresent())
                    {
                        testRan(failed);
                        FailureReport($"Injection vulnerability at {driver.Url}, in field: URL, with arguments: {injection}");
                    }
                }
                catch (OpenQA.Selenium.WebDriverException e)
                {
                    testRan(passed);
                    continue;
                }
                driver.Navigate().Back();
            }
          

        }

        private void InputsTest()
        {
            try
            {
                var inputs = driver.FindElements(By.TagName("input"));
                foreach (IWebElement input in inputs)
                {
                    foreach (string injection in INJECTIONS)
                    {
                        try
                        {
                            input.SendKeys(injection);
                            if (isDialogPresent())
                            {
                                FailureReport($"Injection vulnerability at {driver.Url}, in field: {input.Text}, with arguments: {injection}");
                                testRan(failed);
                            }
                        }
                        catch (OpenQA.Selenium.WebDriverException e)
                        {
                            testRan(passed);
                            continue;
                        }
                    }

                }
            }
            catch(OpenQA.Selenium.UnhandledAlertException e) {
                FailureReport($"Injection vulnerability at {driver.Url}");
            }
            

        }
        private bool isInDomain(string ref_)
        {
            Uri uri = new Uri(ref_);
            string domain = uri.Host;

            Uri startingUri = new Uri(driver.Url);
            string startingDomain = startingUri.Host;
            return domain.Equals(startingDomain, StringComparison.OrdinalIgnoreCase);
        }
        private bool isDialogPresent()
        {
            Thread.Sleep(100);
            try
            {
                driver.SwitchTo().Alert().Accept();
                return true;
            }
            catch (OpenQA.Selenium.NoAlertPresentException ex)
            {
                return false;
            }
            
        }


    }   

}
