using Neo.EasyAccounts.Data.Repositories;
using Neo.EasyAccounts.Data.Repositories.Vouchers;
using Neo.EasyAccounts.Models.Domain.Vouchers;
using Neo.EasyAccounts.Service.Vouchers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting;
using System.Text;
using System.Threading.Tasks;

namespace Neo.EasyAccounts.Service
{
	public static class ServiceUtility
	{
		//public static string GenerateNumber(JournalVoucher entity, IJournalVoucherService repo, IAutoGenNoSettingRepository autoGenNoSettingsRepo)
		//{
		//	if (entity == null) return null;
		//	if (entity.Number != null && entity.Number.Length > 0) return entity.Number;

		//	string generatedNumber = null;
		//	bool conflict = true;
		//	int countOut = 0;

		//	try
		//	{
		//		countOut = 5; //Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["RegNoGenerationAttempts"]);
		//	}
		//	catch (Exception)
		//	{
		//		countOut = 25;
		//	}
		//	do
		//	{
		//		var k = repo.GetAll().ToList();
		//		var regNos = new List<int>();
		//		int regNo = 0;
		//		int maxFormNo = repo.GetAll()
		//			.Where(d => d.Number != null && int.TryParse(d.Number.Split('-')[1], out regNo))
		//			.Select(d => (d.Number != null && d.Number.Trim().Length > 0 && d.Number.Contains('-') && d.Number.Split('-').Length > 0) ? int.Parse(d.Number.Split('-')[1]) : 0)
		//			.Max();

		//		foreach (var item in k)
		//		{
		//			// To avoid empty form number counted in this list
		//			int regNO = 0;

		//			if (item.Number == null)
		//				regNos.Add(0);
		//			else if (int.TryParse(item.Number.Split('-')[1], out regNO))
		//				regNos.Add(regNO);
		//			else
		//				regNos.Add(0);
		//		}

		//		if (regNos.Count > 0) maxFormNo = regNos.Max();

		//		maxFormNo++;

		//		var settings = autoGenNoSettingsRepo.Get(d => d.EntityName.Equals(entity.GetType().Name));

		//		string numberOfDigitsStringFormat = "D" + settings.NoOfDigits;

		//		generatedNumber = String.Format("{1}{0}{2}{0}{3}",
		//			settings.Separator,
		//			settings.PreFix,
		//			maxFormNo.ToString(numberOfDigitsStringFormat),
		//			DateTime.Now.ToString("yy")
		//		);
		//		if (settings.PostFix != null && settings.PostFix.Length > 0)
		//		{
		//			generatedNumber = String.Format("{0}{1}{2}", generatedNumber, settings.Separator, settings.PostFix);
		//		}

		//		int CountServiceFormNo = repo.GetAll((generatedNumber).Count();

		//		if (CountServiceFormNo > 0)
		//		{
		//			conflict = true;
		//			System.Threading.Thread.Sleep(1500);
		//		}
		//		else
		//		{
		//			conflict = false;
		//		}
		//		countOut--;
		//	} while (conflict && countOut > 0);

		//	if (conflict) throw new Exception("Unable to generate a Unique Number. Please Contact Support for Resolution.");

		//	return generatedNumber;
		//}

		//public static string GenerateNumber(PaymentVoucher entity, IPaymentVoucherRepository repo, IAutoGenNoSettingRepository autoGenNoSettingsRepo)
		//{
		//	if (entity == null) return null;
		//	if (entity.Number != null && entity.Number.Length > 0) return entity.Number;

		//	string generatedNumber = null;
		//	bool conflict = true;
		//	int countOut = 0;

		//	try
		//	{
		//		countOut = 5; //Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["RegNoGenerationAttempts"]);
		//	}
		//	catch (Exception)
		//	{
		//		countOut = 25;
		//	}
		//	do
		//	{
		//		var k = repo.GetAll().ToList();
		//		var regNos = new List<int>();
		//		int regNo = 0;
		//		int maxFormNo = repo.GetAll()
		//			.Where(d => d.Number != null && int.TryParse(d.Number.Split('-')[1], out regNo))
		//			.Select(d => (d.Number != null && d.Number.Trim().Length > 0 && d.Number.Contains('-') && d.Number.Split('-').Length > 0) ? int.Parse(d.Number.Split('-')[1]) : 0)
		//			.Max();

