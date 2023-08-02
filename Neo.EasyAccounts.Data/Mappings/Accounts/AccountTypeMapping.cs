﻿using Neo.EasyAccounts.Models.Domain.Accounts;
using System.Data.Entity.ModelConfiguration;

namespace Neo.EasyAccounts.Data.Mappings.Accounts
{
	public class AccountTypeMapping : EntityTypeConfiguration<AccountType>, IMapper
	{
		public AccountTypeMapping()
		{
			Property(d => d.Name).IsRequired().HasMaxLength(250);
			Property(d => d.Code).IsRequired().HasMaxLength(250);
			Property(d => d.Description).HasMaxLength(500);

			Property(d => d.CreatedBy).IsRequired().HasMaxLength(250);
			Property(d => d.DateCreated).IsRequired();
			Property(d => d.IsDeleted).IsRequired();
			Property(d => d.IsActive).IsRequired();
			Property(d => d.ModifiedBy).HasMaxLength(250);

			//ToTable("Countries");
		}
	}
}
