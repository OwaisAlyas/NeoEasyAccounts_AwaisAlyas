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
	public interface IPaymentVoucherDetailService : IService<PaymentVoucherDetail>
	{
		Task<IEnumerable<PaymentVoucherDetail>> GetAllByVoucherIDAsync(long voucherID);
	}
	internal class PaymentVoucherDetailService : ServiceBase<PaymentVoucherDetail>, IPaymentVoucherDetailService
	{
		private readonly IPaymentVoucherDetailRepository repo;
		public PaymentVoucherDetailService(IPaymentVoucherDetailRepository repo, IRepository<PaymentVoucherDetail> repository, IUnitofWork unitofWork)
			: base(repository, unitofWork)
		{
			this.repo = repo;
		}

		public async Task<IEnumerable<PaymentVoucherDetail>> GetAllByVoucherIDAsync(long voucherID)
		{
			var list = await repo.GetAllAsync(d => d.PaymentVoucherID == voucherID);
			return list;
		}
	}
}
