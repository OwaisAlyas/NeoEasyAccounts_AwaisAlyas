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
	public interface IReceiptVoucherService : IService<ReceiptVoucher>
	{
		ReceiptVoucher Get(string Number);
		IEnumerable<ReceiptVoucher> GetAll(string Number);
	}
	internal class ReceiptVoucherService : ServiceBase<ReceiptVoucher>, IReceiptVoucherService
	{
		private readonly IReceiptVoucherRepository repo;
		public ReceiptVoucherService(IReceiptVoucherRepository repo, IRepository<ReceiptVoucher> repository, IUnitofWork unitofWork)
			: base(repository, unitofWork)
		{
			this.repo = repo;
		}

		public ReceiptVoucher Get(string Number)
		{
			var entity = repo.Get(d => d.Number.Equals(Number));

			return entity;
		}
		public IEnumerable<ReceiptVoucher> GetAll(string Number)
		{
			var entities = repo.GetAll(d => d.Number.Equals(Number));

			return entities;
		}
	}
}
