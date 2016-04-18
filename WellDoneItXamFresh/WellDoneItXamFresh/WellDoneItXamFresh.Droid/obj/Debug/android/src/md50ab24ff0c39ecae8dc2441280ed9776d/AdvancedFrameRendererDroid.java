package md50ab24ff0c39ecae8dc2441280ed9776d;


public class AdvancedFrameRendererDroid
	extends md5b60ffeb829f638581ab2bb9b1a7f4f3f.VisualElementRenderer_1
	implements
		mono.android.IGCUserPeer
{
	static final String __md_methods;
	static {
		__md_methods = 
			"n_onDraw:(Landroid/graphics/Canvas;)V:GetOnDraw_Landroid_graphics_Canvas_Handler\n" +
			"";
		mono.android.Runtime.register ("FreshEssentials.Droid.AdvancedFrameRendererDroid, FreshEssentials.Droid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", AdvancedFrameRendererDroid.class, __md_methods);
	}


	public AdvancedFrameRendererDroid (android.content.Context p0, android.util.AttributeSet p1, int p2) throws java.lang.Throwable
	{
		super (p0, p1, p2);
		if (getClass () == AdvancedFrameRendererDroid.class)
			mono.android.TypeManager.Activate ("FreshEssentials.Droid.AdvancedFrameRendererDroid, FreshEssentials.Droid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "Android.Content.Context, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=84e04ff9cfb79065:Android.Util.IAttributeSet, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=84e04ff9cfb79065:System.Int32, mscorlib, Version=2.0.5.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e", this, new java.lang.Object[] { p0, p1, p2 });
	}


	public AdvancedFrameRendererDroid (android.content.Context p0, android.util.AttributeSet p1) throws java.lang.Throwable
	{
		super (p0, p1);
		if (getClass () == AdvancedFrameRendererDroid.class)
			mono.android.TypeManager.Activate ("FreshEssentials.Droid.AdvancedFrameRendererDroid, FreshEssentials.Droid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "Android.Content.Context, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=84e04ff9cfb79065:Android.Util.IAttributeSet, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=84e04ff9cfb79065", this, new java.lang.Object[] { p0, p1 });
	}


	public AdvancedFrameRendererDroid (android.content.Context p0) throws java.lang.Throwable
	{
		super (p0);
		if (getClass () == AdvancedFrameRendererDroid.class)
			mono.android.TypeManager.Activate ("FreshEssentials.Droid.AdvancedFrameRendererDroid, FreshEssentials.Droid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "Android.Content.Context, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=84e04ff9cfb79065", this, new java.lang.Object[] { p0 });
	}


	public void onDraw (android.graphics.Canvas p0)
	{
		n_onDraw (p0);
	}

	private native void n_onDraw (android.graphics.Canvas p0);

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
