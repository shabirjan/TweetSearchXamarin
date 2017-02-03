using System;
using System.Collections.Generic;
using System.IO;


using UIKit;
using MBProgressHUD;

namespace PressMatrixTask.iOS
{
	public partial class ViewController : UIViewController
	{
		public MTMBProgressHUD hud;
		public string bearerToken;
		public SearchManager searchManager;
		#region Computed Properties
		public TableSource DataSource
		{
			get { return tweetTableView.DataSource as TableSource; }
		}

		public TableDelegate TableDelegate
		{
			get { return tweetTableView.Delegate as TableDelegate; }
		}
		#endregion

		public ViewController(IntPtr handle) : base(handle)
		{
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			// Perform any additional setup after loading the view, typically from a nib.
			bearerToken = "";
			var appDlg = (AppDelegate)UIApplication.SharedApplication.Delegate;

			searchManager = new SearchManager(appDlg.conn);



			loadTweets();

		}


		public async void loadTweets()
		{
			hud = new MTMBProgressHUD(View)
			{
				LabelText = "Searching Twitter...",
				RemoveFromSuperViewOnHide = true
			};
			View.AddSubview(hud);

			var tweets = new List<Status>();
			if (Reachability.IsHostReachable("http://google.com"))
			{

				bearerToken = await searchManager.getTwitterAuthToken();
				tweets = await searchManager.FetchTweetsOnline(bearerToken, 20, "xamarin");
			}
			if (tweets.Count == 0)
			{
				tweets = await searchManager.FetchTweetsOffline();
			}
			if (tweets.Count > 0)
			{
				loadTableView(tweets);
			}
			else {
				hud.Hide(animated: true);
				var alert = new UIAlertView("Warning", "Search request failed, Check Internet connectivity and try again", null, "Cancel", "Retry");
				alert.Show();
				alert.Clicked += (sender, e) =>
				{
					if (e.ButtonIndex == 0)
					{

					}
					else {
						loadTweets();
					}
				};
			}


		}
		public void loadTableView(List<Status> tweets)

		{

			tweetTableView.DataSource = new TableSource(tweets);
			tweetTableView.Delegate = new TableDelegate(this);
			tweetTableView.EstimatedRowHeight = 40f;
			hud.Hide(animated: true);

		}
		public override void DidReceiveMemoryWarning()
		{
			base.DidReceiveMemoryWarning();
			// Release any cached data, images, etc that aren't in use.		
		}
		string getSQLiteDatabaseFilePath()
		{
			var sqliteFilename = "TweetSQLite.db3";
			string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal); // Documents folder
			string libraryPath = Path.Combine(documentsPath, "..", "Library"); // Library folder
			var path = Path.Combine(libraryPath, sqliteFilename);
			return path;
		}
	}
}
