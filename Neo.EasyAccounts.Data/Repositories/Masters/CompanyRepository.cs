using Neo.EasyAccounts.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neo.EasyAccounts.Data.Repositories.Masters
{
	public interface ICompanyRepository : IRepository<Company>
	{
		Company Get(string Name);
		IEnumerable<Company> GetAll(string Name);
	}

	internal class CompanyRepository : RepositoryBase<Company>, ICompanyRepository
	{
		public CompanyRepository(IDbFactory dbFactory)
			: base(dbFactory)
		{

		}

		public Company Get(string Name)
		{
			var entity = DbContext.Companies.FirstOrDefault(d => d.Name.Equals(Name));

			return entity;
		}
		public IEnumerable<Company> GetAll(string Name)
		{
			var entities = DbContext.Companies.Where(d => d.Name.Equals(Name));

			return entities;
		}
	}
}
