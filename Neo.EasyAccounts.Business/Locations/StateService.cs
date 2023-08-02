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
	public interface IStateService : IService<State>
	{
		State Get(string Name);
		IEnumerable<State> GetAll(string Name);
		IEnumerable<State> GetAllByCountryID(long countryID);
		
		Task<State> GetAsync(string Name);
		Task<IEnumerable<State>> GetAllAsync(string Name);
		Task<IEnumerable<State>> GetAllByCountryIDAsync(long countryID);
	}
	internal class StateService : ServiceBase<State>, IStateService
	{
		private readonly IStateRepository _repo;
		public StateService(IStateRepository repo ,IRepository<State> repository, IUnitofWork unitofWork)
			: base(repository, unitofWork)
		{
			_repo = repo;
		}

		public IEnumerable<State> GetByCountryID(long countryID) 
		{
			var entity = _repo.GetAll(d=> d.CountryID == countryID);
			return entity;
		}
		public State Get(string Name)
		{
			var entity = _repo.Get(d => d.Name.Equals(Name));
			return entity;
		}
		public IEnumerable<State> GetAll(string Name)
		{
			var list = _repo.GetAll(d => d.Name.Equals(Name));
			return list;
		}
		public IEnumerable<State> GetAllByCountryID(long countryID)
		{
			var list = _repo.GetAll(d => d.CountryID.Equals(countryID));
			return list;
		}

		public async Task<IEnumerable<State>> GetByCountryIDAsync(long countryID)
		{
			var list = await _repo.GetAllAsync(d => d.CountryID == countryID);
			return list;
		}
		public async Task<State> GetAsync(string Name)
		{
			var entity = await _repo.GetAsync(d => d.Name.Equals(Name));
			return entity;
		}
		public async Task<IEnumerable<State>> GetAllAsync(string Name)
		{
			var list = await _repo.GetAllAsync(d => d.Name.Equals(Name));
			return list;
		}
		public async Task<IEnumerable<State>> GetAllByCountryIDAsync(long countryID)
		{
			var list = await _repo.GetAllAsync(d => d.CountryID.Equals(countryID));
			return list;
		}
	}
}
