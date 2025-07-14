using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Timers;
using AudioToolbox;
using Foundation;
using MixItUp.CustomControls;
using MixItUp.iOS.Renderer;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(CustomGrid), typeof(CustomGridRenderer))]
namespace MixItUp.iOS.Renderer
{
    public class CustomGridRenderer : ViewRenderer
    {
        private const string NotificationSoundPath = @"/System/Library/Audio/UISounds/Tock.caf";
        private UIView _view;

        private static int MAX_CLICK_DURATION = 200;
        private long startClickTime;
        private Stopwatch _stopWatch = new Stopwatch();

        public CustomGridRenderer()
        {
            var gesture = new UITapGestureRecognizer();
            gesture.AddTarget(() => Touch(gesture));
            AddGestureRecognizer(gesture);
        }

        private void Touch(UITapGestureRecognizer recognizer)
        {
            switch (recognizer.State)
            {
                case UIGestureRecognizerState.Possible:
                    break;
                case UIGestureRecognizerState.Began:
                    _stopWatch.Start();
                    break;
                case UIGestureRecognizerState.Changed:
                    break;
                case UIGestureRecognizerState.Ended:

                    long clickDuration = _stopWatch.ElapsedMilliseconds;
                    _stopWatch.Stop();
                    if (clickDuration < MAX_CLICK_DURATION)
                    {
                        SystemSound notificationSound = SystemSound.FromFile(NotificationSoundPath);
                        notificationSound.AddSystemSoundCompletion(SystemSound.Vibrate.PlaySystemSound);
                        notificationSound.PlaySystemSound();
                    }
                    break;
                case UIGestureRecognizerState.Cancelled:
                    break;
                case UIGestureRecognizerState.Failed:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        protected override void OnElementChanged(ElementChangedEventArgs<View> e)
        {
            base.OnElementChanged(e);
            if (Control != null)
            {
                if (_view == null)
                {
                    _view = Control;
                }
            }
        }

        public override void TouchesBegan(NSSet touches, UIEvent evt)
        {
            base.TouchesBegan(touches, evt);
            switch (evt.Type)
            {
                case UIEventType.Touches:
                    break;
                case UIEventType.Motion:
                    break;
                case UIEventType.RemoteControl:
                    break;
                case UIEventType.Presses:
                    SystemSound notificationSound = SystemSound.FromFile(NotificationSoundPath);
                    notificationSound.AddSystemSoundCompletion(SystemSound.Vibrate.PlaySystemSound);
                    notificationSound.PlaySystemSound();
                    break;
            }
        }

        public override void TouchesEnded(NSSet touches, UIEvent evt)
        {
            base.TouchesEnded(touches, evt);
            switch (evt.Type)
            {
                case UIEventType.Touches:
                    break;
                case UIEventType.Motion:
                    break;
                case UIEventType.RemoteControl:
                    break;
                case UIEventType.Presses:
                    SystemSound notificationSound = SystemSound.FromFile(NotificationSoundPath);
                    notificationSound.AddSystemSoundCompletion(SystemSound.Vibrate.PlaySystemSound);
                    notificationSound.PlaySystemSound();
                    break;
            }
        }
    }
}