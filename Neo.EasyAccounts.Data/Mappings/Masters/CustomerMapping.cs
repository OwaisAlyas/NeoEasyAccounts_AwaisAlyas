using Neo.EasyAccounts.Models.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neo.EasyAccounts.Data.Mappings.Masters
{
	public class CustomerMapping : EntityTypeConfiguration<Customer>, IMapper
	{
		public CustomerMapping()
		{
			Property(d => d.Name).IsRequired().HasMaxLength(250);
			Property(d => d.Code).IsRequired().HasMaxLength(250);
			Property(d => d.Type).IsRequired().HasMaxLength(100);
			
			Property(d => d.CreatedBy).IsRequired().HasMaxLength(250);
			Property(d => d.DateCreated).IsRequired();
			Property(d => d.IsDeleted).IsRequired();
			Property(d => d.IsActive).IsRequired();
			Property(d => d.ModifiedBy).HasMaxLength(250);
		}
	}
}
