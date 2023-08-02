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
	public interface IJournalVoucherDetailService : IService<JournalVoucherDetail>
	{
		IEnumerable<JournalVoucherDetail> GetAllByVoucherID(long voucherID);
		Task<IEnumerable<JournalVoucherDetail>> GetAllByVoucherIDAsync(long voucherID);
	}
	internal class JournalVoucherDetailService : ServiceBase<JournalVoucherDetail>, IJournalVoucherDetailService
	{
		private readonly IJournalVoucherDetailRepository repo;
		public JournalVoucherDetailService(IJournalVoucherDetailRepository repo, IRepository<JournalVoucherDetail> repository, IUnitofWork unitofWork)
			: base(repository, unitofWork)
		{
			this.repo = repo;
		}

		public IEnumerable<JournalVoucherDetail> GetAllByVoucherID (long voucherID)
		{
			var list = repo.GetAll(d => d.JournalVoucherID == voucherID).ToList();
			return list;
		}

		public async Task<IEnumerable<JournalVoucherDetail>> GetAllByVoucherIDAsync(long voucherID)
		{
			var list = await repo.GetAllAsync(d => d.JournalVoucherID == voucherID);
			return list;
		}
	}
}
