using System;
using System.ComponentModel;

using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

using DataTemplates.Droid.Renderers;
using DataTemplates.Views;

using Android.Graphics.Drawables;
using Android.Content;

[assembly: ExportRenderer(typeof(TimeSlotFrame), typeof(FixFrameRenderer))]
namespace DataTemplates.Droid.Renderers
{
    public class FixFrameRenderer : FrameRenderer
    {
        public FixFrameRenderer(Context context) : base(context)
        {
        }

        [Obsolete("This constructor is obsolete as of version 2.5. Please use FrameRenderer(Context) instead.")]
        public FixFrameRenderer()
        {
            AutoPackage = false;
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Frame> e)
        {
            base.OnElementChanged(e);
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
        }

        protected override void UpdateBackgroundColor()
        {
            // This method doesn't work well in Xamarin.Forms -Version 2.3.4.247
        }
    }
}