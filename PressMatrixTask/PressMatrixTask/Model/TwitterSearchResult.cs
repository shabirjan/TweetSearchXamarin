using System;
using System.Collections.Generic;
using Newtonsoft;
using Newtonsoft.Json;
using SQLite;

namespace PressMatrixTask
{
	public class TwitterSearchResult
	{
		public List<Status> Statuses { get; set; }
		public TwitterSearchResult()
		{
		}
	}
	public class Status
	{
		[SQLite.PrimaryKey, SQLite.AutoIncrement]
		public Int64 Id { get; set; }
		public Status()
		{

		}
		public string Text
		{
			get;
			set;
		}
		[Ignore]
		public User User
		{
			get; set;
		}
	}
	public class User
	{
		public User()
		{

		}

		[JsonProperty(PropertyName = "profile_image_url")]
		public string ProfileImageUrl
		{
			get; set;
		}
		[SQLite.PrimaryKey, SQLite.AutoIncrement]
		public Int64 StatusId { get; set; }
	}
}
