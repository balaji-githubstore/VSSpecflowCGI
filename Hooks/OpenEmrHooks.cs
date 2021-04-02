using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;
using TechTalk.SpecFlow;

namespace OpenEmrApplication.Hooks
{
    [Binding]
    class OpenEmrHooks
    {
        public static IWebDriver driver;

        [AfterScenario]
        public void TearDown()
        {
            OpenEmrHooks.driver.Quit();
        }

        public static ExtentReports extent;

        private static ExtentTest feature;
        private static ExtentTest scenario;
        private static string featureTitle;

        private readonly ScenarioContext scenarioContext;
        private readonly FeatureContext featureContext;

        public OpenEmrHooks(FeatureContext featureContext, ScenarioContext scenarioContext)
        {
            if (scenarioContext == null) throw new ArgumentNullException("scenarioContext");
            this.scenarioContext = scenarioContext;
            if (featureContext == null) throw new ArgumentNullException("FeatureContext");
            this.featureContext = featureContext;
        }


        [BeforeTestRun]
        public static void BeforeTestRun()
        {
            string reportPath = @"D:\Report\"; //where to save
            var report = new ExtentHtmlReporter(reportPath);
            report.Config.Theme = AventStack.ExtentReports.Reporter.Configuration.Theme.Standard;

            extent = new ExtentReports();
            extent.AttachReporter(report); //accumulate the html while running the scenarios
        }

        [AfterTestRun]
        public static void AfterTestRun()
        {
            extent.Flush(); //generate html with accumulated html
        }

        [BeforeScenario]
        public void BeforeScenario()
        {
            if (featureTitle != featureContext.FeatureInfo.Title)
            {
                feature = extent.CreateTest(new GherkinKeyword("Feature"), "Feature: " + featureContext.FeatureInfo.Title);
            }
            featureTitle = featureContext.FeatureInfo.Title;
            scenario = feature.CreateNode(new GherkinKeyword("Scenario"), "Scenario: " + scenarioContext.ScenarioInfo.Title);
        }

        [AfterStep]
        public void AfterStep()
        {
            Console.WriteLine(scenarioContext.StepContext.StepInfo.Text);
            var stepType = scenarioContext.StepContext.StepInfo.StepDefinitionType.ToString();
            if (scenarioContext.TestError == null)
            {
                scenario.CreateNode(new GherkinKeyword(stepType),  scenarioContext.StepContext.StepInfo.Text);
            }
            else if (scenarioContext.TestError != null)
            {
                scenario.CreateNode(new GherkinKeyword(stepType),  scenarioContext.StepContext.StepInfo.Text).Fail(scenarioContext.TestError.Message);
            }
        }
    }
}
