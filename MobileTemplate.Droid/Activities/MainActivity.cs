namespace MobileTemplate.Droid.Activities
{
    using System.Globalization;
    using System.Threading;
    using MvvmCross.Forms.Platforms.Android.Views;
    using Core.ViewModels;
    using Android.App;
    using Android.Content.PM;
    using Android.OS;

    /// <summary>
    /// Main activity 
    /// </summary>
    [Activity(
        Theme = "@style/AppTheme",
        Label = "@string/AppName",
        Icon = "@mipmap/ic_launcher",
        ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation,
        LaunchMode = LaunchMode.SingleTask)]
    public class MainActivity : MvxFormsAppCompatActivity<StartUpViewModel>
    {
        /// <summary>
        /// Setups resources
        /// </summary>
        protected override void OnCreate(Bundle bundle)
        {
            ToolbarResource = Resource.Layout.Toolbar;
            TabLayoutResource = Resource.Layout.Tabbar;
            base.OnCreate(bundle);
            FFImageLoading.Forms.Platform.CachedImageRenderer.Init(enableFastRenderer: true);
        }


        /// <summary>
        /// Forms and plugins initialization
        /// </summary>        
        public override void InitializeForms(Bundle bundle)
        {
            base.InitializeForms(bundle);
            Xamarin.Forms.Forms.Init(this, bundle);

        }


        /// <summary>
        /// OnResume
        /// Forces the culture to es-UY
        /// </summary>
        protected override void OnResume()
        {
            var selectedCulture = new CultureInfo("es-UY");
            Thread.CurrentThread.CurrentCulture = selectedCulture;
            base.OnResume();
        }


        /// <summary>
        /// Handles the permissions result for Xamrin Essentials
        /// </summary>
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}

