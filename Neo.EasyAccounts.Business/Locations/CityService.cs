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
	public interface ICityService : IService<City>
	{
		City Get(string Name);
		IEnumerable<City> GetAll(string Name);
		IEnumerable<City> GetCitiesByStateID(long stateID);
		
		Task<City> GetAsync(string Name);
		Task<IEnumerable<City>> GetAllAsync(string Name);
		Task<IEnumerable<City>> GetCitiesByStateIDAsync(long stateID);
	}
	internal class CityService : ServiceBase<City>, ICityService
	{
		private readonly ICityRepository _repo;
		public CityService(ICityRepository repo, IRepository<City> repository, IUnitofWork unitofWork)
			: base(repository, unitofWork)
		{
			_repo = repo;
		}

		public IEnumerable<City> GetCitiesByStateID(long stateID)
		{
			return _repo.GetAll(d => d.StateID == stateID);
		}
		public City Get(string Name)
		{
			return _repo.Get(d => d.Name.Equals(Name));
		}
		public IEnumerable<City> GetAll(string Name)
		{
			return _repo.GetAll(d => d.Name.Equals(Name));
		}

		public async Task<City> GetAsync(string Name)
		{
			return await _repo.GetAsync(d => d.Name.Equals(Name));
		}
		public async Task<IEnumerable<City>> GetAllAsync(string Name)
		{
			return await _repo.GetAllAsync(d => d.Name.Equals(Name));
		}
		public async Task<IEnumerable<City>> GetCitiesByStateIDAsync(long stateID)
		{
			return await _repo.GetAllAsync(d => d.StateID == stateID);
		}
	}
}
