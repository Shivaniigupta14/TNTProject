using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using TNTapp.CustomControls;
using TNTapp.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(CustomPickerRegular), typeof(CustomPickerRegularRenderer))]

namespace TNTapp.Droid.Renderers
{
    public class CustomPickerRegularRenderer : PickerRenderer
    {
        public CustomPickerRegularRenderer(Context context) : base(context)
        {

        }
        protected override void OnElementChanged(ElementChangedEventArgs<Picker> e)
        {
            base.OnElementChanged(e);
            if (e.OldElement != null || e.NewElement != null)
            {
                var customPicker = e.NewElement as CustomPickerRegular;

                Control.TextSize *= (customPicker.FontSize * 0.01f);
                Control.SetHintTextColor(Android.Graphics.Color.ParseColor(customPicker.PlaceholderColor));
            }
            if (Control != null)
            {
                var customPicker = e.NewElement as CustomPickerRegular;
                Control.Background = null;
                Element.BackgroundColor = Color.Transparent;
                Control.Gravity = GravityFlags.Start;
                Control.TextSize = 16;

            }

        }
    }
}