using Neo.EasyAccounts.Data;
using Neo.EasyAccounts.Data.Repositories;
using Neo.EasyAccounts.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neo.EasyAccounts.Service.Masters
{
	public interface ICustomerService : IService<Customer>
	{

	}
	internal class CustomerService : ServiceBase<Customer>, ICustomerService
	{
		public CustomerService(IRepository<Customer> repository, IUnitofWork unitofWork)
			: base(repository, unitofWork)
		{

		}
	}
}
