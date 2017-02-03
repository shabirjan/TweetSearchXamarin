using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SQLite;
namespace PressMatrixTask
{
	public class TweetDatabase
	{
		readonly SQLiteAsyncConnection database;

		public TweetDatabase(SQLiteAsyncConnection conn)
		{
			database = conn;
			var c = conn.CreateTableAsync<User>().Result;
			c = conn.CreateTableAsync<Status>().Result;
		}
		public async Task<Status> GetStatusAsync(Int64 id)
		{
			Status statusData;
			statusData = await database.Table<Status>().Where(x => x.Id == id).FirstOrDefaultAsync();
			return statusData;
		}
		public async Task<User> GetUserAsync(Int64 id)
		{
			User userData;
			userData = await database.Table<User>().Where(x => x.StatusId == id).FirstOrDefaultAsync();
			return userData;
		}
		public async Task<List<Status>> GetAllItems()
		{
			List<Status> list = new List<Status>();
			if (database != null)
			{
				var tempList = await database.Table<Status>().ToListAsync();

				if (tempList != null)
				{
					foreach (var item in tempList)
					{
						item.User = await GetUserAsync(item.Id);
						list.Add(item);
					}
				}

				return list;
			}
			else {
				return list;
			}
		}
		public async Task<int> SaveAllItem(List<Status> items)
		{

			foreach (Status obj in items)
			{
				Status duplicate = await GetStatusAsync(obj.Id);
				if (duplicate != null)
				{
					duplicate.Id = obj.Id;
					duplicate.Text = obj.Text;
					await database.UpdateAsync(duplicate);
				}
				else {
					await database.InsertAsync(obj);
				}

				User duplicate1 = await GetUserAsync(obj.Id);
				if (duplicate1 != null)
				{
					duplicate1.StatusId = obj.Id;
					duplicate1.ProfileImageUrl = obj.User.ProfileImageUrl;
					await database.UpdateAsync(duplicate1);
				}
				else {
					await database.InsertAsync(obj.User);
				}


			}
			return 0;
		}
	}
}
