using Neo.EasyAccounts.Models.Domain.Accounts;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;

namespace Neo.EasyAccounts.Data.Initializers
{
	internal static class AccountsInitializer
	{
		public static void SeedInitialData(DbEntities context)
		{
			var accountTypes = new List<AccountType> { 
				new AccountType(){ Name = "Assets", Code="1000", Description="All type of Asset Accounts" ,DateCreated = DateTime.Now, CreatedBy = "1",   IsDeleted = false, IsActive = true },
				new AccountType(){ Name = "Liabilities", Code="2000", Description="All type of Liability Accounts" ,DateCreated = DateTime.Now, CreatedBy = "1",   IsDeleted = false, IsActive = true },
				new AccountType(){ Name = "Equity", Code="3000", Description="All type of Equity Accounts" ,DateCreated = DateTime.Now, CreatedBy = "1",   IsDeleted = false, IsActive = true },
				new AccountType(){ Name = "Revenues", Code="4000", Description="All type of Revenue Accounts" ,DateCreated = DateTime.Now, CreatedBy = "1",   IsDeleted = false, IsActive = true },
				new AccountType(){ Name = "COGS", Code="5000", Description="Cost of Goods Sold" ,DateCreated = DateTime.Now, CreatedBy = "1",   IsDeleted = false, IsActive = true },
				new AccountType(){ Name = "Expenses", Code="6000", Description="All type of Expense Accounts" ,DateCreated = DateTime.Now, CreatedBy = "1",   IsDeleted = false, IsActive = true }
			};
			accountTypes.ForEach(d => context.AccountTypes.AddOrUpdate(p => p.ID, d));
			context.SaveChanges();

			var assetType = context.AccountTypes.FirstOrDefault(d => d.Name.Equals("Assets"));
			var liabilitiesType = context.AccountTypes.FirstOrDefault(d => d.Name.Equals("Liabilities"));
			var accountGroups = new List<AccountGroup> { 
				new AccountGroup(){ AccountType	= assetType, Code="1100", Name = "Current Assets", Description="Some Good Description" ,DateCreated = DateTime.Now, CreatedBy = "1", IsDeleted = false, IsActive = true },
				new AccountGroup(){ AccountType	= assetType, Code="1200", Name = "Non-Current Assets", Description="Some Good Description" ,DateCreated = DateTime.Now, CreatedBy = "1", IsDeleted = false, IsActive = true },
				new AccountGroup(){ AccountType	= liabilitiesType, Code="2100", Name = "Current Liabilities", Description="Some Good Description" ,DateCreated = DateTime.Now, CreatedBy = "1", IsDeleted = false, IsActive = true },
				new AccountGroup(){ AccountType	= liabilitiesType, Code="2200", Name = "Non-Current Liabilities", Description="Some Good Description" ,DateCreated = DateTime.Now, CreatedBy = "1", IsDeleted = false, IsActive = true }
			};
			accountGroups.ForEach(d => context.AccountGroups.AddOrUpdate(p => p.ID, d));
			context.SaveChanges();

			var accountGroupCurrentAsset = context.AccountGroups.FirstOrDefault(d => d.Name.Equals("Current Assets"));
			var accountGroupCurrentLiabilities = context.AccountGroups.FirstOrDefault(d => d.Name.Equals("Current Liabilities"));
			var accountSubGroups = new List<AccountSubGroup> { 
				new AccountSubGroup(){ AccountGroup	= accountGroupCurrentAsset, Code="101010", Name = "Staff Advances", Description="Some Good Description" ,DateCreated = DateTime.Now, CreatedBy = "1", IsDeleted = false, IsActive = true },
				new AccountSubGroup(){ AccountGroup	= accountGroupCurrentAsset, Code="102020", Name = "Fixed Assets", Description="Some Good Description" ,DateCreated = DateTime.Now, CreatedBy = "1", IsDeleted = false, IsActive = true },
				new AccountSubGroup(){ AccountGroup	= accountGroupCurrentLiabilities, Code="501010", Name = "Purchases", Description="Some Good Description" ,DateCreated = DateTime.Now, CreatedBy = "1", IsDeleted = false, IsActive = true }
			};
			accountSubGroups.ForEach(d => context.AccountSubGroups.AddOrUpdate(p => p.ID, d));
			context.SaveChanges();

			var staffAdvancesSubGroup = context.AccountSubGroups.FirstOrDefault(d => d.Name.Equals("Staff Advances"));
			var fixedAssetsSubGroup = context.AccountSubGroups.FirstOrDefault(d => d.Name.Equals("Fixed Assets"));
			var purchasesSubGroup = context.AccountSubGroups.FirstOrDefault(d => d.Name.Equals("Purchases"));
			var accountTitles = new List<AccountTitle> { 
				new AccountTitle(){ AccountSubGroup = staffAdvancesSubGroup, Code="1010101", Name = "Staff Advances Account1", Description="Some Good Description" ,DateCreated = DateTime.Now, CreatedBy = "1", IsDeleted = false, IsActive = true },
				new AccountTitle(){ AccountSubGroup = staffAdvancesSubGroup, Code="1010102", Name = "Staff Advances Account2", Description="Some Good Description" ,DateCreated = DateTime.Now, CreatedBy = "1", IsDeleted = false, IsActive = true },
				new AccountTitle(){ AccountSubGroup = staffAdvancesSubGroup, Code="1010103", Name = "Staff Advances Account3", Description="Some Good Description" ,DateCreated = DateTime.Now, CreatedBy = "1", IsDeleted = false, IsActive = true },

				new AccountTitle(){ AccountSubGroup = fixedAssetsSubGroup, Code="1010101", Name = "Fixed Assets Account1", Description="Some Good Description" ,DateCreated = DateTime.Now, CreatedBy = "1", IsDeleted = false, IsActive = true },
				new AccountTitle(){ AccountSubGroup = fixedAssetsSubGroup, Code="1010102", Name = "Fixed Assets Account2", Description="Some Good Description" ,DateCreated = DateTime.Now, CreatedBy = "1", IsDeleted = false, IsActive = true },
				new AccountTitle(){ AccountSubGroup = fixedAssetsSubGroup, Code="1010103", Name = "Fixed Assets Account3", Description="Some Good Description" ,DateCreated = DateTime.Now, CreatedBy = "1", IsDeleted = false, IsActive = true },

				new AccountTitle(){ AccountSubGroup = purchasesSubGroup, Code="1010101", Name = "Purchases Account1", Description="Some Good Description" ,DateCreated = DateTime.Now, CreatedBy = "1", IsDeleted = false, IsActive = true },
				new AccountTitle(){ AccountSubGroup = purchasesSubGroup, Code="1010102", Name = "Purchases Account2", Description="Some Good Description" ,DateCreated = DateTime.Now, CreatedBy = "1", IsDeleted = false, IsActive = true },
				new AccountTitle(){ AccountSubGroup = purchasesSubGroup, Code="1010103", Name = "Purchases Account3", Description="Some Good Description" ,DateCreated = DateTime.Now, CreatedBy = "1", IsDeleted = false, IsActive = true }
			};
			accountTitles.ForEach(d => context.AccountTitles.AddOrUpdate(p => p.ID, d));
			context.SaveChanges();
		}
	}
}
