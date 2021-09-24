using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace TNTapp.CustomControls
{
    //Use Date CustomPickerRegular Control In PCL
    public class CustomPickerRegular : Picker
    {
        //Define constructor
        public CustomPickerRegular()
        { 
        }
        public static readonly BindableProperty FontSizeProperty =  BindableProperty.Create(nameof(FontSize), typeof(Int32), typeof(CustomPickerRegular), 16, BindingMode.TwoWay);

        public Int32 FontSize
        {
            set
            { 
                SetValue(FontSizeProperty, value);
            }
            get
            { 
                return (Int32)GetValue(FontSizeProperty);
            }
        }

        public static BindableProperty PlaceholderColorProperty = BindableProperty.Create(nameof(PlaceholderColor), typeof(string), typeof(CustomPickerRegular), "#00487A", BindingMode.TwoWay);

        public string PlaceholderColor
        {
            get
            { 
                return (string)GetValue(PlaceholderColorProperty); 
            }
            set
            { 
                SetValue(PlaceholderColorProperty, value);
            }
        }

        public static BindableProperty TitleColorProperty =
           BindableProperty.Create(nameof(TitleColor), typeof(string), typeof(CustomPickerRegular), "#00487A", BindingMode.TwoWay);
        public string TitleColor
        {
            get
            { 
                return (string)GetValue(TitleColorProperty); 
            }
            set 
            { 
                SetValue(TitleColorProperty, value);
            }
        }


    }
}
