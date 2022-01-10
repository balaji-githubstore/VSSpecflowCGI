using NUnit.Framework;
using OpenEmrApplication.Hooks;
using OpenEmrApplication.Utilities;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using TechTalk.SpecFlow;

namespace OpenEmrApplication.Steps
{
    [Binding]
    public class LoginSteps
    {
        [Given(@"I have browser with openemr url")]
        public void GivenIHaveBrowserWithOpenemrUrl()
        {
           //var dt= ExcelUtils.SheetToDataTable(@"D:\Report\OpenEMRData.xlsx", "Invalid Credential");
            OpenEmrHooks.driver = new ChromeDriver(@"D:\B-Mine\Company\Specflow\OpenEmrApplication_2021\OpenEmrApplication\Drivers\");
            OpenEmrHooks.driver.Manage().Window.Maximize();
            OpenEmrHooks.driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(20);
            OpenEmrHooks.driver.Url = "http://demo.openemr.io/b/openemr/interface/login/login.php?site=default";
        }
        
        [When(@"I enter username as '(.*)'")]
        public void WhenIEnterUsernameAs(string username)
        {
            OpenEmrHooks.driver.FindElement(By.Id("authUser")).SendKeys(username);   
        }
        
        [When(@"I enter password as '(.*)'")]
        public void WhenIEnterPasswordAs(string password)
        {
            OpenEmrHooks.driver.FindElement(By.Id("clearPass")).SendKeys(password);
        }
        
        [When(@"I select the language as '(.*)'")]
        public void WhenISelectTheLanguageAs(string langaugeText)
        {
            SelectElement select = new SelectElement(OpenEmrHooks.driver.FindElement(By.Name("languageChoice")));
            select.SelectByText(langaugeText);
        }
        
        [When(@"I click on login")]
        public void WhenIClickOnLogin()
        {
            OpenEmrHooks.driver.FindElement(By.XPath("//button[@type='submit']")).Click();
        }
        
        [Then(@"I should get the error detail as '(.*)'")]
        public void ThenIShouldGetTheErrorDetailAs(string expectedValue)
        {
           string actualValue=OpenEmrHooks.driver.FindElement(By.XPath("//*[contains(text(),'Invalid')]")).Text;

            Assert.IsTrue(actualValue.Contains(expectedValue)); //expect - true
        }

        [Then(@"I should get access to the portal with title as '(.*)'")]
        public void ThenIShouldGetAccessToThePortalWithTitleAs(string expectedValue)
        {
            Assert.AreEqual(expectedValue, OpenEmrHooks.driver.Title.Trim());
        }

    }
}
