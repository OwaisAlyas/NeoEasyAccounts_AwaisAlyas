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
	public interface ILineOfBusinessService : IService<LineOfBusiness>
	{

	}
	internal class LineOfBusinessService : ServiceBase<LineOfBusiness>, ILineOfBusinessService
	{
		public LineOfBusinessService(IRepository<LineOfBusiness> repository, IUnitofWork unitofWork)
			: base(repository, unitofWork)
		{

		}
	}
}
