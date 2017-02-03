using System;
using UIKit;
namespace PressMatrixTask.iOS
{
	public class TableDelegate : UITableViewDelegate
	{
		#region Private Variables
		private ViewController Controller;
		#endregion

		#region Constructors
		public TableDelegate()
		{
		}
		public TableDelegate(ViewController controller)
		{
			// Initialize
			this.Controller = controller;
		}
		#endregion
		#region Override Methods
		public override nfloat EstimatedHeight(UITableView tableView, Foundation.NSIndexPath indexPath)
		{
			return 40f;
		}

		public override void RowSelected(UITableView tableView, Foundation.NSIndexPath indexPath)
		{
			// Output selected row
			Console.WriteLine("Row selected: {0}", indexPath.Row);
		}
		#endregion
	}
}
