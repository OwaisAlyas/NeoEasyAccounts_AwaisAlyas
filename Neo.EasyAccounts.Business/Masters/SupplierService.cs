using Neo.EasyAccounts.Data;
using Neo.EasyAccounts.Data.Repositories;
using Neo.EasyAccounts.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neo.EasyAccounts.Service.Masters
{
	public interface ISupplierService : IService<Supplier>
	{

	}
	internal class SupplierService : ServiceBase<Supplier>, ISupplierService
	{
		public SupplierService(IRepository<Supplier> repository, IUnitofWork unitofWork)
			: base(repository, unitofWork)
		{

		}
	}
}
