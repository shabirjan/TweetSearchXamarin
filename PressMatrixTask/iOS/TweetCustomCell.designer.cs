// WARNING
//
// This file has been generated automatically by Xamarin Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace PressMatrixTask.iOS
{
    [Register ("TweetCustomCell")]
    partial class TweetCustomCell
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel CellTitle { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIActivityIndicatorView imgIndicator { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIImageView profileImage { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (CellTitle != null) {
                CellTitle.Dispose ();
                CellTitle = null;
            }

            if (imgIndicator != null) {
                imgIndicator.Dispose ();
                imgIndicator = null;
            }

            if (profileImage != null) {
                profileImage.Dispose ();
                profileImage = null;
            }
        }
    }
}