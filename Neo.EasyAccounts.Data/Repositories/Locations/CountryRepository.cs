using Neo.EasyAccounts.Models.Domain.Locations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace Neo.EasyAccounts.Data.Repositories.Locations
{
	public interface ICountryRepository : IRepository<Country>
	{
		
	}
	internal class CountryRepository : RepositoryBase<Country>, ICountryRepository
	{
		public CountryRepository(IDbFactory dbFactory)
			: base(dbFactory)
		{

		}
	}
}
