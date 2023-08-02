using Neo.EasyAccounts.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neo.EasyAccounts.Data.Repositories.Masters
{
	public interface IEmployeeRepository : IRepository<Employee>
	{
		Employee Get(string Name);
		IEnumerable<Employee> GetAll(string Name);
	}

	internal class EmployeeRepository : RepositoryBase<Employee>, IEmployeeRepository
	{
		public EmployeeRepository(IDbFactory dbFactory)
			: base(dbFactory)
		{

		}

		public Employee Get(string Name)
		{
			var entity = DbContext.Employees.FirstOrDefault(d => d.Name.Equals(Name));

			return entity;
		}
		public IEnumerable<Employee> GetAll(string Name)
		{
			var entities = DbContext.Employees.Where(d => d.Name.Equals(Name));

			return entities;
		}
	}
}
