using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace MixItUp.CustomControls
{
    public class AdMobView : View
    {
        // public AdMobView() { }
        public static readonly BindableProperty AdUnitIdProperty = BindableProperty.Create(
                     nameof(AdUnitId),
                     typeof(string),
                     typeof(AdMobView),
                     string.Empty);

        public string AdUnitId
        {
            get => (string)GetValue(AdUnitIdProperty);
            set => SetValue(AdUnitIdProperty, value);
        }
    }
}
