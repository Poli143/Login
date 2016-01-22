package md533583cc845b761e36e0b683119bc5692;


public class WyborKategori
	extends android.app.Activity
	implements
		mono.android.IGCUserPeer
{
	static final String __md_methods;
	static {
		__md_methods = 
			"n_onCreate:(Landroid/os/Bundle;)V:GetOnCreate_Landroid_os_Bundle_Handler\n" +
			"";
		mono.android.Runtime.register ("Login.WyborKategori, Login, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", WyborKategori.class, __md_methods);
	}


	public WyborKategori () throws java.lang.Throwable
	{
		super ();
		if (getClass () == WyborKategori.class)
			mono.android.TypeManager.Activate ("Login.WyborKategori, Login, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public void onCreate (android.os.Bundle p0)
	{
		n_onCreate (p0);
	}

	private native void n_onCreate (android.os.Bundle p0);

	java.util.ArrayList refList;
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
