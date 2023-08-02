using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neo.EasyAccounts.Models.Domain
{
	public abstract class ModelBase
	{
		public long ID { get; set; }

		public long CreatedBy { get; set; }
		public long? ModifiedBy { get; set; }
		public DateTime DateCreated { get; set; }
		public DateTime? DateModified { get; set; }

		public bool IsDeleted { get; set; }
		public bool IsActive { get; set; }

		public override string ToString()
		{
			return Convert.ToString(ID);
		}

		public override int GetHashCode()
		{
			return this.ID.GetHashCode();
		}

		//public override bool Equals(object model)
		//{
		//	return model != null && model is ModelBase && this == (ModelBase)model;
		//}

		//public static bool operator ==(ModelBase modelA, ModelBase modelB)
		//{
		//	if ((modelA == null && modelB != null) || (modelB == null && modelA != null))
		//		return false;

		//	bool result = ((object)modelA == null && (object)modelB == null);

		//	result = (modelA.ID.ToString().ToLower() == modelB.ID.ToString().ToLower());

		//	//if ((object)modelA == null && (object)modelB == null) result = true;

		//	//if ((object)modelA == null || (object)modelB == null) result = true;

		//	//if (modelA.ID.ToString().ToLower() == modelB.ID.ToString().ToLower()) result = true;

		//	return result;
		//}
		//public static bool operator !=(ModelBase modelA, ModelBase modelB)
		//{
		//	return (!(modelA == modelB));
		//}
	}
}
