using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Timers;
using Android.App;
using Android.Content;
using Android.Icu.Util;
using Android.Views;
using MixItUp.CustomControls;
using MixItUp.Droid.Renderer;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using View = Android.Views.View;

[assembly: ExportRenderer(typeof(CustomGrid), typeof(CustomGridRenderer))]
namespace MixItUp.Droid.Renderer
{
    public class CustomGridRenderer : ViewRenderer
    {
        private View _view;

        private static int MAX_CLICK_DURATION = 200;
        private long startClickTime;

        public CustomGridRenderer(Context context) : base(context)
        {

        }

        protected override void OnVisibilityChanged(View changedView, ViewStates visibility)
        {
            base.OnVisibilityChanged(changedView, visibility);
            if (_view == null)
            {
                _view = changedView;
                _view.SoundEffectsEnabled = true;
                _view.Touch += OnTouch;
            }
        }

        private void OnTouch(object sender, View.TouchEventArgs e)
        {
            switch (e.Event.Action)
            {
                case MotionEventActions.Down:
                    startClickTime = Calendar.Instance.TimeInMillis;
                    break;
                case MotionEventActions.Up:
                    long clickDuration = Calendar.Instance.TimeInMillis - startClickTime;
                    if (clickDuration < MAX_CLICK_DURATION)
                    {
                        _view.PlaySoundEffect(SoundEffects.Click);
                    }
                    break;
            }
            e.Handled = false;
        }

    }
}