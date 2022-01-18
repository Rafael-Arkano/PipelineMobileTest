using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// Aliases Func<AppQuery, AppQuery> with Query
using Query = System.Func<Xamarin.UITest.Queries.AppQuery, Xamarin.UITest.Queries.AppQuery>;

namespace MobileTemplate.UITests.Pages
{
    public class LoginPage : BasePage
    {
        readonly Query UserField;
        readonly Query PasswordField;
        readonly Query LogInButton;


        protected override PlatformQuery Trait => new PlatformQuery
        {
            Android = x => x.Marked("userNameEntry"),
            iOS = x => x.Marked("userNameEntry")
        };
        public LoginPage()
        {
            // The same for both platforms
            LogInButton = x => x.Marked("btnLogin");

            if (OnAndroid)
            {
                UserField = x => x.Marked("userNameEntry");
                PasswordField = x => x.Marked("passwordEntry");
            }

            if (OniOS)
            {
                UserField = x => x.Id("userNameEntry");
                PasswordField = x => x.Id("passwordEntry");
            }


        }

        public LoginPage EnterCredentials(string user, string pass)
        {
            if (OnAndroid)
            {
                app.EnterText(UserField, user);

                app.EnterText(PasswordField, pass);

                app.DismissKeyboard();

                app.Tap(LogInButton);
            }

            if (OniOS)
            {
               
                app.PressEnter();

             

                app.PressEnter();
            }

            app.Screenshot($"enter credentials \"{user}\" entered \"{pass}\" pass");

            return this;
        }
    }   
}
