using Neo.EasyAccounts.Models.Domain.Vouchers;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;

namespace Neo.EasyAccounts.Data.Initializers
{
	internal static class VouchersInitializer
	{
		public static void SeedInitialData(DbEntities context)
		{
			var journalVouchers = new List<JournalVoucher> { 
				new JournalVoucher(){ Type = "JV", Number = "JV-00001-16", Date = DateTime.Now, DateCreated = DateTime.Now, CreatedBy = "1", IsDeleted = false, IsActive = true },
				new JournalVoucher(){ Type = "JV", Number = "JV-00002-16", Date = DateTime.Now, DateCreated = DateTime.Now, CreatedBy = "1", IsDeleted = false, IsActive = true },
				new JournalVoucher(){ Type = "JV", Number = "JV-00003-16", Date = DateTime.Now, DateCreated = DateTime.Now, CreatedBy = "1", IsDeleted = false, IsActive = true },
				new JournalVoucher(){ Type = "JV", Number = "JV-00004-16", Date = DateTime.Now, DateCreated = DateTime.Now, CreatedBy = "1", IsDeleted = false, IsActive = true }
			};
			journalVouchers.ForEach(d => context.JournalVouchers.AddOrUpdate(p => p.ID, d));
			context.SaveChanges();

			var journalVoucherDetails = new List<JournalVoucherDetail> { 
				new JournalVoucherDetail(){ AccountID = 1, JournalVoucherID = 1, Debit = 1500, Credit = 0, Narration = "Testing Initial Data", DateCreated = DateTime.Now, CreatedBy = "1", IsDeleted = false, IsActive = true },
				new JournalVoucherDetail(){ AccountID = 2, JournalVoucherID = 1, Debit = 0, Credit = 1500, Narration = "Testing Initial Data", DateCreated = DateTime.Now, CreatedBy = "1", IsDeleted = false, IsActive = true },

				new JournalVoucherDetail(){ AccountID = 1, JournalVoucherID = 2, Debit = 2500, Credit = 0, Narration = "Testing Initial Data", DateCreated = DateTime.Now, CreatedBy = "1", IsDeleted = false, IsActive = true },
				new JournalVoucherDetail(){ AccountID = 2, JournalVoucherID = 2, Debit = 0, Credit = 2500, Narration = "Testing Initial Data", DateCreated = DateTime.Now, CreatedBy = "1", IsDeleted = false, IsActive = true },

				new JournalVoucherDetail(){ AccountID = 1, JournalVoucherID = 3, Debit = 3500, Credit = 0, Narration = "Testing Initial Data", DateCreated = DateTime.Now, CreatedBy = "1", IsDeleted = false, IsActive = true },
				new JournalVoucherDetail(){ AccountID = 2, JournalVoucherID = 3, Debit = 0, Credit = 3500, Narration = "Testing Initial Data", DateCreated = DateTime.Now, CreatedBy = "1", IsDeleted = false, IsActive = true },

				new JournalVoucherDetail(){ AccountID = 1, JournalVoucherID = 4, Debit = 4500, Credit = 0, Narration = "Testing Initial Data", DateCreated = DateTime.Now, CreatedBy = "1", IsDeleted = false, IsActive = true },
				new JournalVoucherDetail(){ AccountID = 2, JournalVoucherID = 4, Debit = 0, Credit = 4500, Narration = "Testing Initial Data", DateCreated = DateTime.Now, CreatedBy = "1", IsDeleted = false, IsActive = true }
			};
			journalVoucherDetails.ForEach(d => context.JournalVoucherDetails.AddOrUpdate(p => p.ID, d));
			context.SaveChanges();

			var paymentVouchers = new List<PaymentVoucher> { 
				new PaymentVoucher(){ Type = "PV", SupplierID = 1, Number = "PV-00001-16", Date = DateTime.Now, DateCreated = DateTime.Now, CreatedBy = "1", IsDeleted = false, IsActive = true },
				new PaymentVoucher(){ Type = "PV", SupplierID = 1, Number = "PV-00002-16", Date = DateTime.Now, DateCreated = DateTime.Now, CreatedBy = "1", IsDeleted = false, IsActive = true },
				new PaymentVoucher(){ Type = "PV", SupplierID = 1, Number = "PV-00003-16", Date = DateTime.Now, DateCreated = DateTime.Now, CreatedBy = "1", IsDeleted = false, IsActive = true },
				new PaymentVoucher(){ Type = "PV", SupplierID = 1, Number = "PV-00004-16", Date = DateTime.Now, DateCreated = DateTime.Now, CreatedBy = "1", IsDeleted = false, IsActive = true }
			};
			paymentVouchers.ForEach(d => context.PaymentVouchers.AddOrUpdate(p => p.ID, d));
			context.SaveChanges();

			var paymentVoucherDetails = new List<PaymentVoucherDetail> { 
				new PaymentVoucherDetail(){ AccountID = 1, PaymentVoucherID = 1, Debit = 1500, Credit = 0, Narration = "Testing Initial Data", DateCreated = DateTime.Now, CreatedBy = "1", IsDeleted = false, IsActive = true },
				new PaymentVoucherDetail(){ AccountID = 2, PaymentVoucherID = 1, Debit = 0, Credit = 1500, Narration = "Testing Initial Data", DateCreated = DateTime.Now, CreatedBy = "1", IsDeleted = false, IsActive = true },

				new PaymentVoucherDetail(){ AccountID = 1, PaymentVoucherID = 2, Debit = 2500, Credit = 0, Narration = "Testing Initial Data", DateCreated = DateTime.Now, CreatedBy = "1", IsDeleted = false, IsActive = true },
				new PaymentVoucherDetail(){ AccountID = 2, PaymentVoucherID = 2, Debit = 0, Credit = 2500, Narration = "Testing Initial Data", DateCreated = DateTime.Now, CreatedBy = "1", IsDeleted = false, IsActive = true },

				new PaymentVoucherDetail(){ AccountID = 1, PaymentVoucherID = 3, Debit = 3500, Credit = 0, Narration = "Testing Initial Data", DateCreated = DateTime.Now, CreatedBy = "1", IsDeleted = false, IsActive = true },
				new PaymentVoucherDetail(){ AccountID = 2, PaymentVoucherID = 3, Debit = 0, Credit = 3500, Narration = "Testing Initial Data", DateCreated = DateTime.Now, CreatedBy = "1", IsDeleted = false, IsActive = true },

				new PaymentVoucherDetail(){ AccountID = 1, PaymentVoucherID = 4, Debit = 4500, Credit = 0, Narration = "Testing Initial Data", DateCreated = DateTime.Now, CreatedBy = "1", IsDeleted = false, IsActive = true },
				new PaymentVoucherDetail(){ AccountID = 2, PaymentVoucherID = 4, Debit = 0, Credit = 4500, Narration = "Testing Initial Data", DateCreated = DateTime.Now, CreatedBy = "1", IsDeleted = false, IsActive = true }
			};
			paymentVoucherDetails.ForEach(d => context.PaymentVoucherDetails.AddOrUpdate(p => p.ID, d));
			context.SaveChanges();

			var receiptVouchers = new List<ReceiptVoucher> { 
				new ReceiptVoucher(){ Type = "RV", CustomerID = 1, Number = "RV-00001-16", Date = DateTime.Now, DateCreated = DateTime.Now, CreatedBy = "1", IsDeleted = false, IsActive = true },
				new ReceiptVoucher(){ Type = "RV", CustomerID = 1, Number = "RV-00002-16", Date = DateTime.Now, DateCreated = DateTime.Now, CreatedBy = "1", IsDeleted = false, IsActive = true },
				new ReceiptVoucher(){ Type = "RV", CustomerID = 1, Number = "RV-00003-16", Date = DateTime.Now, DateCreated = DateTime.Now, CreatedBy = "1", IsDeleted = false, IsActive = true },
				new ReceiptVoucher(){ Type = "RV", CustomerID = 1, Number = "RV-00004-16", Date = DateTime.Now, DateCreated = DateTime.Now, CreatedBy = "1", IsDeleted = false, IsActive = true }
			};
			receiptVouchers.ForEach(d => context.ReceiptVouchers.AddOrUpdate(p => p.ID, d));
			context.SaveChanges();

			var receiptVoucherDetails = new List<ReceiptVoucherDetail> { 
				new ReceiptVoucherDetail(){ AccountID = 1, ReceiptVoucherID = 1, Debit = 1500, Credit = 0, Narration = "Testing Initial Data", DateCreated = DateTime.Now, CreatedBy = "1", IsDeleted = false, IsActive = true },
				new ReceiptVoucherDetail(){ AccountID = 2, ReceiptVoucherID = 1, Debit = 0, Credit = 1500, Narration = "Testing Initial Data", DateCreated = DateTime.Now, CreatedBy = "1", IsDeleted = false, IsActive = true },

				new ReceiptVoucherDetail(){ AccountID = 1, ReceiptVoucherID = 2, Debit = 2500, Credit = 0, Narration = "Testing Initial Data", DateCreated = DateTime.Now, CreatedBy = "1", IsDeleted = false, IsActive = true },
				new ReceiptVoucherDetail(){ AccountID = 2, ReceiptVoucherID = 2, Debit = 0, Credit = 2500, Narration = "Testing Initial Data", DateCreated = DateTime.Now, CreatedBy = "1", IsDeleted = false, IsActive = true },

				new ReceiptVoucherDetail(){ AccountID = 1, ReceiptVoucherID = 3, Debit = 3500, Credit = 0, Narration = "Testing Initial Data", DateCreated = DateTime.Now, CreatedBy = "1", IsDeleted = false, IsActive = true },
				new ReceiptVoucherDetail(){ AccountID = 2, ReceiptVoucherID = 3, Debit = 0, Credit = 3500, Narration = "Testing Initial Data", DateCreated = DateTime.Now, CreatedBy = "1", IsDeleted = false, IsActive = true },

				new ReceiptVoucherDetail(){ AccountID = 1, ReceiptVoucherID = 4, Debit = 4500, Credit = 0, Narration = "Testing Initial Data", DateCreated = DateTime.Now, CreatedBy = "1", IsDeleted = false, IsActive = true },
				new ReceiptVoucherDetail(){ AccountID = 2, ReceiptVoucherID = 4, Debit = 0, Credit = 4500, Narration = "Testing Initial Data", DateCreated = DateTime.Now, CreatedBy = "1", IsDeleted = false, IsActive = true }
			};
			receiptVoucherDetails.ForEach(d => context.ReceiptVoucherDetails.AddOrUpdate(p => p.ID, d));
			context.SaveChanges();
		}
	}
}
