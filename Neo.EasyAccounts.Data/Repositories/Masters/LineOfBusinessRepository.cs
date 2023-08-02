using Neo.EasyAccounts.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neo.EasyAccounts.Data.Repositories.Masters
{
	public interface ILineOfBusinessRepository : IRepository<LineOfBusiness>
	{
		LineOfBusiness Get(string Name);
		IEnumerable<LineOfBusiness> GetAll(string Name);
	}

	internal class LineOfBusinessRepository : RepositoryBase<LineOfBusiness>, ILineOfBusinessRepository
	{
		public LineOfBusinessRepository(IDbFactory dbFactory)
			: base(dbFactory)
		{

		}

		public LineOfBusiness Get(string Name)
		{
			var entity = DbContext.LineOfBusinesses.FirstOrDefault(d => d.Name.Equals(Name));

			return entity;
		}
		public IEnumerable<LineOfBusiness> GetAll(string Name)
		{
			var entities = DbContext.LineOfBusinesses.Where(d => d.Name.Equals(Name));

			return entities;
		}
	}
}
