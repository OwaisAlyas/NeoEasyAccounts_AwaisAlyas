using Neo.EasyAccounts.Data;
using Neo.EasyAccounts.Data.Repositories;
using Neo.EasyAccounts.Data.Repositories.Vouchers;
using Neo.EasyAccounts.Models.Domain.Vouchers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neo.EasyAccounts.Service.Vouchers
{
	public interface IJournalVoucherService : IService<JournalVoucher>
	{
		JournalVoucher Get(string voucherNo);
		IEnumerable<JournalVoucher> GetAll(string voucherNo);

		Task<JournalVoucher> GetAsync(string voucherNo);
		Task<IEnumerable<JournalVoucher>> GetAllAsync(string voucherNo);
	}
	internal class JournalVoucherService : ServiceBase<JournalVoucher>, IJournalVoucherService
	{
		private readonly IJournalVoucherRepository repo;
		public JournalVoucherService(IJournalVoucherRepository repo, IRepository<JournalVoucher> repository, IUnitofWork unitofWork)
			: base(repository, unitofWork)
		{
			this.repo = repo;
		}

		public JournalVoucher Get(string voucherNo)
		{
			var entity = repo.Get(d => d.Number == voucherNo);
			return entity;
		}
		public IEnumerable<JournalVoucher> GetAll(string voucherNo)
		{
			var list = repo.GetAll(d=> d.Number == voucherNo);
			return list;
		}
		public async Task<JournalVoucher> GetAsync(string voucherNo)
		{
			var entity = await repo.GetAsync(d => d.Number == voucherNo);
			return entity;
		}
		public async Task<IEnumerable<JournalVoucher>> GetAllAsync(string voucherNo)
		{
			var list = await repo.GetAllAsync(d => d.Number == voucherNo);
			return list;
		}
	}
}
