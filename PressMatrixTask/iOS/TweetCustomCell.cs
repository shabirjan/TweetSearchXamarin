using Foundation;
using System;
using UIKit;

namespace PressMatrixTask.iOS
{
	public partial class TweetCustomCell : UITableViewCell
	{
		#region Constructor
		public TweetCustomCell(IntPtr handle) : base(handle)
		{
		}
		#endregion

		#region Computed Properties
		public UIActivityIndicatorView indicator
		{
			get { return imgIndicator; }
			set { imgIndicator = value; }
		}
		public string Title
		{
			get { return CellTitle.Text; }
			set { CellTitle.Text = value; }
		}
		public UIImageView Image
		{
			get { return profileImage; }
			set { profileImage = value; }
		}
		#endregion
	}
}