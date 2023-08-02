using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Neo.EasyAccounts.Web.UI.Helpers
{
	/// <summary>
	/// Contains all the accessible information for the AppSettings section of web configuration file
	/// </summary>
	public class WebConfigAppSettingsHelper
	{
		private static string GetAppSettingKey(string Key)
		{
			string value = null;

			if (Key != null && Key.Length > 0) value = System.Configuration.ConfigurationManager.AppSettings[Key];

			return value;
		}

		public static string MerchantIDAED
		{
			get { return GetAppSettingKey("MerchantIDAED"); }
		}
		public static string EncryptionKeyAED
		{
			get { return GetAppSettingKey("encryptionIDAED"); }
		}
		public static string MerchantIDUSD
		{
			get { return GetAppSettingKey("MerchantIDUSD"); }
		}
		public static string EncryptionKeyUSD
		{
			get { return GetAppSettingKey("encryptionIDUSD"); }
		}
		public static string PaymentURL
		{
			get { return GetAppSettingKey("PaymentURL"); }
		}
		public static string SuccessURL
		{
			get { return WebUtilHelper.ApplicationBaseURL + GetAppSettingKey("SuccessURL"); }
		}
		public static string FailureURL
		{
			get { return WebUtilHelper.ApplicationBaseURL + GetAppSettingKey("FailureURL"); }
		}
		public static string LogoURL
		{
			get { return GetAppSettingKey("LogoURL"); }
		}

		public static string UserName
		{
			get { return GetAppSettingKey("UserName"); }
		}
		public static string Password
		{
			get { return GetAppSettingKey("Password"); }
		}
		public static string AuthorizedBy
		{
			get { return GetAppSettingKey("AuthorizedBy"); }
		}
		public static string TraceID
		{
			get { return GetAppSettingKey("TraceID"); }
		}
		public static string OriginApplication
		{
			get { return GetAppSettingKey("OriginApplication"); }
		}
		public static string TargetBranch
		{
			get { return GetAppSettingKey("TargetBranch"); }
		}
		public static string Provider
		{
			get { return GetAppSettingKey("Provider"); }
		}

		public static string UserNamePP
		{
			get { return GetAppSettingKey("UserNamepp"); }
		}
		public static string PasswordPP
		{
			get { return GetAppSettingKey("PasswordPP"); }
		}
		public static string TargetBranchPP
		{
			get { return GetAppSettingKey("TargetBranchPP"); }
		}

		public static string AdultPassengerCode
		{
			get { return GetAppSettingKey("AdultPassengerCode"); }
		}
		public static string ChildPassengerCode
		{
			get { return GetAppSettingKey("ChildPassengerCode"); }
		}
		public static string InfantPassengerCode
		{
			get { return GetAppSettingKey("InfantPassengerCode"); }
		}

		public static bool IsPreProduction
		{
			get { return Convert.ToBoolean(GetAppSettingKey("IsPreProduction")); }
		}
		public static bool IsTravelPortReqResLoging
		{
			get { return Convert.ToBoolean(GetAppSettingKey("IsTravelPortReqResLoging")); }
		}

		public static string SMTPHost
		{
			get { return GetAppSettingKey("SMTPHost"); }
		}
		public static string CustomerCareMailId
		{
			get { return GetAppSettingKey("CustomerCareMailId"); }
		}
		public static string OrderMailerUName
		{
			get { return GetAppSettingKey("OrderMailerUName"); }
		}
		public static string InfoMail
		{
			get { return GetAppSettingKey("InfoMail"); }
		}
		public static string OrderMailerPwd
		{
			get { return GetAppSettingKey("OrderMailerPwd"); }
		}
		public static int PortNo
		{
			get { return Convert.ToInt32(GetAppSettingKey("PortNo")); }
		}

		public static string DevEmail
		{
			get { return GetAppSettingKey("DevEmail"); }
		}
		public static string DevEmailName
		{
			get { return GetAppSettingKey("DevEmailName"); }
		}

		public static string BossEmail
		{
			get { return GetAppSettingKey("BossEmail"); }
		}
		public static string BossEmailName
		{
			get { return GetAppSettingKey("BossEmailName"); }
		}

		public static string InfoEMail
		{
			get { return GetAppSettingKey("InfoEMail"); }
		}
		public static string InfoEMailName
		{
			get { return GetAppSettingKey("InfoEMailName"); }
		}
	}
}