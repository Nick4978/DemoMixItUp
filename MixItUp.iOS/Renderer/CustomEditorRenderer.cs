using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using MixItUp.CustomControls;
using MixItUp.iOS.Renderer;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(CustomEditor), typeof(CustomEditorRenderer))]
namespace MixItUp.iOS.Renderer
{
   public class CustomEditorRenderer: EditorRenderer
    {
        #region Constructor
        public CustomEditorRenderer()
        {

        }
        #endregion

        #region Define Overrides Here
        protected override void OnElementChanged(ElementChangedEventArgs<Editor> e)
        {
            base.OnElementChanged(e);
            if (Control != null)
            {
                //Element.BackgroundColor = Color.Transparent;
                //Element.PlaceholderColor = Color.Black;
            }
        }
        #endregion
    }
}