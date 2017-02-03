using System;
using System.Collections.Generic;
using UIKit;
using SDWebImage;
using Foundation;
using System.Diagnostics.Contracts;
namespace PressMatrixTask.iOS
{
	public class TableSource : UITableViewDataSource
	{

		#region Private Variables
		private ViewController Controller;
		#endregion

		#region Computed Properties
		public List<Status> allTweets;
		public string CellID
		{
			get { return "TweetCell"; }
		}
		#endregion

		#region Constructors


		public TableSource(ViewController controller)
		{
			// Initialize
			this.Controller = controller;

		}
		public TableSource(List<Status> objects)
		{
			allTweets = objects;
		}
		#endregion

		#region Override Methods
		public override nint RowsInSection(UITableView tableView, nint section)
		{
			return allTweets.Count;
		}
		public override UITableViewCell GetCell(UITableView tableView, Foundation.NSIndexPath indexPath)
		{
			Contract.Ensures(Contract.Result<UITableViewCell>() != null);
			var cell = tableView.DequeueReusableCell(CellID, indexPath) as TweetCustomCell;

			Status currentObject = allTweets[indexPath.Row];

			cell.Title = currentObject.Text;

			if (currentObject.User != null)
			{
				cell.Image.SetImage(url: new NSUrl(currentObject.User.ProfileImageUrl), completedBlock: (image, data, error, finished) =>
				 {
					 InvokeOnMainThread(() =>
					 {
						 cell.SetNeedsLayout();
						 cell.indicator.StopAnimating();
					 });


				 });
			}

			return cell;
		}
		#endregion
	}
}
