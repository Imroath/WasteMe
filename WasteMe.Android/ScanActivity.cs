using System;
using Android;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Views;
using ScanditBarcodePicker.Android;
using ScanditBarcodePicker.Android.Recognition;

namespace WasteMe.Droid
{
    [Activity (Label = "ScanActivity")]
    public class ScanActivity : Activity, IOnScanListener, IDialogInterfaceOnCancelListener
    {
        public static string appKey = "AcHd7CZpOPMgAqAj4UHMbckJGXaUCWTHH0WWprZ9NCCafa2ZuXELhCIRjguocU7miliNHV12pKvlY63Ch3n+yAdRFFX0QAPKNzlYkppDDyRJHoObaWkASodfsqQtVxex7FML4U9Ozgwzf84ianBznyNMV4/OLvviT1f3thxB3/xreRgErEafwrt1KUuLX0lh0mmFSLpt1V94cDnnTH0gfBhppfFmR6LOgES3H4tQqSnSRE4D1n4S/MVCtU1RSwQdR2Y5kN90mnD9K5GKoWJj+lVSYqSNRlKrx38YcMJdZdmkZz5wdUYYer9hoQl7baucHEzOvxRZJgtIWASbdXiIENgXjpd1Tk5qmy4j9MZWMXgRTRHMCmhk8vFZ8DtccBf0cWSbkD1JsUkhCiz5ikgeXO9GWA6CaeuSAXx3uokQisvxVpTZlkxchuVIUbvdIvD9gGj21l9EpDQwHNlsCm9smdxoAKD5D4QpynXe9LN6Cdo/QsK/3kgaUQcWd3qwZcVtVwZXONkSCXh5D0gI3cUO33Gqk8YXRjpC+cVPqUxS4W89jKm04Y4mc/LGUxQNKLD4mMtBC+UwKceUQXaJqyDoe5GtpPA+t86bPSY22Hwh9jAJFAtBthYS/qzjQISd6bUN3ei/Efhu+SwDNWWkJ0pk1NRe9sqtObYrIqPKilfRZGEihy0kd1Dvy0lPOI39htvvFsVE8WFJI8pxXLjVECMSj8AQM2dmrOZ3s6IpgIDupHnj7i+LgBCTOxj1ODowVcgz0W5Oi3onHD8y6tsF8LsC1aLCQwZ0p0HBrkTzL0r/8IiFaiJfdtqjJcklzP3pWFwi9V4+Pvpb30OZ8aEeEN113sT6BTWjVnIb4hoRuUJvYzMXqqyejt495x8abJsq8XjzcpbqlSLWdaoLQ8qs1d2ejO7xkfeKVTQDiJDtcdEGaaRO7yyr6Qhf8/oFRmOvouwnAXo8rzO9LbQuPKwdDDC67/knsOjgI5Zu5xBb7PyMm0EdUIGz/7z2Q5yvQ9fQM95sH5fZwOY4H4gZQO94Uki8TuGeLDBw4fH1KZQYNocM9ZuwYNOcroQbprI8fC+/zs0O7K19ZY1Sn7DbPmVeHRY6TM19zEEc5ybDLp7f/VQjuFZ3MzxDeVCxI58ImVLQZlzrVTXXk3TSF1JiuYPWKKLMVTYg2bo2bUHhZX2dyw2uy8rR9o5E7PhFnfw021M=";

        private const int CameraPermissionRequest = 0;

        private BarcodePicker barcodePicker;
        private bool deniedCameraAccess = false;
        private bool paused = true;


        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            RequestWindowFeature(WindowFeatures.NoTitle);
            Window.SetFlags(WindowManagerFlags.Fullscreen, WindowManagerFlags.Fullscreen);

            // Set the app key before instantiating the picker.
            ScanditLicense.AppKey = appKey;

            InitializeAndStartBarcodeScanning();
        }

        protected override void OnPause()
        {
            base.OnPause();

            // Call GC.Collect() before stopping the scanner as the garbage collector for some reason does not
            // collect objects without references asap but waits for a long time until finally collecting them.
            GC.Collect();
            barcodePicker.StopScanning();
            paused = true;
        }

