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
	public interface IReceiptVoucherDetailService : IService<ReceiptVoucherDetail>
	{
		Task<IEnumerable<ReceiptVoucherDetail>> GetAllByVoucherIDAsync(long voucherID);
	}
	internal class ReceiptVoucherDetailService : ServiceBase<ReceiptVoucherDetail>, IReceiptVoucherDetailService
	{
		private readonly IReceiptVoucherDetailRepository repo;
		public ReceiptVoucherDetailService(IReceiptVoucherDetailRepository repo, IRepository<ReceiptVoucherDetail> repository, IUnitofWork unitofWork)
			: base(repository, unitofWork)
		{
			this.repo = repo;
		}

		public async Task<IEnumerable<ReceiptVoucherDetail>> GetAllByVoucherIDAsync(long voucherID)
		{
			var list = await repo.GetAllAsync(d => d.ReceiptVoucherID == voucherID);
			return list;
		}
	}
}
