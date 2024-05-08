<<<<<<< HEAD
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.DevTools.V120.CSS;
using OpenQA.Selenium.Support.Events;

/*
Testing should include the login portal
The test loads file of common usernames and passwords and tries to login with them
If the login is successful, the test fails
*/

namespace SecureProgrammingProject.Tests
{

    internal class CommonPasswordsTest : BaseSeleniumTester
    {
        private List<String> UserNames = new List<string>();
        private List<String> PassWords = new List<string>();
        private String DefaultFilename = "default_devices_users+passwords.txt";
        private static readonly List<String> Commonusernamefields = new List<string>{
                "username",
                        "usernamefield",
                        "user_id",
                        "käyttäjänimi",
                        "name",
                        "uid"
        };
        private static readonly List<string> Commonpasswordfields = new List<string>
        {
            "password",
            "salasana",
            "pw"
        };
        private static readonly List<string> CommonsubmitButtons = new List<string>
        {
            "login",
            "kirjaudu",
            "submit"

        };

        private string portal = "";

        protected override string testName { get => "Commom Passwords Test"; }

        private void LoadFile(String filename, String delimiter = ":")
        {
            string[] lines = File.ReadAllLines(filename);
            foreach (string line in lines)
            {
                string[] parts = line.Split(delimiter);
                UserNames.Add(parts[0]);
                PassWords.Add(parts[1]);
            }
        }

        public CommonPasswordsTest(String filename, String delimiter, string portal)
        {
            if (filename == null) LoadFile(DefaultFilename, delimiter);
            else if (filename != null && delimiter == null) throw new ArgumentException("Missing delimiter. File should be in the form usernames'delimiter'password");
            else { LoadFile(filename, delimiter); }
            this.portal = portal;
        }



        public CommonPasswordsTest(string portal)
        {
            LoadFile(DefaultFilename);
            this.portal = portal;
        }



        
        public override void RunTest(string address, StringWriter resultsStream)
        {


            InitDriver(address);
            IWebElement usernameField = null;
            IWebElement passwordField = null;
            IWebElement SubmitButton = null;
            try
            {
                 usernameField = FindElement("input", "name",Commonusernamefields );
                passwordField = FindElement("input", "name", Commonpasswordfields);
                SubmitButton = FindElement("input", "type", CommonsubmitButtons);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
                return;
            }
            Console.WriteLine(usernameField);
            Console.WriteLine(passwordField);

            var Buttons = driver.FindElements(By.TagName("input"));
            foreach (var button in Buttons)
            {
                if (CommonsubmitButtons.Contains(button.GetAttribute("type")))
                {
                    SubmitButton = button;
                    break;
                }
            }

            Console.WriteLine(SubmitButton);



            if (usernameField == null || passwordField == null || SubmitButton == null) { Print(resultsStream, "Could not find login fields"); return; };
            for (int i = 0; i < UserNames.Count; i++)
            {


                try
                {
                    usernameField.SendKeys(UserNames[i]);
                    passwordField.SendKeys(PassWords[i]);
                    SubmitButton.Submit();

                    usernameField = FindElement("input", "name", Commonusernamefields);
                    passwordField = FindElement("input", "name", Commonpasswordfields);
                    SubmitButton = FindElement("input", "type", CommonsubmitButtons);
                    testRan(passed);

                }

                catch (ArgumentException ex)
                {
                    InitDriver(address);
                    testRan(failed);
                    FailureReport($"Weak password found username: {UserNames[i]} password {PassWords[i]}");
                    driver.Manage().Cookies.DeleteAllCookies();
                    continue;

                }
                catch (OpenQA.Selenium.StaleElementReferenceException ex)
                {
                    usernameField = FindElement("input", "name", Commonusernamefields);
                    passwordField = FindElement("input", "name", Commonpasswordfields);
                    SubmitButton = FindElement("input", "type", CommonsubmitButtons);

                }

                catch (OpenQA.Selenium.WebDriverException e)
                {
                    Console.WriteLine(e.Message);
                }

            }
            FinalReport();
            driver.Quit();
        }
        private IWebElement FindElement(String tagname, String attributeName, List<string> container)
        {

            var inputFields = driver.FindElements(By.TagName(tagname));
            foreach (var field in inputFields)
            {
                string attribute = field.GetAttribute(attributeName);
                if (container.Contains(attribute))
                {
                    return field;
                }
            }
            throw new ArgumentException($"Element not found ${attributeName}");
        }

        private void InitDriver(String address)
        {
            navigateToURl(address);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            String endpoint = driver.Url;
            navigateToURl(endpoint + portal);
            driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(5);
        }

    }
}

