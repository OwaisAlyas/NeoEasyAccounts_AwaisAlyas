using Neo.EasyAccounts.Models.Domain.Locations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace Neo.EasyAccounts.Data.Repositories.Locations
{
	public interface ICityRepository : IRepository<City>
	{
		
	}
	internal class CityRepository : RepositoryBase<City>, ICityRepository
	{
		public CityRepository(IDbFactory dbFactory)
			: base(dbFactory)
		{

		}
	}
}
