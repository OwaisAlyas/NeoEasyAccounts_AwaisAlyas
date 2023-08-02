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
	public interface IAreaService : IService<Area>
	{
		Area Get(string Name);
		IEnumerable<Area> GetAll(string Name);
		IEnumerable<Area> GetAllByCity(long cityID);

		Task<Area> GetAsync(string Name);
		Task<IEnumerable<Area>> GetAllAsync(string Name);
		Task<IEnumerable<Area>> GetAllByCityAsync(long cityID);
	}
	internal class AreaService : ServiceBase<Area>, IAreaService
	{
		private readonly IAreaRepository repo;
		public AreaService(IAreaRepository repo, IRepository<Area> repository, IUnitofWork unitofWork)
			: base(repository, unitofWork)
		{
			this.repo = repo;
		}

		public Area Get(string Name)
		{
			var entity = repo.Get(d => d.Name.Equals(Name));
			return entity;
		}
		public IEnumerable<Area> GetAll(string Name)
		{
			var entities = repo.GetAll(d => d.Name.Equals(Name));
			return entities;
		}
		public IEnumerable<Area> GetAllByCity(long cityID)
		{
			var entities = repo.GetAll(d => d.CityID.Equals(cityID));
			return entities;
		}

		public async Task<Area> GetAsync(string Name)
		{
			var entity = await repo.GetAsync(d => d.Name.Equals(Name));
			return entity;
		}
		public async Task<IEnumerable<Area>> GetAllAsync(string Name)
		{
			var entities = await repo.GetAllAsync(d => d.Name.Equals(Name));
			return entities;
		}
		public async Task<IEnumerable<Area>> GetAllByCityAsync(long cityID)
		{
			var entities = await repo.GetAllAsync(d => d.CityID.Equals(cityID));
			return entities;
		}
	}
}
