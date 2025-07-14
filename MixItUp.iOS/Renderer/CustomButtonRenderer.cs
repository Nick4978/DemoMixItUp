using System;
using System.Collections.Generic;
using System.Runtime.Remoting.Contexts;
using AudioToolbox;
using MixItUp.CustomControls;
using MixItUp.iOS.Renderer;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(CustomButton), typeof(CustomButtonRenderer))]
namespace MixItUp.iOS.Renderer
{
    public class CustomButtonRenderer : ButtonRenderer
    {

        private const string NotificationSoundPath = @"/System/Library/Audio/UISounds/Tock.caf";
        private UIView _view;

        public CustomButtonRenderer()
        {

        }

        protected override void OnElementChanged(ElementChangedEventArgs<Button> e)
        {
            base.OnElementChanged(e);
            if (Control != null)
            {
                if (_view == null)
                {
                    _view = Control;
                    Control.TouchDown += ControlOnTouchDown;
                }
            }
        }

        private void ControlOnTouchDown(object sender, EventArgs e)
        {
            SystemSound notificationSound = SystemSound.FromFile(NotificationSoundPath);
            notificationSound.AddSystemSoundCompletion(SystemSound.Vibrate.PlaySystemSound);
            notificationSound.PlaySystemSound();
        }
    }
}