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
using MixItUp.CustomControls;
using MixItUp.Droid.Renderer;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(ArmPicker), typeof(AmrPickerRenderer))]
namespace MixItUp.Droid.Renderer
{
    public class AmrPickerRenderer : PickerRenderer
    {
        public AmrPickerRenderer(Context context) : base(context)
        {
            if (Control != null)
            {
            }
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.Picker> e)
        {
            base.OnElementChanged(e);
            if (Control != null)
            {

                if (e.OldElement == null)
                {
                    Control.Background = null;
                    Control.TextSize = 14f;
                    var armPicker = e.NewElement as ArmPicker;
                    var layoutParams = new MarginLayoutParams(Control.LayoutParameters);
                    layoutParams.SetMargins(0, 0, 0, 0);
                    LayoutParameters = layoutParams;
                    Control.LayoutParameters = layoutParams;
                    Control.SetPadding(0, 0, 0, 0);
                    SetPadding(0, 0, 0, 0); 
                }

            }
        }
    }
}