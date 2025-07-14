using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

using Foundation;
using MixItUp.CustomControls;
using MixItUp.iOS.Renderer;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(ArmPicker), typeof(ArmPickerRenderer))]
namespace MixItUp.iOS.Renderer
{
    public class ArmPickerRenderer : PickerRenderer
    {
        public ArmPickerRenderer()
        {
        }
        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            Control.Layer.BorderWidth = 0;
            Control.BorderStyle = UITextBorderStyle.None;
           
        }
    }
}