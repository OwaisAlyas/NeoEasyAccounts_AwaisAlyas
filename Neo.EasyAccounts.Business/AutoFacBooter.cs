using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neo.EasyAccounts.Service
{
	public class AutoFacBooter
	{
		public AutoFacBooter()
		{

		}

		public static Type RepositoryType
		{
			get { return typeof(Data.AutoFacBooter); }
		}

		public static Type GetUnitOfWorkType
		{
			get { return typeof(Data.UnitofWork); }
		}
	}
}
