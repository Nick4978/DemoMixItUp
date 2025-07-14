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

[assembly: ExportRenderer(typeof(CustomEntry), typeof(CustomEntryRenderer))]
namespace MixItUp.Droid.Renderer
{
    class CustomEntryRenderer : EntryRenderer
    {
        public CustomEntryRenderer(Context context) : base(context)
        {

        }

        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.Entry> e)
        {
            base.OnElementChanged(e);
            this.Control.SetSelectAllOnFocus(true);
            if (Control != null)
            {
                var entry = (EditText)Control;
                entry.Background = null;
                Control.SetBackgroundColor(global::Android.Graphics.Color.Transparent);
                Element.BackgroundColor = Color.Transparent;
            }
        }
    }
}