using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using System;

namespace DropDownSelection
{
    public class Tests
    {
        private static IWebDriver _driver;

        [OneTimeSetUp]
        public static void OneTimeSetUp()
        {
            _driver = new FirefoxDriver();

            _driver.Manage().Window.Maximize();
            _driver.Navigate().GoToUrl("https://www.w3schools.com/tags/tryit.asp?filename=tryhtml_select_multiple");

            // accept cookies id=accept-choices
            IWebElement acceptChoicesButton = _driver.FindElement(By.Id("accept-choices"));

            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(30));
            wait.Until(ExpectedConditions.ElementToBeClickable(acceptChoicesButton));

            acceptChoicesButton.Click();
        }

        [OneTimeTearDown]
        public static void OneTimeTearDown()
        {
            _driver.Quit();
        }

        [TestCase("Volvo", "cars=volvo ", TestName = "Volvo")]
        [TestCase("Saab", "cars=saab ", TestName = "Saab")]
        [TestCase("Opel", "cars=opel ", TestName = "Opel")]
        [TestCase("Audi", "cars=audi ", TestName = "Audi")]
        public static void DropDownSelectionTests(string carBrand, string expectedResult)
        {
            _driver.SwitchTo().Frame("iframeResult");

            IWebElement carsDropDownElement = _driver.FindElement(By.Id("cars"));
            SelectElement SelectACar = new SelectElement(carsDropDownElement);
            SelectACar.SelectByText(carBrand);

            IWebElement submitButton = _driver.FindElement(By.CssSelector("body > form:nth-child(3) > input:nth-child(5)"));
            submitButton.Click();

            IWebElement actualResult = _driver.FindElement(By.XPath("/html/body/div[1]"));

            Assert.AreEqual(expectedResult, actualResult.Text, "The output does not match the expected output");

            // Switch back from iFrame and refresh the page
            _driver.SwitchTo().DefaultContent();

            IWebElement refreshButton = _driver.FindElement(By.XPath("/html/body/div[3]/div/button"));
            refreshButton.Click();
        }
    }
}
