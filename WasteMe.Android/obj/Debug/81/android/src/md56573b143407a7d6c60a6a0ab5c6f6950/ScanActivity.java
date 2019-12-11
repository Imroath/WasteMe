package md56573b143407a7d6c60a6a0ab5c6f6950;


public class ScanActivity
	extends android.app.Activity
	implements
		mono.android.IGCUserPeer,
		com.scandit.barcodepicker.OnScanListener,
		android.content.DialogInterface.OnCancelListener
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onCreate:(Landroid/os/Bundle;)V:GetOnCreate_Landroid_os_Bundle_Handler\n" +
			"n_onPause:()V:GetOnPauseHandler\n" +
			"n_onRequestPermissionsResult:(I[Ljava/lang/String;[I)V:GetOnRequestPermissionsResult_IarrayLjava_lang_String_arrayIHandler\n" +
			"n_onResume:()V:GetOnResumeHandler\n" +
			"n_onBackPressed:()V:GetOnBackPressedHandler\n" +
			"n_didScan:(Lcom/scandit/barcodepicker/ScanSession;)V:GetDidScan_Lcom_scandit_barcodepicker_ScanSession_Handler:ScanditBarcodePicker.Android.IOnScanListenerInvoker, ScanditSDK\n" +
			"n_onCancel:(Landroid/content/DialogInterface;)V:GetOnCancel_Landroid_content_DialogInterface_Handler:Android.Content.IDialogInterfaceOnCancelListenerInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\n" +
			"";
		mono.android.Runtime.register ("WasteMe.Droid.ScanActivity, WasteMe.Android", ScanActivity.class, __md_methods);
	}


	public ScanActivity ()
	{
		super ();
		if (getClass () == ScanActivity.class)
			mono.android.TypeManager.Activate ("WasteMe.Droid.ScanActivity, WasteMe.Android", "", this, new java.lang.Object[] {  });
	}


	public void onCreate (android.os.Bundle p0)
	{
		n_onCreate (p0);
	}

	private native void n_onCreate (android.os.Bundle p0);


	public void onPause ()
	{
		n_onPause ();
	}

	private native void n_onPause ();


	public void onRequestPermissionsResult (int p0, java.lang.String[] p1, int[] p2)
	{
		n_onRequestPermissionsResult (p0, p1, p2);
	}

	private native void n_onRequestPermissionsResult (int p0, java.lang.String[] p1, int[] p2);


	public void onResume ()
	{
		n_onResume ();
	}

	private native void n_onResume ();


	public void onBackPressed ()
	{
		n_onBackPressed ();
	}

	private native void n_onBackPressed ();


	public void didScan (com.scandit.barcodepicker.ScanSession p0)
	{
		n_didScan (p0);
	}

	private native void n_didScan (com.scandit.barcodepicker.ScanSession p0);


	public void onCancel (android.content.DialogInterface p0)
	{
		n_onCancel (p0);
	}

	private native void n_onCancel (android.content.DialogInterface p0);

	private java.util.ArrayList refList;
	public void monodroidAddReference (java.lang.Object obj)
	{
		if (refList == null)
			refList = new java.util.ArrayList ();
		refList.add (obj);
	}

	public void monodroidClearReferences ()
	{
		if (refList != null)
			refList.clear ();
	}
}
