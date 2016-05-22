using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using WellDoneItXamFresh.Droid.Renderers;
using WellDoneItXamFresh.Views;

[assembly: Xamarin.Forms.ExportRenderer(typeof(PortraitOrientationPage), typeof(PortraitOrientationPageRenderer))]
namespace WellDoneItXamFresh.Droid.Renderers
{

    public class PortraitOrientationPageRenderer : Xamarin.Forms.Platform.Android.PageRenderer
    {
        private ScreenOrientation _previousOrientation = ScreenOrientation.Unspecified;

        protected override void OnWindowVisibilityChanged(ViewStates visibility)
        {
            base.OnWindowVisibilityChanged(visibility);

            var activity = (Activity)Context;

            if (visibility == ViewStates.Gone)
            {
                // Revert to previous orientation
                activity.RequestedOrientation = _previousOrientation == ScreenOrientation.Unspecified ? ScreenOrientation.Portrait : _previousOrientation;
            }
            else if (visibility == ViewStates.Visible)
            {
                if (_previousOrientation == ScreenOrientation.Unspecified)
                {
                    _previousOrientation = activity.RequestedOrientation;
                }

                activity.RequestedOrientation = ScreenOrientation.Portrait;
            }
        }
    }
}