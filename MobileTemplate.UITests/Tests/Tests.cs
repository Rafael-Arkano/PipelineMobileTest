using System;
using System.IO;
using System.Linq;
using MobileTemplate.UITests.Pages;
using NUnit.Framework;
using Xamarin.UITest;
using Xamarin.UITest.Queries;
using Query = System.Func<Xamarin.UITest.Queries.AppQuery, Xamarin.UITest.Queries.AppQuery>;
namespace MobileTemplate.UITests
{
    [TestFixture(Platform.Android)]
    //[TestFixture(Platform.iOS)]
    public class Tests : BaseTestFixture
    {


        public Tests(Platform platform)
             : base(platform)
        {

        }

        [Test]
        public void AppLaunches()
        {
            //Repl is useful for navigate into elements of the ui
            app.Repl();
            app.Screenshot("First screen.");
        }
        [Test]
        public void DoLogin()
        {
            new LoginPage()
                .EnterCredentials("usuario","pass");

            new SynchronizationPage();
        }
        [Test]
        public void WelcomeTextIsDisplayed()
        {
            AppResult[] results = app.WaitForElement(c => c.Marked("HelloWorldLabel"));
            app.Screenshot("Welcome screen.");

            Assert.IsTrue(results.Any());

            //app.Repl();
            // Create a Xamarin.UITest that will search for our Label based on its AutomationId
            //Query helloWorldLabelQuery = x => x.Marked("HelloWorldLabel");

            //// Perform the Query.
            //// `app.Query` will return an Array all UI Elements that use "HelloWorldLabel"
            //AppResult[] helloWorldLabelQueryResults = app.Query(helloWorldLabelQuery);

            //// Because we've only assigned "HelloWorldLabel" to one UI Element, we are confident that the first result in the
            //string helloWorldLabelText = helloWorldLabelQueryResults?.FirstOrDefault()?.Text;

            //// `Assert.AreEqual` tells Xamarin.UITest to compare the expected string, "Welcome to Xamarin Forms!", with the actual string in helloWorldLabelText
            //// If the strings are equal, our test will pass.
            //// If the strings are not equal, our test will fail.
            //Assert.AreEqual("Hello test", helloWorldLabelText);

        }
    }
}
