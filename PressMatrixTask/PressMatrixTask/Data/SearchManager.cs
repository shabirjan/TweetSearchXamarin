using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SQLite;

namespace PressMatrixTask

{
	public class SearchManager
	{
		readonly TwitterClient _webClient;
		readonly TweetDatabase _database;
		public SearchManager(SQLiteAsyncConnection conn)
		{
			_webClient = new TwitterClient();
			_database = new TweetDatabase(conn);
		}
		public async Task<string> getTwitterAuthToken()
		{
			return await _webClient.Autheticate("NZFKdb2FXDfPk9wdEHaiXYxYB", "7aJGrvfK41Tc2kBP31ga8PBcutYHw0tDY9oVbbgZ0DNJc4YpIZ");

		}
		public async Task<List<Status>> FetchTweetsOnline(string token, int numberOfTweets, string keyword)
		{
			var tweetList = await _webClient.Search(keyword, numberOfTweets, token);
			await _database.SaveAllItem(tweetList);
			return tweetList;

		}
		public async Task<List<Status>> FetchTweetsOffline()
		{
			List<Status> tweetList = new List<Status>();
			tweetList = await _database.GetAllItems();
			if (tweetList == null)
			{
				tweetList = new List<Status>();
			}

			return tweetList;
		}
		//public async Task<List<Status> LoadAllTweets(string token, int numberOfTweets, string keyword){
		//	var onlineTweets = await FetchTweetsOnline(token, numberOfTweets, keyword);
		//	var 
		//}




	}
}
