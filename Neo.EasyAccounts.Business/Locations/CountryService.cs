using Neo.EasyAccounts.Data;
using Neo.EasyAccounts.Data.Repositories;
using Neo.EasyAccounts.Data.Repositories.Locations;
using Neo.EasyAccounts.Models.Domain.Locations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neo.EasyAccounts.Service.Locations
{
	public interface ICountryService : IService<Country>
	{
		Country Get(string Name);
		IEnumerable<Country> GetAll(string Name);
		Task<Country> GetAsync(string Name);
		Task<IEnumerable<Country>> GetAllAsync(string Name);
	}
	public class CountryService : ServiceBase<Country>, ICountryService
	{
		private readonly ICountryRepository repo;
		
		public CountryService(ICountryRepository repo, IRepository<Country> repository, IUnitofWork unitofWork)
			: base(repository, unitofWork)
		{
			this.repo = repo;
		}

		public Country Get(string Name)
		{
			var entity = repo.Get(d => d.Name.ToLower().Equals(Name.Trim().ToLower()));
			
			return entity;
		}
		public IEnumerable<Country> GetAll(string Name)
		{
			var list = repo.GetAll(d => d.Name.Equals(Name));
			return list;
		}
		public async Task<Country> GetAsync(string Name)
		{
			var entity = await repo.GetAsync(d => d.Name.Equals(Name));
			return entity;
		}
		public async Task<IEnumerable<Country>> GetAllAsync(string Name)
		{
			var list = await repo.GetAllAsync(d => d.Name.Equals(Name));
			return list;
		}
	}
}
