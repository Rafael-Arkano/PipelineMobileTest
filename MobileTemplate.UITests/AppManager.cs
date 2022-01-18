using System;
using Xamarin.UITest;
using Xamarin.UITest.Queries;

namespace MobileTemplate.UITests
{
    public class AppManager
    {
        //C:\Users\AndreaCiullo\Binaries\mobileTemplate.apk
        const string ApkPath = "../../../Binaries/mobileTemplate.apk";
        const string AppPath = "../../../Binaries/mobileTemplate.app";
        const string AppPackageName = "com.companyname.mobiletemplate.dev";

        static IApp app;
        public static IApp App
        {
            get
            {
                if (app == null)
                    throw new NullReferenceException("'AppManager.App' not set. Call 'AppManager.StartApp()' before trying to access it.");
                return app;
            }
        }

        static Platform? platform;
        public static Platform Platform
        {
            get
            {
                if (platform == null)
                    throw new NullReferenceException("'AppManager.Platform' not set.");
                return platform.Value;
            }

            set
            {
                platform = value;
            }
        }

        public static void StartApp()
        {
            if (Platform == Platform.Android)
            {
                app = ConfigureApp
                    .Android
                    //used to run in the installed device
                    .InstalledApp(AppPackageName)
                    // Used to run a .apk file:
                    //.ApkFile(ApkPath)
                    .StartApp();
            }

            if (Platform == Platform.iOS)
            {
                app = ConfigureApp
                    .iOS
                    // Used to run a .app file on an ios simulator:
                    .AppBundle(AppPath)
                    // Used to run a .ipa file on a physical ios device:
                    //.InstalledApp(ipaBundleId)
                    .StartApp();
            }
        }

    }
}