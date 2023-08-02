using Neo.EasyAccounts.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neo.EasyAccounts.Data.Repositories.Masters
{
	public interface ISupplierRepository : IRepository<Supplier>
	{
		Supplier Get(string Name);
		IEnumerable<Supplier> GetAll(string Name);
	}

	internal class SupplierRepository : RepositoryBase<Supplier>, ISupplierRepository
	{
		public SupplierRepository(IDbFactory dbFactory)
			: base(dbFactory)
		{

		}
		
		public Supplier Get(string Name)
		{
			var entity = DbContext.Suppliers.FirstOrDefault(d => d.Name.Equals(Name));

			return entity;
		}
		public IEnumerable<Supplier> GetAll(string Name)
		{
			var entities = DbContext.Suppliers.Where(d => d.Name.Equals(Name));

			return entities;
		}
	}
}
