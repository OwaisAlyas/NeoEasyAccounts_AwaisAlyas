using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neo.EasyAccounts.Models.Domain
{
	//public interface IEntity<T>
	//{
	//	T ID { get; set; }
	//	bool IsDeleted { get; set; }
	//	bool IsActive { get; set; }
	//}
	public interface IEntity
	{
		long ID { get; set; }
		bool IsDeleted { get; set; }
		bool IsActive { get; set; }
	}

	public interface IAuditableEntity
	{
		DateTime DateCreated { get; set; }
		string CreatedBy { get; set; }

		DateTime? DateModified { get; set; }
		string ModifiedBy { get; set; }
	}
}
