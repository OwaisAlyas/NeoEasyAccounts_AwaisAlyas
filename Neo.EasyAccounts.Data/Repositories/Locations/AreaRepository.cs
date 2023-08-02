using Neo.EasyAccounts.Models.Domain.Locations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neo.EasyAccounts.Data.Repositories.Locations
{
	public interface IAreaRepository : IRepository<Area>
	{

	}
	internal class AreaRepository : RepositoryBase<Area>, IAreaRepository
	{
		public AreaRepository(IDbFactory dbFactory)
			: base(dbFactory)
		{

		}
	}
}
