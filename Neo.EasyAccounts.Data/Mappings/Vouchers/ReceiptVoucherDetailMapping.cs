using Neo.EasyAccounts.Models.Domain.Vouchers;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neo.EasyAccounts.Data.Mappings.Vouchers
{
	public class ReceiptVoucherDetailMapping : EntityTypeConfiguration<ReceiptVoucherDetail>, IMapper
	{
		public ReceiptVoucherDetailMapping()
		{
			Property(d => d.AccountID).IsRequired();
			Property(d => d.ReceiptVoucherID).IsRequired();

			Property(d => d.ChequeNo).HasMaxLength(250);
			Property(d => d.Narration).HasMaxLength(500);
						
			Property(d => d.CreatedBy).IsRequired().HasMaxLength(250);
			Property(d => d.DateCreated).IsRequired();
			Property(d => d.IsDeleted).IsRequired();
			Property(d => d.IsActive).IsRequired();
			Property(d => d.ModifiedBy).HasMaxLength(250);
		}
	}
}
