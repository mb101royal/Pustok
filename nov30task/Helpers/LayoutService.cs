using nov30task.Context;
using nov30task.Models;

namespace nov30task.Helpers
{
	public class LayoutService
	{
		PustokDbContext Db { get; }

		public LayoutService(PustokDbContext db)
		{
			Db = db;
		}

		public async Task<Setting> GetSettingsAsync() => await Db.Settings.FindAsync(1);
	}
}
