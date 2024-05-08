<<<<<<< HEAD
﻿using System;
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

        protected void navigateToURl(String address)
        {
            driver.Navigate().GoToUrl(address);
        }



        public override abstract void RunTest(string address, StringWriter resultsStream);

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
=======
﻿using System;
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

        protected void navigateToURl(String address)
        {
            driver.Navigate().GoToUrl(address);
        }



        public override abstract void RunTest(string address, StringWriter resultsStream);

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
>>>>>>> 534183718ef6d07849b46bbcf4c532876b779d53
