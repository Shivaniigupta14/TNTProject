using System;
using System.ComponentModel;
using TNTapp.CustomControls;
using TNTapp.iOS.Renderers;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(CustomEntry), typeof(CustomEntryRenderer))]

namespace TNTapp.iOS.Renderers
{
    public class CustomEntryRenderer : EntryRenderer
    {
        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            try
            {
                base.OnElementPropertyChanged(sender, e);
                if (Control != null)
                {
                    Control.BorderStyle = UITextBorderStyle.None;
                    Control.TintColor = UIColor.Black;
                }
            }
            catch (Exception ex)
            {
                var message = ex.Message;
            }
        }
    }
}