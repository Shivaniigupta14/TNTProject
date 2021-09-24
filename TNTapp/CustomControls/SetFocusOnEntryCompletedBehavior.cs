using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace TNTapp.CustomControls
{
    public class SetFocusOnEntryCompletedBehavior : Behavior
    {
        public static readonly BindableProperty TargetElementProperty
           = BindableProperty.Create(nameof(TargetElement), typeof(VisualElement), typeof(SetFocusOnEntryCompletedBehavior));

        public VisualElement TargetElement
        {
            get => (VisualElement)GetValue(TargetElementProperty);
            set => SetValue(TargetElementProperty, value);
        }

        protected override void OnAttachedTo(BindableObject bindable)
        {
            base.OnAttachedTo(bindable);

            if (bindable is Entry entry)
                entry.Completed += Entry_Completed;
        }

        protected override void OnDetachingFrom(BindableObject bindable)
        {
            if (bindable is Entry entry)
                entry.Completed -= Entry_Completed;

            base.OnDetachingFrom(bindable);
        }

        private void Entry_Completed(object sender, EventArgs e)
        {
            TargetElement?.Focus();
        }
        
    }
}
