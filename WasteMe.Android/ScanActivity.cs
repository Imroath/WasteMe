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
        public static string appKey = "Aeld711pQ9UqARqoTDtBkSsu6zcXPYf4bmW6kQwshC65HOUIhF4wJ+xAe7ytWnekawydkkgaelpwSu/N5DPMcRomK8AGaMFbiBpRvGt5JWMnTtI0iGAsVi5qnpdwCeBZdDR0iq8Y7hSWyEhMmhnXku02RFeGLrDOkvz5CP4rkQ5ejx2W9aC1BV8K+l7MZAJKlwY3zISkQgG1dutAOyqoLZa/hi6WUZ3zt29/87I1SIw3fcdndFYPtkQDH/uMQ8OEgLSQ8xOwa/Gi3y/Bu1JNZXYzfwwHDjt0/6ImM2j88mkPcwEgtnrDMeSyljBUjmQRtW/YyzCh0dXytkc/GKsZ9WQVElTM+uHoGt9PgFbhaMI2wOfXkrjKSQbyUggffmX5MwHJL9ykWf/ApvE665YxwCR/E9FXoHeLTrslTyAl259AgP9xFrbWdt9GxqeCsASn6iXT560UY3pmTg8YVEPmGPg3wG/m/MNBCjK8cWXYG45gdywNNLUfdVaZFvcW3T5VmNzIErxDE30bdmhtR2bWeXXiW/SnttyHPR3YpqeROIRxVMU6/O4vKDNNAxPczsVn+zpB4Ygbnhiitbn88naIGcz0v4hjPfqsD6CQ3Uo1AXF18I8J8+HijMVI6KwowA2Q8Uyqjyd6aXEBM/zzpSUtnGqwPDD/YVqvTR6tIi2pkhmyvk+Q5IU0qz8Tn2aqa7PAYscyuImxbvN9XaWG63DCvZWyBwGC9lQSuMglXtq1cZHCi/fZC9TLDvVCSxia8z3QecOVkSWBwRyz/iJNSzpxh7rkN0jvcQFUPKri0OJPQnJmBV6ADka3DQ==";

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
                /*RunOnUiThread (() => {
                    AlertDialog alert = new AlertDialog.Builder (this)
                        .SetTitle (code.SymbologyName + " Barcode Detected")
                        .SetMessage (code.Data)
                        .SetPositiveButton("OK", delegate {
                            barcodePicker.StartScanning ();
                        })
                        .SetOnCancelListener(this)
                        .Create ();

                    alert.Show ();
                });*/

                Intent i = new Intent(this, typeof(MainActivity));
                i.AddFlags(ActivityFlags.ReorderToFront);
                i.PutExtra("barcode", code.Data);
                this.StartActivity(i);
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
