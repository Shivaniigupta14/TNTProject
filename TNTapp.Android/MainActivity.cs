using System;
using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.OS;
using Acr.UserDialogs;
using Android;
using AndroidX.Core.Content;
using AndroidX.Core.App;
using Plugin.Permissions;

namespace TNTapp.Droid
{
    [Activity(Label = "TNT_app", Icon = "@drawable/iconlauncher", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        //permission taking from user for storage
        readonly string[] PermissionsLocation =
        {
           Manifest.Permission.ReadExternalStorage,
           Manifest.Permission.WriteExternalStorage
        };
        protected override void OnCreate(Bundle savedInstanceState)
        {

            base.OnCreate(savedInstanceState);
            //persmission of camera
            if (ContextCompat.CheckSelfPermission(this, Manifest.Permission.ReadExternalStorage) == (int)Permission.Granted && ContextCompat.CheckSelfPermission(this, Manifest.Permission.WriteExternalStorage) == (int)Permission.Granted)
            {
                // We have permission, go ahead and use the camera.
            }
            else
            {
                ActivityCompat.RequestPermissions(this, new String[] { Manifest.Permission.ReadExternalStorage, Manifest.Permission.WriteExternalStorage }, 1);
            }
            #region Initializing Nugets  
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            UserDialogs.Init(this);
            // Initializing Popups
            Rg.Plugins.Popup.Popup.Init(this);
           
            #endregion
            LoadApplication(new App());
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}