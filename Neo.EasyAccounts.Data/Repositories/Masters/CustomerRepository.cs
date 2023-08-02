using Neo.EasyAccounts.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neo.EasyAccounts.Data.Repositories.Masters
{
	public interface ICustomerRepository : IRepository<Customer>
	{
		Customer Get(string Name);
		IEnumerable<Customer> GetAll(string Name);
	}

	internal class CustomerRepository : RepositoryBase<Customer>, ICustomerRepository
	{
		public CustomerRepository(IDbFactory dbFactory)
			: base(dbFactory)
		{

		}

		public Customer Get(string Name)
		{
			var entity = DbContext.Customers.FirstOrDefault(d => d.Name.Equals(Name));

			return entity;
		}
		public IEnumerable<Customer> GetAll(string Name)
		{
			var entities = DbContext.Customers.Where(d => d.Name.Equals(Name));

			return entities;
		}
	}
}
