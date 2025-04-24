using System;
using System.Text;
using NUnit.Framework;
using NUnit.Framework.Legacy;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using System.IO;

namespace NUnit_SEQA
{
    [TestFixture]
    public class IFrameTest
    {
        private IWebDriver driver;
        private string baseURL;
        private StringBuilder verificationErrors;
        private string path;

        [SetUp]
        public void Setup()
        {
            driver = new FirefoxDriver();
            baseURL = "http://the-internet.herokuapp.com/iframe";
            verificationErrors = new StringBuilder();
            path = "C:/Users/AkuToR/source/repos/NUnit_SEQA/screenshots/";

            Directory.CreateDirectory(path);
        }

        [Test]
        public void TestIFrameInteraction()
        {
            driver.Navigate().GoToUrl(baseURL);

            TakeScreenshot("01_opened_page.png");

            driver.SwitchTo().Frame("mce_0_ifr");

            var editor = driver.FindElement(By.Id("tinymce"));
            editor.Clear();
            editor.SendKeys("Hello!");

            TakeScreenshot("02_text_entered.png");

            ClassicAssert.AreEqual("Hello!", editor.Text);

            driver.SwitchTo().DefaultContent();
        }

        private void TakeScreenshot(string fileName)
        {
            Screenshot screenshot = ((ITakesScreenshot)driver).GetScreenshot();
            screenshot.SaveAsFile(Path.Combine(path, fileName), ScreenshotImageFormat.Png); 



        }

        [TearDown]
        public void Teardown()
        {
            try
            {
                driver.Quit();
            }
            catch (Exception)
            {
               
            }
            ClassicAssert.AreEqual("", verificationErrors.ToString());
        }

        static public void Main(string[] args)
        {
            IFrameTest test = new IFrameTest();
            test.Setup();
            test.TestIFrameInteraction();
            test.Teardown();
        }
    }
}