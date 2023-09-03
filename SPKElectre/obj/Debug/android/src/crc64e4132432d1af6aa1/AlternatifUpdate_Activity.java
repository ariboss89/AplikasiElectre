package crc64e4132432d1af6aa1;


public class AlternatifUpdate_Activity
	extends android.app.Activity
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onCreate:(Landroid/os/Bundle;)V:GetOnCreate_Landroid_os_Bundle_Handler\n" +
			"";
		mono.android.Runtime.register ("SPKElectre.Activities.UpdateActivity.AlternatifUpdate_Activity, SPKElectre", AlternatifUpdate_Activity.class, __md_methods);
	}


	public AlternatifUpdate_Activity ()
	{
		super ();
		if (getClass () == AlternatifUpdate_Activity.class) {
			mono.android.TypeManager.Activate ("SPKElectre.Activities.UpdateActivity.AlternatifUpdate_Activity, SPKElectre", "", this, new java.lang.Object[] {  });
		}
	}


	public void onCreate (android.os.Bundle p0)
	{
		n_onCreate (p0);
	}

	private native void n_onCreate (android.os.Bundle p0);

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
