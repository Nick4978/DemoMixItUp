using System;
using System.Collections.Generic;
using Android.App;
using Android.Content;
using Android.Views;
using MixItUp.CustomControls;
using MixItUp.Droid.Renderer;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using View = Android.Views.View;

[assembly: ExportRenderer(typeof(NativeListView), typeof(NativeListViewRenderer))]
namespace MixItUp.Droid.Renderer
{
    public class NativeListViewRenderer : ListViewRenderer
    {
        private View _view;
        public NativeListViewRenderer(Context context) : base(context)
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

        private void OnTouch(object sender, TouchEventArgs e)
        {
            if (e.Event.Action == MotionEventActions.Down)
            {
                _view.PlaySoundEffect(SoundEffects.Click);
            }
        }
    }
}