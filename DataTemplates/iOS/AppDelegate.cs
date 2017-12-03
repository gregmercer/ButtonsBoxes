using System;
using System.Collections.Generic;
using System.Linq;
using CarouselView.FormsPlugin.iOS;
using Foundation;
using UIKit;

using XFShapeView.iOS;

namespace DataTemplates.iOS
{
	[Register ("AppDelegate")]
	public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
	{
		public override bool FinishedLaunching (UIApplication app, NSDictionary options)
		{
			global::Xamarin.Forms.Forms.Init();

            SlideOverKit.iOS.SlideOverKit.Init();
            CarouselViewRenderer.Init();
            ShapeRenderer.Init();
            ZXing.Net.Mobile.Forms.iOS.Platform.Init();

			LoadApplication (new App ());

			return base.FinishedLaunching (app, options);
		}
	}
}

