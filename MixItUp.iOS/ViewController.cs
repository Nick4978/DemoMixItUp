using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;

namespace MixItUp.iOS
{
    public partial class ViewController : UIViewController
    {
        public ViewController(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            //NavigationController.NavigationBar.TintColor = UIColor.White;

            //NavigationController.NavigationBar.BarTintColor = UIColor.FromRGB(52, 39, 108);
            //NavigationController.NavigationBar.BarStyle = UIBarStyle.Black;
            // Perform any additional setup after loading the view, typically from a nib.
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, , etc that aren't in use.
        }
        public override void LoadView()
        {
            base.LoadView();
        }
        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
        }

        public override void ViewWillDisappear(bool animated)
        {
            base.ViewWillDisappear(animated);
        }
    }
}