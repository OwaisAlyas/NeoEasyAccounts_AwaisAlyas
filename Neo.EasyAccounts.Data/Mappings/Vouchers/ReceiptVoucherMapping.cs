using Neo.EasyAccounts.Models.Domain.Vouchers;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neo.EasyAccounts.Data.Mappings.Vouchers
{
	public class ReceiptVoucherMapping : EntityTypeConfiguration<ReceiptVoucher>, IMapper
	{
		public ReceiptVoucherMapping()
		{
			Property(d => d.Number).IsRequired().HasMaxLength(250);
			Property(d => d.Type).IsRequired().HasMaxLength(100);
			Property(d => d.Date).IsRequired();
			Property(d => d.Description).HasMaxLength(500);
			Property(d => d.CustomerID).IsRequired();

			Property(d => d.CreatedBy).IsRequired().HasMaxLength(250);
			Property(d => d.DateCreated).IsRequired();
			Property(d => d.IsDeleted).IsRequired();
			Property(d => d.IsActive).IsRequired();
			Property(d => d.ModifiedBy).HasMaxLength(250);
		}
	}
}
