using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;


namespace PressMatrixTask
{
	public class TwitterClient
	{
		readonly HttpClient client;
		public TwitterClient()
		{
			client = new HttpClient();
			client.MaxResponseContentBufferSize = Int64.MaxValue;
		}
		public async Task<string> Autheticate(string consumerKey, string consumerSecret)
		{
			string tokenCreds = consumerKey + ":" + consumerSecret;
			string base64String = System.Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(tokenCreds));

			client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", base64String);
			client.BaseAddress = new Uri("https://api.twitter.com/oauth2/token");
			var content = new FormUrlEncodedContent(new[]
			{
					new KeyValuePair<string,string>("grant_type","client_credentials")
				});
			var result = await client.PostAsync("", content);
			string resultContent = await result.Content.ReadAsStringAsync();
			var json = Newtonsoft.Json.Linq.JObject.Parse(resultContent);
			return (string)json["access_token"];

		}

		public async Task<List<Status>> Search(string keyword, int numberOfTweets, string authToken)
		{
			client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", authToken);
			client.BaseAddress = new Uri("https://api.twitter.com/1.1/search/tweets.json?q=" + keyword + "&count=" + numberOfTweets);
			var result = await client.GetAsync("");
			string resultContent = await result.Content.ReadAsStringAsync();
			var twitterResult = JsonConvert.DeserializeObject<TwitterSearchResult>(resultContent);
			foreach (Status status in twitterResult.Statuses)
			{
				status.User.StatusId = status.Id;
			}
			return twitterResult.Statuses;



		}

	}
}
