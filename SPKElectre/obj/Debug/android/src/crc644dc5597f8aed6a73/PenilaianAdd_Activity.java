package crc644dc5597f8aed6a73;


public class PenilaianAdd_Activity
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
		mono.android.Runtime.register ("SPKElectre.Activities.AddActivity.PenilaianAdd_Activity, SPKElectre", PenilaianAdd_Activity.class, __md_methods);
	}


	public PenilaianAdd_Activity ()
	{
		super ();
		if (getClass () == PenilaianAdd_Activity.class) {
			mono.android.TypeManager.Activate ("SPKElectre.Activities.AddActivity.PenilaianAdd_Activity, SPKElectre", "", this, new java.lang.Object[] {  });
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