=======
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.DevTools.V120.CSS;
using OpenQA.Selenium.Support.Events;

/*
Testing should include the login portal
The test loads file of common usernames and passwords and tries to login with them
If the login is successful, the test fails
*/

namespace SecureProgrammingProject.Tests
{

    internal class CommonPasswordsTest : BaseSeleniumTester
    {
        private List<String> UserNames = new List<string>();
        private List<String> PassWords = new List<string>();
        private String DefaultFilename = "default_devices_users+passwords.txt";
        private static readonly List<String> Commonusernamefields = new List<string>{
                "username",
                        "usernamefield",
                        "user_id",
                        "käyttäjänimi",
                        "name",
                        "uid"
        };
        private static readonly List<string> Commonpasswordfields = new List<string>
        {
            "password",
            "salasana",
            "pw"
        };
        private static readonly List<string> CommonsubmitButtons = new List<string>
        {
            "login",
            "kirjaudu",
            "submit"

        };

        protected override string testName { get => "Commom Passwords Test"; }

        private void LoadFile(String filename, String delimiter = ":")
        {
            string[] lines = File.ReadAllLines(filename);
            foreach (string line in lines)
            {
                string[] parts = line.Split(delimiter);
                UserNames.Add(parts[0]);
                PassWords.Add(parts[1]);
            }
        }

        public CommonPasswordsTest(String filename, String delimiter)
        {
            if (filename == null) LoadFile(DefaultFilename, delimiter);
            else if (filename != null && delimiter == null) throw new ArgumentException("Missing delimiter. File should be in the form usernames'delimiter'password");
            else { LoadFile(filename, delimiter); }
        }



        public CommonPasswordsTest()
        {
            LoadFile(DefaultFilename);
        }



        
        public override void RunTest(string address, StringWriter resultsStream)
        {


            InitDriver(address);
            IWebElement usernameField = null;
            IWebElement passwordField = null;
            IWebElement SubmitButton = null;
            try
            {
                 usernameField = FindElement("input", "name",Commonusernamefields );
                passwordField = FindElement("input", "name", Commonpasswordfields);
                SubmitButton = FindElement("input", "type", CommonsubmitButtons);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
                return;
            }
            Console.WriteLine(usernameField);
            Console.WriteLine(passwordField);

            var Buttons = driver.FindElements(By.TagName("input"));
            foreach (var button in Buttons)
            {
                if (CommonsubmitButtons.Contains(button.GetAttribute("type")))
                {
                    SubmitButton = button;
                    break;
                }
            }

            Console.WriteLine(SubmitButton);



            if (usernameField == null || passwordField == null || SubmitButton == null) { Print(resultsStream, "Could not find login fields"); return; };
            for (int i = 0; i < UserNames.Count; i++)
            {


                try
                {
                    usernameField.SendKeys(UserNames[i]);
                    passwordField.SendKeys(PassWords[i]);
                    SubmitButton.Submit();

                    usernameField = FindElement("input", "name", Commonusernamefields);
                    passwordField = FindElement("input", "name", Commonpasswordfields);
                    SubmitButton = FindElement("input", "type", CommonsubmitButtons);
                    testRan(passed);

                }

                catch (ArgumentException ex)
                {
                    InitDriver(address);
                    testRan(failed);
                    FailureReport($"Weak password found username: {UserNames[i]} password {PassWords[i]}");
                    driver.Manage().Cookies.DeleteAllCookies();
                    continue;

                }
                catch (OpenQA.Selenium.StaleElementReferenceException ex)
                {
                    usernameField = FindElement("input", "name", Commonusernamefields);
                    passwordField = FindElement("input", "name", Commonpasswordfields);
                    SubmitButton = FindElement("input", "type", CommonsubmitButtons);

                }

                catch (OpenQA.Selenium.WebDriverException e)
                {
                    Console.WriteLine(e.Message);
                }

            }
            FinalReport();
            driver.Quit();
        }
        private IWebElement FindElement(String tagname, String attributeName, List<string> container)
        {

            var inputFields = driver.FindElements(By.TagName(tagname));
            foreach (var field in inputFields)
            {
                string attribute = field.GetAttribute(attributeName);
                if (container.Contains(attribute))
                {
                    return field;
                }
            }
            throw new ArgumentException($"Element not found ${attributeName}");
        }

        private void InitDriver(String address)
        {
            navigateToURl(address);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            String endpoint = driver.Url;
            navigateToURl(endpoint + "login");
            driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(5);
        }

    }
}

>>>>>>> 534183718ef6d07849b46bbcf4c532876b779d53
