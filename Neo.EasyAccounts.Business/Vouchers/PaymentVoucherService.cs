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
	public interface IPaymentVoucherService : IService<PaymentVoucher>
	{
		PaymentVoucher Get(string number);
		IEnumerable<PaymentVoucher> GetAll(string number);
	}
	internal class PaymentVoucherService : ServiceBase<PaymentVoucher>, IPaymentVoucherService
	{
		private readonly IPaymentVoucherRepository repo;

		public PaymentVoucherService(IPaymentVoucherRepository repo, IRepository<PaymentVoucher> repository, IUnitofWork unitofWork)
			: base(repository, unitofWork)
		{
			this.repo = repo;
		}

		public PaymentVoucher Get(string number)
		{
			var entity = repo.Get(d => d.Number == number);
			return entity;
		}
		public IEnumerable<PaymentVoucher> GetAll(string number)
		{
			var list = repo.GetAll(d => d.Number == number);
			return list;
		}
	}
}
