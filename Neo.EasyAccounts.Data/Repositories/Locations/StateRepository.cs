using Neo.EasyAccounts.Models.Domain.Locations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace Neo.EasyAccounts.Data.Repositories.Locations
{
	public interface IStateRepository : IRepository<State>
	{
		
	}

	internal class StateRepository : RepositoryBase<State>, IStateRepository
	{
		public StateRepository(IDbFactory dbFactory)
			: base(dbFactory)
		{

		}
	}
}
