﻿using System;
using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Content;
using WasteMe.Views;
using WasteMe.ViewModels;

namespace WasteMe.Droid
{
    [Activity(Label = "WasteMe", Icon = "@mipmap/icon", Theme = "@style/MainTheme", ScreenOrientation =ScreenOrientation.Portrait,  ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;
            base.OnCreate(savedInstanceState);
            global::Xamarin.Forms.Forms.SetFlags("CollectionView_Experimental");
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            LoadApplication(new App());
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
        protected override void OnNewIntent(Intent intent)
        {
            base.OnNewIntent(intent);
            Intent = intent;
        }
        protected override void OnPostResume()
        {
            base.OnPostResume();
            if (Intent.Extras != null)
            {
                string barcode = Intent.Extras.GetString("barcode");
                Intent.RemoveExtra("fileName");
                if (!string.IsNullOrEmpty(barcode))
                {
                    App.Current.MainPage.Navigation.PushAsync(new ItemDetailPage(new ItemDetailViewModel(barcode)));
                }
            }
        }
    }
}