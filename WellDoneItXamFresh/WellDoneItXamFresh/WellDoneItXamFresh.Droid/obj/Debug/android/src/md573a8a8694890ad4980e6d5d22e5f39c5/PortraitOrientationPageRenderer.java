package md573a8a8694890ad4980e6d5d22e5f39c5;


public class PortraitOrientationPageRenderer
	extends md5b60ffeb829f638581ab2bb9b1a7f4f3f.PageRenderer
	implements
		mono.android.IGCUserPeer
{
	static final String __md_methods;
	static {
		__md_methods = 
			"n_onWindowVisibilityChanged:(I)V:GetOnWindowVisibilityChanged_IHandler\n" +
			"";
		mono.android.Runtime.register ("WellDoneItXamFresh.Droid.Renderers.PortraitOrientationPageRenderer, WellDoneItXamFresh.Droid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", PortraitOrientationPageRenderer.class, __md_methods);
	}


	public PortraitOrientationPageRenderer (android.content.Context p0, android.util.AttributeSet p1, int p2) throws java.lang.Throwable
	{
		super (p0, p1, p2);
		if (getClass () == PortraitOrientationPageRenderer.class)
			mono.android.TypeManager.Activate ("WellDoneItXamFresh.Droid.Renderers.PortraitOrientationPageRenderer, WellDoneItXamFresh.Droid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "Android.Content.Context, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=84e04ff9cfb79065:Android.Util.IAttributeSet, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=84e04ff9cfb79065:System.Int32, mscorlib, Version=2.0.5.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e", this, new java.lang.Object[] { p0, p1, p2 });
	}


	public PortraitOrientationPageRenderer (android.content.Context p0, android.util.AttributeSet p1) throws java.lang.Throwable
	{
		super (p0, p1);
		if (getClass () == PortraitOrientationPageRenderer.class)
			mono.android.TypeManager.Activate ("WellDoneItXamFresh.Droid.Renderers.PortraitOrientationPageRenderer, WellDoneItXamFresh.Droid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "Android.Content.Context, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=84e04ff9cfb79065:Android.Util.IAttributeSet, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=84e04ff9cfb79065", this, new java.lang.Object[] { p0, p1 });
	}


	public PortraitOrientationPageRenderer (android.content.Context p0) throws java.lang.Throwable
	{
		super (p0);
		if (getClass () == PortraitOrientationPageRenderer.class)
			mono.android.TypeManager.Activate ("WellDoneItXamFresh.Droid.Renderers.PortraitOrientationPageRenderer, WellDoneItXamFresh.Droid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "Android.Content.Context, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=84e04ff9cfb79065", this, new java.lang.Object[] { p0 });
	}


	public void onWindowVisibilityChanged (int p0)
	{
		n_onWindowVisibilityChanged (p0);
	}

	private native void n_onWindowVisibilityChanged (int p0);

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
