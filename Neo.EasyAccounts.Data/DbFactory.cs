﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neo.EasyAccounts.Data
{
	public class DbFactory : Disposable, IDbFactory
	{
		DbEntities dbContext;

		public DbEntities Init()
		{
			return dbContext ?? (dbContext = new DbEntities());
		}

		protected override void DisposeCore()
		{
			if (dbContext != null) dbContext.Dispose();
		}
	}
}
