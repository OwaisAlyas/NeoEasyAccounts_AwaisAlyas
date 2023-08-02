using Neo.EasyAccounts.Data;
using Neo.EasyAccounts.Data.Repositories;
using Neo.EasyAccounts.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neo.EasyAccounts.Service
{
	public interface IAutoGenNoSettingService : IService<AutoGenNoSetting>
	{
		AutoGenNoSetting GetByEntity(string entityName);
	}
	internal class AutoGenNoSettingService : ServiceBase<AutoGenNoSetting>, IAutoGenNoSettingService
	{
		private readonly IAutoGenNoSettingRepository repo;
		public AutoGenNoSettingService(IAutoGenNoSettingRepository repo, IRepository<AutoGenNoSetting> repository, IUnitofWork unitofWork)
			: base(repository, unitofWork)
		{
			this.repo = repo;
		}


		public override AutoGenNoSetting Get(long ID)
		{
			var entity = GetAll().FirstOrDefault(d => d.ID == ID);
			return entity;
		}
		public AutoGenNoSetting GetByEntity(string entityName)
		{
			var entity = GetAll().FirstOrDefault(d => d.EntityName == entityName);
			return entity;
		}

		public AutoGenNoSetting Save(AutoGenNoSetting entity)
		{
			throw new NotImplementedException();
		}

		public IEnumerable<AutoGenNoSetting> Save(IEnumerable<AutoGenNoSetting> entities)
		{
			throw new NotImplementedException("AutoGenerating code settings from database not yet implemented. Call only get methods here");
		}

		public int Delete(long id, bool isPremanent = false)
		{
			throw new NotImplementedException("AutoGenerating code settings from database not yet implemented. Call only get methods here");
		}

		public int Delete(AutoGenNoSetting entity, bool isPremanent = false)
		{
			throw new NotImplementedException("AutoGenerating code settings from database not yet implemented. Call only get methods here");
		}

		public int Delete(IEnumerable<AutoGenNoSetting> entities, bool isPremanent = false)
		{
			throw new NotImplementedException("AutoGenerating code settings from database not yet implemented. Call only get methods here");
		}
		public override async Task<AutoGenNoSetting> UpdateAsync(AutoGenNoSetting model)
		{
			throw new NotImplementedException("AutoGenerating code settings from database not yet implemented. Call only get methods here");
		}
		public override AutoGenNoSetting Update(AutoGenNoSetting model)
		{
			throw new NotImplementedException("AutoGenerating code settings from database not yet implemented. Call only get methods here");
		}
		public override AutoGenNoSetting Add(AutoGenNoSetting model)
		{
			throw new NotImplementedException("AutoGenerating code settings from database not yet implemented. Call only get methods here");
		}
		public override async Task<AutoGenNoSetting> AddAsync(AutoGenNoSetting model)
		{
			throw new NotImplementedException("AutoGenerating code settings from database not yet implemented. Call only get methods here");
		}
		public override int Delete(AutoGenNoSetting model)
		{
			throw new NotImplementedException("AutoGenerating code settings from database not yet implemented. Call only get methods here");
		}
		public override async Task<int> DeleteAsync(AutoGenNoSetting model)
		{
			throw new NotImplementedException("AutoGenerating code settings from database not yet implemented. Call only get methods here");
		}
		
		public override async Task<AutoGenNoSetting> GetAsync(long ID)
		{
			throw new NotImplementedException("AutoGenerating code settings from database not yet implemented. Call only get methods here");
		}
		public override async Task<AutoGenNoSetting> GetAsync(System.Linq.Expressions.Expression<Func<AutoGenNoSetting, bool>> condition)
		{
			throw new NotImplementedException("AutoGenerating code settings from database not yet implemented. Call only get methods here");
		}
		
	}
}