        void GrantCameraPermissionsThenStartScanning()
        {
            if (CheckSelfPermission(Manifest.Permission.Camera) != (int)Permission.Granted)
            {
                if (deniedCameraAccess == false)
                {
                    // It's pretty clear for why the camera is required. We don't need to give a
                    // detailed reason.
                    RequestPermissions(new String[] { Manifest.Permission.Camera }, CameraPermissionRequest);
                }

            }
            else
            {
                Console.WriteLine("starting scanning");
                // We already have the permission.
                barcodePicker.StartScanning();
            }
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Permission[] grantResults)
        {
            if (requestCode == CameraPermissionRequest)
            {
                if (grantResults.Length > 0 && grantResults[0] == Permission.Granted)
                {
                    deniedCameraAccess = false;
                    if (!paused)
                    {
                        barcodePicker.StartScanning();
                    }
                }
                else
                {
                    deniedCameraAccess = true;
                }
                return;
            }
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        protected override void OnResume()
        {
            base.OnResume();

            paused = false;
            // Handle permissions for Marshmallow and onwards.
            if ((int)Build.VERSION.SdkInt >= 23)
            {
                GrantCameraPermissionsThenStartScanning();
            }
            else
            {
                // Once the activity is in the foreground again, restart scanning.
                barcodePicker.StartScanning();
            }
        }

        void InitializeAndStartBarcodeScanning()
        {
            // The scanning behavior of the barcode picker is configured through scan
            // settings. We start with empty scan settings and enable a very generous
            // set of symbologies. In your own apps, only enable the symbologies you
            // actually need.
            ScanSettings settings = ScanSettings.Create ();
            int[] symbologiesToEnable = new int[] {
                Barcode.SymbologyEan13,
                Barcode.SymbologyEan8,
                Barcode.SymbologyUpca,
                Barcode.SymbologyDataMatrix,
                Barcode.SymbologyQr,
                Barcode.SymbologyCode39,
                Barcode.SymbologyCode128,
                Barcode.SymbologyInterleaved2Of5,
                Barcode.SymbologyUpce
            };

            foreach (int symbology in symbologiesToEnable)
            {
                settings.SetSymbologyEnabled (symbology, true);
            }

            // Some 1d barcode symbologies allow you to encode variable-length data. By default, the
            // Scandit BarcodeScanner SDK only scans barcodes in a certain length range. If your
            // application requires scanning of one of these symbologies, and the length is falling
            // outside the default range, you may need to adjust the "active symbol counts" for this
            // symbology. This is shown in the following few lines of code.

            SymbologySettings symSettings = settings.GetSymbologySettings(Barcode.SymbologyCode128);
            short[] activeSymbolCounts = new short[] {
                7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20
            };
            symSettings.SetActiveSymbolCounts(activeSymbolCounts);
            // For details on defaults and how to calculate the symbol counts for each symbology, take
            // a look at http://docs.scandit.com/stable/c_api/symbologies.html.

            barcodePicker = new BarcodePicker (this, settings);

            // Set listener for the scan event.
            barcodePicker.SetOnScanListener (this);

            // Show the scan user interface
            SetContentView (barcodePicker);
        }

        public void DidScan(IScanSession session)
        {
            if (session.NewlyRecognizedCodes.Count > 0) {
                Barcode code = session.NewlyRecognizedCodes [0];
                Console.WriteLine ("barcode scanned: {0}, '{1}'", code.SymbologyName, code.Data);

                // Call GC.Collect() before stopping the scanner as the garbage collector for some reason does not
                // collect objects without references asap but waits for a long time until finally collecting them.
                GC.Collect ();

                // Stop the scanner directly on the session.
                session.StopScanning ();

                // If you want to edit something in the view hierarchy make sure to run it on the UI thread.
                RunOnUiThread (() => {
                    AlertDialog alert = new AlertDialog.Builder (this)
                        .SetTitle (code.SymbologyName + " Barcode Detected")
                        .SetMessage (code.Data)
                        .SetPositiveButton("OK", delegate {
                            barcodePicker.StartScanning ();
                        })
                        .SetOnCancelListener(this)
                        .Create ();

                    alert.Show ();
                });
            }
        }

        public void OnCancel(IDialogInterface dialog) {
            barcodePicker.StartScanning ();
        }

        public override void OnBackPressed ()
        {
            base.OnBackPressed ();
            Finish ();
        }
    }
}
