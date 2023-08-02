using Neo.EasyAccounts.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neo.EasyAccounts.Data.Repositories.Masters
{
	public interface IAddressRepository : IRepository<Address>
	{
	}

	internal class AddressRepository : RepositoryBase<Address>, IAddressRepository
	{
		public AddressRepository(IDbFactory dbFactory)
			: base(dbFactory)
		{

		}
	}
}
