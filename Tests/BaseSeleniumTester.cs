using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
namespace SecureProgrammingProject.Tests
{
    public abstract class BaseSeleniumTester : BaseTester
    {



         ~BaseSeleniumTester()
        {
            driver.Quit();
        }
        protected IWebDriver driver { get; private set; }

        protected BaseSeleniumTester(IWebDriver driver)
        {
            this.driver = driver;
        }

        public BaseSeleniumTester()
        {
            this.driver = new ChromeDriver();
        }

        protected bool navigateToURl(String address)
        {
            try
            {
                driver.Navigate().GoToUrl(address);
                return true;
            }
            catch (WebDriverArgumentException)
            {
                Console.WriteLine("Invalid server address");
                driver.Quit();
                return false;
            }
            catch(Exception ex)
            {
                return false;
            }

            
        }



        public override abstract void RunTest(string address, StringWriter resultsStream);

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
