using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neo.EasyAccounts.Data
{
	public class UnitofWork : IUnitofWork
	{
		private IDbFactory _dbFactory;

		public UnitofWork(IDbFactory dbFactory)
		{
			this._dbFactory = dbFactory;
		}

		private DbEntities _dbContext;
		public DbEntities DbContext
		{
			get
			{
				return _dbContext ?? (_dbContext = _dbFactory.Init());
			}
		}

		public int Commit()
		{
			int rowsEffected = DbContext.SaveChanges();
			//DbContext.Commit();
			return rowsEffected;
		}
		public async Task<int> CommitAsync()
		{
			var rowsEffected = await DbContext.SaveChangesAsync();
			//DbContext.Commit();
			return rowsEffected;
		}
	}
}