		//		foreach (var item in k)
		//		{
		//			// To avoid empty form number counted in this list
		//			int regNO = 0;

		//			if (item.Number == null)
		//				regNos.Add(0);
		//			else if (int.TryParse(item.Number.Split('-')[1], out regNO))
		//				regNos.Add(regNO);
		//			else
		//				regNos.Add(0);
		//		}

		//		if (regNos.Count > 0) maxFormNo = regNos.Max();

		//		maxFormNo++;

		//		var settings = autoGenNoSettingsRepo.Get(d => d.EntityName.Equals(entity.GetType().Name));

		//		string numberOfDigitsStringFormat = "D" + settings.NoOfDigits;

		//		generatedNumber = String.Format("{1}{0}{2}{0}{3}",
		//			settings.Separator,
		//			settings.PreFix,
		//			maxFormNo.ToString(numberOfDigitsStringFormat),
		//			DateTime.Now.ToString("yy")
		//		);
		//		if (settings.PostFix != null && settings.PostFix.Length > 0)
		//		{
		//			generatedNumber = String.Format("{0}{1}{2}", generatedNumber, settings.Separator, settings.PostFix);
		//		}

		//		int CountServiceFormNo = repo.GetAll(generatedNumber).Count();

		//		if (CountServiceFormNo > 0)
		//		{
		//			conflict = true;
		//			System.Threading.Thread.Sleep(1500);
		//		}
		//		else
		//		{
		//			conflict = false;
		//		}
		//		countOut--;
		//	} while (conflict && countOut > 0);

		//	if (conflict) throw new Exception("Unable to generate a Unique Number. Please Contact Support for Resolution.");

		//	return generatedNumber;
		//}

		//public static string GenerateNumber(ReceiptVoucher entity, IReceiptVoucherRepository repo, IAutoGenNoSettingRepository autoGenNoSettingsRepo)
		//{
		//	if (entity == null) return null;
		//	if (entity.Number != null && entity.Number.Length > 0) return entity.Number;

		//	string generatedNumber = null;
		//	bool conflict = true;
		//	int countOut = 0;

		//	try
		//	{
		//		countOut = 5; //Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["RegNoGenerationAttempts"]);
		//	}
		//	catch (Exception)
		//	{
		//		countOut = 25;
		//	}
		//	do
		//	{
		//		var k = repo.GetAll().ToList();
		//		var regNos = new List<int>();
		//		int regNo = 0;
		//		int maxFormNo = repo.GetAll()
		//			.Where(d => d.Number != null && int.TryParse(d.Number.Split('-')[1], out regNo))
		//			.Select(d => (d.Number != null && d.Number.Trim().Length > 0 && d.Number.Contains('-') && d.Number.Split('-').Length > 0) ? int.Parse(d.Number.Split('-')[1]) : 0)
		//			.Max();

		//		foreach (var item in k)
		//		{
		//			// To avoid empty form number counted in this list
		//			int regNO = 0;

		//			if (item.Number == null)
		//				regNos.Add(0);
		//			else if (int.TryParse(item.Number.Split('-')[1], out regNO))
		//				regNos.Add(regNO);
		//			else
		//				regNos.Add(0);
		//		}

		//		if (regNos.Count > 0) maxFormNo = regNos.Max();

		//		maxFormNo++;

		//		var settings = autoGenNoSettingsRepo.Get(d => d.EntityName.Equals(entity.GetType().Name));

		//		string numberOfDigitsStringFormat = "D" + settings.NoOfDigits;

		//		generatedNumber = String.Format("{1}{0}{2}{0}{3}",
		//			settings.Separator,
		//			settings.PreFix,
		//			maxFormNo.ToString(numberOfDigitsStringFormat),
		//			DateTime.Now.ToString("yy")
		//		);
		//		if (settings.PostFix != null && settings.PostFix.Length > 0)
		//		{
		//			generatedNumber = String.Format("{0}{1}{2}", generatedNumber, settings.Separator, settings.PostFix);
		//		}

		//		int CountServiceFormNo = repo.GetAll(generatedNumber).Count();

