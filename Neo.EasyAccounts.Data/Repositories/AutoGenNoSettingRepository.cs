using Neo.EasyAccounts.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neo.EasyAccounts.Data.Repositories
{
	public interface IAutoGenNoSettingRepository : IRepository<AutoGenNoSetting>
	{

	}
	internal class AutoGenNoSettingRepository : RepositoryBase<AutoGenNoSetting>, IAutoGenNoSettingRepository
	{
		/*
		 * 1. Override Update method to save the existing meta data of CreatedBy and DateCreated
		 * 2. Make GetAsync(long ID)
		 */
		public AutoGenNoSettingRepository(IDbFactory dbFactory)
			: base(dbFactory)
		{

		}

		public override IEnumerable<AutoGenNoSetting> GetAll()
		{
			var list = new List<AutoGenNoSetting>() { 
				new AutoGenNoSetting() { EntityName="JournalVoucher", NoOfDigits=6, PostFix=null, PreFix="JV", Separator="-", IsActive=true }, //CreatedBy=1, DateCreated=DateTime.Now },
				new AutoGenNoSetting() { EntityName="PaymentVoucher", NoOfDigits=6, PostFix=null, PreFix="PV", Separator="-", IsActive=true }, //CreatedBy=1, DateCreated=DateTime.Now },
				new AutoGenNoSetting() { EntityName="ReceiptVoucher", NoOfDigits=6, PostFix=null, PreFix="RV", Separator="-", IsActive=true } //,CreatedBy=1, DateCreated=DateTime.Now }
			};
			return list;
		}

		public override AutoGenNoSetting Get(System.Linq.Expressions.Expression<Func<AutoGenNoSetting, bool>> condition)
		{
			var entity = GetAll(condition).First();
			return entity;
		}
		public override IEnumerable<AutoGenNoSetting> GetAll(System.Linq.Expressions.Expression<Func<AutoGenNoSetting, bool>> condition)
		{
			var list = GetAll().AsQueryable().Where(condition);
			return list;
		}

		public Task<AutoGenNoSetting> GetAsync(long ID)
		{
			throw new NotImplementedException("AutoGenerating code settings from database not yet implemented. Call only get methods here");
		}
	}
}
