﻿using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace MixItUp.CustomControls
{
    public class CustomFrame : Frame
    {
        public static new readonly BindableProperty CornerRadiusProperty = BindableProperty.Create(nameof(CustomFrame), typeof(CornerRadius), typeof(CustomDatePicker));

        public CustomFrame()
        {
            // MK Clearing default values (e.g. on iOS it's 5)
            base.CornerRadius = 0;
        }

        public new CornerRadius CornerRadius
        {
            get => (CornerRadius)GetValue(CornerRadiusProperty);
            set => SetValue(CornerRadiusProperty, value);
        }

    }
}
