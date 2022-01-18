using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// Aliases Func<AppQuery, AppQuery> with Query
using Query = System.Func<Xamarin.UITest.Queries.AppQuery, Xamarin.UITest.Queries.AppQuery>;

namespace MobileTemplate.UITests.Pages
{
    public class SynchronizationPage : BasePage
    {
        readonly Query SynchroButton;
        protected override PlatformQuery Trait => new PlatformQuery
        {
            Android = x => x.Marked("btnSync"),
            iOS = x => x.Marked("btnSync")
        };

        public SynchronizationPage()
        {
            SynchroButton = x => x.Marked("btnSync");

            app.Tap(SynchroButton);
        }


    }
}