		//		if (CountServiceFormNo > 0)
		//		{
		//			conflict = true;
		//			System.Threading.Thread.Sleep(1500);
		//		}
		//		else
		//		{
		//			conflict = false;
		//		}
		//		countOut--;
		//	} while (conflict && countOut > 0);

		//	if (conflict) throw new Exception("Unable to generate a Unique Number. Please Contact Support for Resolution.");

		//	return generatedNumber;
		//}

		public static string GenerateNumber(dynamic entity, dynamic service, IAutoGenNoSettingService autoGenNoSettingsService)
		{
			if (entity == null) return null;
			if (entity.Number != null && entity.Number.Length > 0) return entity.Number;

			string generatedNumber = null;
			bool conflict = true;
			int countOut = 0;

			try
			{
				countOut = 5; //Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["RegNoGenerationAttempts"]);
			}
			catch (Exception)
			{
				countOut = 25;
			}
			do
			{
				Type repoType = ((object)service).GetType();
				
				int regNo = 0;
				int maxFormNo = 0;

				switch (repoType.Name)
				{
					case "PaymentVoucherService":
						{
							maxFormNo = ((IPaymentVoucherService)service).GetAll()
							   .Where(d => d.Number != null && int.TryParse(d.Number.Split('-')[1], out regNo))
							   .Select(d => (d.Number != null && d.Number.Trim().Length > 0 && d.Number.Contains('-') && d.Number.Split('-').Length > 0) ? int.Parse(d.Number.Split('-')[1]) : 0)
							   .Max();
							break;
						}
					case "JournalVoucherService":
						{
							maxFormNo = ((IJournalVoucherService)service).GetAll()
							   .Where(d => d.Number != null && int.TryParse(d.Number.Split('-')[1], out regNo))
							   .Select(d => (d.Number != null && d.Number.Trim().Length > 0 && d.Number.Contains('-') && d.Number.Split('-').Length > 0) ? int.Parse(d.Number.Split('-')[1]) : 0)
							   .Max();
							break;
						}
					case "ReceiptVoucherService":
						{
							maxFormNo = ((IReceiptVoucherService)service).GetAll()
							   .Where(d => d.Number != null && int.TryParse(d.Number.Split('-')[1], out regNo))
							   .Select(d => (d.Number != null && d.Number.Trim().Length > 0 && d.Number.Contains('-') && d.Number.Split('-').Length > 0) ? int.Parse(d.Number.Split('-')[1]) : 0)
							   .Max();
							break;
						}
					default:
						break;
				}

				maxFormNo++;
				string entityName = entity.GetType().Name;
				var settings = autoGenNoSettingsService.GetByEntity(entityName);

				string numberOfDigitsStringFormat = "D" + settings.NoOfDigits;

				generatedNumber = String.Format("{1}{0}{2}{0}{3}",
					settings.Separator,
					settings.PreFix,
					maxFormNo.ToString(numberOfDigitsStringFormat),
					DateTime.Now.ToString("yy")
				);
				if (settings.PostFix != null && settings.PostFix.Length > 0)
				{
					generatedNumber = String.Format("{0}{1}{2}", generatedNumber, settings.Separator, settings.PostFix);
				}

				//int CountServiceFormNo = repo.GetAll(generatedNumber).Count();
				int countServiceFormNo = 0;

				switch (repoType.Name)
				{
					case "PaymentVoucherService":
						{
							countServiceFormNo = ((IPaymentVoucherService)service).GetAll(generatedNumber).Count();
							break;
						}
					case "JournalVoucherService":
						{
							countServiceFormNo = ((IJournalVoucherService)service).GetAll(generatedNumber).Count();
							break;
						}
					case "ReceiptVoucherService":
						{
							countServiceFormNo = ((IReceiptVoucherService)service).GetAll(generatedNumber).Count();
							break;
						}
					default:
						break;
				}				

				if (countServiceFormNo > 0)
				{
					conflict = true;
					System.Threading.Thread.Sleep(1500);
				}
				else
				{
					conflict = false;
				}
				countOut--;
			} while (conflict && countOut > 0);

			if (conflict) throw new Exception("Unable to generate a Unique Number. Please Contact Support for Resolution.");

			return generatedNumber;
		}


	}
}