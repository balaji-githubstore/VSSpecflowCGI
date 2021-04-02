using NUnit.Framework;
using OpenEmrApplication.Hooks;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Threading;
using TechTalk.SpecFlow;

namespace OpenEmrApplication.Steps
{
    [Binding]
    public class PatientSteps
    {
        [When(@"I do mousehover on patient-client menu")]
        public void WhenIDoMousehoverOnPatient_ClientMenu()
        {
            Actions action = new Actions(OpenEmrHooks.driver);
            action.MoveToElement(OpenEmrHooks.driver.FindElement(By.XPath("//*[text()='Patient/Client']"))).Perform();
        }
        
        [When(@"I click on patients menu")]
        public void WhenIClickOnPatientsMenu()
        {
            OpenEmrHooks.driver.FindElement(By.XPath("//*[text()='Patients']")).Click();
        }
        
        [When(@"I click on Add New Patient")]
        public void WhenIClickOnAddNewPatient()
        {
            OpenEmrHooks.driver.SwitchTo().Frame("fin");
            OpenEmrHooks.driver.FindElement(By.XPath("//*[@id='create_patient_btn1']")).Click();
            //after completing all the operation inside fin frame

            OpenEmrHooks.driver.SwitchTo().DefaultContent();//come out of frame
        }
        
        [When(@"I enter firstname as '(.*)'")]
        public void WhenIEnterFirstnameAs(string p0)
        {
            //move into of frame
            OpenEmrHooks.driver.SwitchTo().Frame(OpenEmrHooks.driver.FindElement(By.XPath("//iframe[@name='pat']")));
            //enter firstname
        }
        
        [When(@"I click on create new patient")]
        public void WhenIClickOnCreateNewPatient()
        {
            // click on create new patient
            OpenEmrHooks.driver.FindElement(By.Id("create")).Click();
            //come out of frame
            OpenEmrHooks.driver.SwitchTo().DefaultContent();
        }

        [When(@"I click on confirm create new patient")]
        public void WhenIClickOnConfirmCreateNewPatient()
        {
            OpenEmrHooks.driver.SwitchTo().Frame("modalframe");

            WebDriverWait wait = new WebDriverWait(OpenEmrHooks.driver, TimeSpan.FromSeconds(30));
            wait.Until(x => x.FindElement(By.XPath("//*[@value='Confirm Create New Patient']")));

            OpenEmrHooks.driver.FindElement(By.XPath("//*[@value='Confirm Create New Patient']")).Click();
            OpenEmrHooks.driver.SwitchTo().DefaultContent();
        }
        
        [When(@"I handle the alert")]
        public void WhenIHandleTheAlert()
        {
            //Thread.Sleep(5000); //not recommeded // convert to explicit or fluent wait selenium
            WebDriverWait wait = new WebDriverWait(OpenEmrHooks.driver, TimeSpan.FromSeconds(30));
            //wait.PollingInterval = TimeSpan.FromSeconds(1);
            wait.Until(x => x.SwitchTo().Alert());

            OpenEmrHooks.driver.SwitchTo().Alert().Accept(); //alert timeout
        }
        
        [When(@"I close the happy birthday popup")]
        public void WhenICloseTheHappyBirthdayPopup()
        {
            if(OpenEmrHooks.driver.FindElements(By.XPath("//*[@class='closeDlgIframe']")).Count>0)
            {
                OpenEmrHooks.driver.FindElement(By.XPath("//*[@class='closeDlgIframe']")).Click();
            } 
        }
        
        [Then(@"I should get the added recorde as '(.*)'")]
        public void ThenIShouldGetTheAddedRecordeAs(string expectedValue)
        {
            OpenEmrHooks.driver.SwitchTo().Frame("pat");
            string actualText= OpenEmrHooks.driver.FindElement(By.XPath("//*[contains(text(),'Medical Record')]")).Text;
            Console.WriteLine(actualText);
            Assert.AreEqual(expectedValue, actualText);
        }


        //[When(@"I fill the form")]
        //public void WhenIFillTheForm(Table table)
        //{
        //    Console.WriteLine(table.RowCount);

        //    Console.WriteLine(table.Rows[0][0]);
        //    Console.WriteLine(table.Rows[0]["dob"]);
        //    Console.WriteLine(table.Rows[0].Count);

        //    //for loop for iterating cell values
        //    for (int c = 0; c < 4; c++)
        //    {
        //        Console.WriteLine(table.Rows[0][c]);
        //    }
        //}

        //[When(@"I fill the form")]
        //public void WhenIFillTheForm(Table table)
        //{
        //    foreach(TableRow row in table.Rows)
        //    {
        //        Console.WriteLine(row[0]);
        //        Console.WriteLine(row["lastname"]);
        //        Console.WriteLine(row["dob"]);
        //        Console.WriteLine(row["gender"]);
        //    }
        //}

        [When(@"I fill the form")]
        public void WhenIFillTheForm(Table table)
        {

            
            OpenEmrHooks.driver.SwitchTo().Frame(OpenEmrHooks.driver.FindElement(By.XPath("//iframe[@name='pat']")));
            //Console.WriteLine(table.Rows[0]["firstname"]);
            //Console.WriteLine(table.Rows[0]["lastname"]);
            //Console.WriteLine(table.Rows[0]["dob"]);
            //Console.WriteLine(table.Rows[0]["gender"]);
            OpenEmrHooks.driver.FindElement(By.Id("form_fname")).SendKeys(table.Rows[0]["firstname"]);
            OpenEmrHooks.driver.FindElement(By.Id("form_lname")).SendKeys(table.Rows[0]["lastname"]);
            OpenEmrHooks.driver.FindElement(By.Id("form_DOB")).SendKeys(table.Rows[0]["dob"]);

            SelectElement selectGender = new SelectElement(OpenEmrHooks.driver.FindElement(By.Id("form_sex")));
            selectGender.SelectByText(table.Rows[0]["gender"]);

            SelectElement selectStatus = new SelectElement(OpenEmrHooks.driver.FindElement(By.Id("form_status")));
            selectStatus.SelectByText(table.Rows[0]["marital_status"]);
        }


        //[When(@"I enter lastname as '(.*)'")]
        //public void WhenIEnterLastnameAs(string p0)
        //{
        //   //enter lastname
        //}

        //[When(@"I enter dob as '(.*)'")]
        //public void WhenIEnterDobAs(string p0)
        //{
        //   //enter dob
        //}

        //[When(@"I select gender as '(.*)'")]
        //public void WhenISelectGenderAs(string p0)
        //{
        //   //select gender
        //}


        [Given(@"I have browser with google map url")]
        public void GivenIHaveBrowserWithGoogleMapUrl()
        {
           
        }

        [When(@"i enter cities")]
        public void WhenIEnterCities(Table table)
        {
           for(int i=0;i<table.RowCount;i++)
            {
                Console.WriteLine(table.Rows[i]["cities"]);
            }

           foreach(TableRow row in table.Rows)
            {
                Console.WriteLine(row["cities"]);
            }
        }

        [Then(@"I should get the km connecting all cities as '(.*)'")]
        public void ThenIShouldGetTheKmConnectingAllCitiesAs(int p0)
        {
            
        }

    }
}
