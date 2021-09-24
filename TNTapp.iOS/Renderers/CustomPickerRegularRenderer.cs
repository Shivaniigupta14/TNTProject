using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using CoreGraphics;
using Foundation;
using TNTapp.CustomControls;
using TNTapp.iOS.Renderers;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(CustomPickerRegular), typeof(CustomPickerRegularRenderer))]

namespace TNTapp.iOS.Renderers
{
    public class CustomPickerRegularRenderer : PickerRenderer
    {
        //public static void Init() { }
        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            try
            {
                base.OnElementPropertyChanged(sender, e);
                if (Control != null)
                {
                    Control.Layer.BorderWidth = 0;
                    Control.Font = UIFont.SystemFontOfSize(16);
                    Control.BorderStyle = UITextBorderStyle.None;
                    Control.LeftView = new UIView(new CGRect(0, 0, 5, 0));
                    Control.LeftViewMode = UITextFieldViewMode.Always;
                }
            }
            catch (Exception ex) { }
        }
    }

}
