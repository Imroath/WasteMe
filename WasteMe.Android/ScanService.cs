using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using WasteMe;
using WasteMe.Droid;
using Xamarin.Forms;

[assembly: Xamarin.Forms.Dependency(typeof(ScanService))]
namespace WasteMe.Droid
{
    public class ScanService : IScanService
    {
        public ScanService() { }
        public void StartScan()
        {
            var intent = new Intent(Android.App.Application.Context, typeof(ScanActivity));
            Android.App.Application.Context.StartActivity(intent);
        }
    }
}