using ClassifiedAds.Auth;
using Neo.EasyAccounts.Service.Accounts;
using Neo.EasyAccounts.Service.Locations;
using Neo.EasyAccounts.Service.Masters;
using Neo.EasyAccounts.Web.UI.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Xml;
using System.Xml.Serialization;

namespace Neo.EasyAccounts.Web.UI.Helpers
{
	public class WebUtilHelper
	{
		//private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

		public static string ApplicationBaseURL
		{
			get
			{
				var request = HttpContext.Current.Request;
				string appUrl = HttpRuntime.AppDomainAppVirtualPath;

				if (!string.IsNullOrWhiteSpace(appUrl)) appUrl += "/";

				string baseUrl = string.Format("{0}://{1}{2}", request.Url.Scheme, request.Url.Authority, appUrl);
				return baseUrl;
			}
		}

		public static void TravelportRequestResponseLog(dynamic RequestResponseObject)
		{
			if (WebConfigAppSettingsHelper.IsTravelPortReqResLoging)
			{
				string filePath = Path.Combine(System.Web.Hosting.HostingEnvironment.ApplicationPhysicalPath, @"App_Data/TravelportRequestResponseLog.txt");

				string objectType = RequestResponseObject.GetType().FullName;
				string objectText = SerializeXML(RequestResponseObject);

				if (objectText != null && objectText.Length > 0)
				{
					if (!File.Exists(filePath))
					{
						File.Create(filePath);
					}
					else
					{
						var fileInfo = new FileInfo(filePath);

						//Clear file if it exceeds 100mbs
						long hundredMB = 1024 * 1024 * 100;
						if (fileInfo.Length > hundredMB)
						{
							using (var writer = new StreamWriter(filePath))
							{
								writer.Write("");
								writer.Close();
							}
						}
					}
					File.SetAttributes(filePath, FileAttributes.Normal);
					string fileContents = File.ReadAllText(filePath);

					using (var writer = new StreamWriter(filePath))
					{
						if (objectType.Contains("LowFareSearchReq"))
						{
							writer.WriteLine("----------------------------------------------------------------------------------");
							writer.WriteLine(string.Format("Date : {0}", DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss.ffff")));
						}
						writer.WriteLine("----------------------------------------------------------------------------------");
						writer.WriteLine(string.Format("** {0} **", objectType));
						writer.WriteLine("----------------------------------------------------------------------------------");
						writer.WriteLine(objectText);

						if (fileContents != null && fileContents.Length > 0) writer.WriteLine(fileContents);
						writer.Close();
					}
				}
			}
		}
		public static string BytesToString(long byteCount)
		{
			string[] suf = { "B", "KB", "MB", "GB", "TB", "PB", "EB" }; //Longs run out around EB

			if (byteCount == 0) return "0" + suf[0];

			long bytes = Math.Abs(byteCount);
			int place = Convert.ToInt32(Math.Floor(Math.Log(bytes, 1024)));
			double num = Math.Round(bytes / Math.Pow(1024, place), 1);
			return (Math.Sign(byteCount) * num).ToString() + suf[place];
		}
		public static string GetPaymentGatwayErrorDetails(int ErrorCode)
		{
			string errorDetails = null;
			switch (ErrorCode)
			{
				case 10001:
					errorDetails = "Invalid Merchant ID";
					break;
				case 10002:
					errorDetails = "Invalid Referral URL";
					break;
				case 10003:
					errorDetails = "Invalid Amount";
					break;
				case 10004:
					errorDetails = "Invalid Expiry Month";
					break;
				case 10005:
					errorDetails = "Invalid Expiry Year";
					break;
				case 10006:
					errorDetails = "Refund amount greater than transaction amount";
					break;
				case 10007:
					errorDetails = " The request is missing one or more required fields";
					break;
				case 10008:
					errorDetails = "One or more fields in the request contains invalid data";
					break;
				case 10009:
					errorDetails = "Invalid NIOnlineRefID";
					break;
				case 20001:
					errorDetails = "Expired card. You might also receive this if the expiration date you provided does not match the date the issuing bank has on file.";
					break;
				case 20002:
					errorDetails = "General decline of the card. No other information was provided by the issuing bank.";
					break;
				case 20003:
					errorDetails = "Insufficient funds in the account.";
					break;
				case 20004:
					errorDetails = "Stolen or lost card.";
					break;
				case 20005:
					errorDetails = "Issuing bank unavailable.";
					break;
				case 20006:
					errorDetails = "Inactive card or card not authorized for card-not-present transactions.";
					break;
				case 20007:
					errorDetails = " American Express Card Identification Digits (CID) did not match";
					break;
				case 20008:
					errorDetails = "The card has reached the credit limit.";
					break;
				case 20009:
					errorDetails = "Invalid CVN.";
					break;
				case 20010:
					errorDetails = " Invalid account number.";
					break;
				case 20011:
					errorDetails = "The card type is not accepted by the payment processor.";
					break;
				case 20012:
					errorDetails = "The requested capture amount exceeds the originally authorized amount.";
					break;
				case 20013:
					errorDetails = "General decline by the processor.";
					break;
				case 20014:
					errorDetails = "The authorization has already been reversed.";
					break;
				case 20015:
					errorDetails = "The authorization has already been captured.";
					break;
				case 20016:
					errorDetails = "The requested transaction amount must match the previous transaction amount.";
					break;
				case 20017:
					errorDetails = "You requested a capture, but there is no corresponding, unused authorization record.";
					break;
				case 20018:
					errorDetails = "The order is rejected by FRM Module.";
					break;
				case 20019:
					errorDetails = "The transaction has already been settled or reversed.";
					break;
				case 20020:
					errorDetails = "The customer cannot be authenticated.";
					break;
				case 20021:
					errorDetails = "Server Timeout.";
					break;
				case 20022:
					errorDetails = "The request was received but there was a server timeout. This error does not include timeouts between the client and the server.";
					break;
				default:
					errorDetails = "Some unknown error occurred.";
					break;
			}
			return errorDetails;
		}

		public static Dictionary<string, string> ReturnHttpHeader()
		{
			var httpHeaders = new Dictionary<string, string>();

			httpHeaders.Add("Authorization", "Basic " + Convert.ToBase64String(
				Encoding.ASCII.GetBytes(WebConfigAppSettingsHelper.UserName + ":" + WebConfigAppSettingsHelper.Password))
			);

			return httpHeaders;
		}

		public static SelectList GetDays()
		{
			List<SelectListItem> list = new List<SelectListItem>();

			for (int i = 1; i < 32; i++)
			{
				list.Add(new SelectListItem { Value = i.ToString(), Text = i.ToString() });
			}

			return new SelectList(list, "Value", "Text");
		}
		public static SelectList GetMonths(string valueType)
		{
			List<SelectListItem> list = new List<SelectListItem>();

			switch (valueType)
			{
				case "names":
					var months = System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.AbbreviatedMonthNames.ToList();
					months.RemoveAll(str => String.IsNullOrEmpty(str));

					foreach (string month in months)
					{
						list.Add(new SelectListItem { Value = month, Text = month });
					}
					break;
				case "numbers":
					for (int i = 1; i < 13; i++)
					{
						list.Add(new SelectListItem { Value = i.ToString(), Text = i.ToString() });
					}
					break;
				default:
					for (int i = 1; i < 13; i++)
					{
						list.Add(new SelectListItem { Value = i.ToString(), Text = i.ToString() });
					}
					break;
			}
			return new SelectList(list, "Value", "Text");
		}
		public static SelectList GetMonths()
		{
			string[] months = System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.MonthNames;

			List<SelectListItem> list = new List<SelectListItem>();

			foreach (string month in months)
			{
				if (month.Length > 0) list.Add(new SelectListItem { Value = month, Text = month });
			}
			return new SelectList(list, "Value", "Text");
		}
		public static SelectList GetYears(int upperLimit, int lowerLimit)
		{
			List<SelectListItem> list = new List<SelectListItem>();

			for (int i = upperLimit; i > lowerLimit + 1; i--)
			{
				list.Add(new SelectListItem { Value = i.ToString(), Text = i.ToString() });
			}

			return new SelectList(list, "Value", "Text");
		}
		public static SelectList GetYears()
		{
			List<SelectListItem> list = new List<SelectListItem>();

			for (int i = DateTime.Now.Year; i < DateTime.Now.Year + 8; i++)
			{
				list.Add(new SelectListItem { Value = i.ToString(), Text = i.ToString() });
			}

			return new SelectList(list, "Value", "Text");
		}

		public static SelectList GetNameTitles()
		{
			List<SelectListItem> list = new List<SelectListItem>();

			list.AddRange(new SelectListItem[] { 
				new SelectListItem { Value = "Mr", Text = "Mr" },
				new SelectListItem { Value = "Mrs", Text = "Mrs" },
				new SelectListItem { Value = "Ms", Text = "Ms" }
			});

			return new SelectList(list, "Value", "Text");
		}

		public static SelectList GetRoles()
		{
			using (var db = new IdentityContext())
			{
				var dataList = db.Roles.OrderBy(d => d.Name).ToList();

				var list = new List<SelectListItem>();

				list.AddRange(dataList.Select(d => new SelectListItem()
				{
					Value = d.Id.ToString(),
					Text = d.Name
				}).ToList());

				list.ToList().Insert(0, new SelectListItem() { Text = "-", Value = "", Selected = true });

				return new SelectList(list, "Value", "Text");
			}
		}
		public static SelectList GetRolesWithoutID()
		{
			using (var db = new IdentityContext())
			{
				var dataList = db.Roles.OrderBy(d => d.Name).ToList();

				var list = new List<SelectListItem>();

				list.AddRange(dataList.Select(d => new SelectListItem()
				{
					Value = d.Name,
					Text = d.Name
				}).ToList());
				list.ToList().Insert(0, new SelectListItem() { Text = "-", Value = "", Selected = true });

				return new SelectList(list, "Value", "Text");
			}
		}
		public static Dictionary<string, string> GetRolesDictionary()
		{
			using (var db = new IdentityContext())
			{
				var dataList = db.Roles.OrderBy(d => d.Name).ToList();

				var list = new Dictionary<string, string>();

				dataList.ForEach(d => list.Add(d.Name, d.Name));
				return list;
			}
		}

		public static List<RadioButtonItem> GetTripTypes()
		{
			var tripTypes = new List<RadioButtonItem>() { 
				new RadioButtonItem { 
					Name = "One Way", 
					Selected = true 
				}, 
				new RadioButtonItem { 
					Name = "Round Trip", 
					Selected = false 
				}, 
				new RadioButtonItem { 
					Name = "Multi City / Stop Over", 
					Selected = false 
				}
			};
			return tripTypes;
		}

		public static SelectList GetCountries(ICountryService service)
		{
			var list = service.GetAll().OrderBy(d => d.Name)
											.Select(d => new SelectListItem()
											{
												Value = d.ID.ToString(),
												Text = d.Name
											});

			return new SelectList(list, "Value", "Text");
		}
		public static SelectList GetStates(IStateService service)
		{
			var list = service.GetAll().OrderBy(d => d.Name)
											.Select(d => new SelectListItem()
											{
												Value = d.ID.ToString(),
												Text = d.Name
											});

			return new SelectList(list, "Value", "Text");
		}
		public static SelectList GetCities(ICityService service)
		{
			var list = service.GetAll().OrderBy(d => d.Name)
											.Select(d => new SelectListItem()
											{
												Value = d.ID.ToString(),
												Text = d.Name
											});

			return new SelectList(list, "Value", "Text");
		}
		public static SelectList GetAreas(IAreaService service)
		{
			var list = service.GetAll().OrderBy(d => d.Name)
											.Select(d => new SelectListItem()
											{
												Value = d.ID.ToString(),
												Text = d.Name
											});

			return new SelectList(list, "Value", "Text");
		}

		public static SelectList GetAccountTypes(IAccountTypeService service)
		{
			var list = service.GetAll().OrderBy(d => d.Name)
											.Select(d => new SelectListItem()
											{
												Value = d.ID.ToString(),
												Text = d.Name
											});

			return new SelectList(list, "Value", "Text");
		}
		public static SelectList GetAccountGroups(IAccountGroupService service)
		{
			var list = service.GetAll().OrderBy(d => d.Name)
											.Select(d => new SelectListItem()
											{
												Value = d.ID.ToString(),
												Text = d.Name
											});

			return new SelectList(list, "Value", "Text");
		}
		public static SelectList GetAccountSubGroups(IAccountSubGroupService service)
		{
			var list = service.GetAll().OrderBy(d => d.Name)
											.Select(d => new SelectListItem()
											{
												Value = d.ID.ToString(),
												Text = d.Name
											});

			return new SelectList(list, "Value", "Text");
		}
		public static SelectList GetAccountTitles(IAccountTitleService service)
		{
			var list = service.GetAll().OrderBy(d => d.Name)
											.Select(d => new SelectListItem()
											{
												Value = d.ID.ToString(),
												Text = d.Name
											});

			return new SelectList(list, "Value", "Text");
		}

		public static SelectList GetSuppliers(ISupplierService service)
		{
			var list = service.GetAll().OrderBy(d => d.Name)
											.Select(d => new SelectListItem()
											{
												Value = d.ID.ToString(),
												Text = d.Name
											});

			return new SelectList(list, "Value", "Text");
		}
		public static SelectList GetCustomers(ICustomerService service)
		{
			var list = service.GetAll().OrderBy(d => d.Name)
											.Select(d => new SelectListItem()
											{
												Value = d.ID.ToString(),
												Text = d.Name
											});

			return new SelectList(list, "Value", "Text");
		}

		private static void TestEmail(string toEmail, string ccEmail)
		{
			var fromAddress = new System.Net.Mail.MailAddress(WebConfigAppSettingsHelper.OrderMailerUName, "Fly Online Booking");
			var toAddress = new System.Net.Mail.MailAddress(toEmail, "");
			var toAddressCC = new System.Net.Mail.MailAddress(ccEmail, "");

			var mm = new System.Net.Mail.MailMessage(fromAddress, toAddress);

			mm.CC.Add(toAddressCC);

			string mailContent = "This is just a testing email";

			mm.Subject = "Your Email Subject";
			mm.Body = mailContent;
			mm.IsBodyHtml = true;

			using (var smtp = new System.Net.Mail.SmtpClient())
			{
				smtp.Host = WebConfigAppSettingsHelper.SMTPHost;
				smtp.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
				var NetworkCred = new System.Net.NetworkCredential();
				NetworkCred.UserName = WebConfigAppSettingsHelper.OrderMailerUName;
				NetworkCred.Password = WebConfigAppSettingsHelper.OrderMailerPwd;
				smtp.Credentials = NetworkCred;
				smtp.Port = WebConfigAppSettingsHelper.PortNo;
				smtp.Send(mm);
			}
		}

		/// <summary>
		/// Sends Email Asynchronously
		/// </summary>
		/// <param name="fromEmailAddress">Email address for "From". Array having EmailAddress,DisplayName</param>
		/// <param name="toEmailAddress">Email address for "To". Array having EmailAddress,DisplayName</param>
		/// <param name="ccEmailAddresses">Email addresses for "CC". Array of Array having EmailAddress,DisplayName</param>
		/// <param name="emailSubject">Email Subject</param>
		/// <param name="emailMessage">Email Message</param>
		/// <param name="credentials">Login Password for sending the email</param>
		/// <param name="IsBodyHTML">Contents of the email message is Plain text or HTML</param>
		/// <returns></returns>
		public static async Task SendEmailAsync(string[] fromEmailAddress, string[] toEmailAddress, string[][] ccEmailAddresses, string[][] bCCEmailAddresses, string emailSubject, string emailMessage, System.Net.NetworkCredential credentials, bool IsBodyHTML = true)
		{
			var fromAddress = new System.Net.Mail.MailAddress(fromEmailAddress[0], fromEmailAddress[1]);
			var toAddress = new System.Net.Mail.MailAddress(toEmailAddress[0], toEmailAddress[1]);
			var message = new System.Net.Mail.MailMessage(fromAddress, toAddress);

			if (ccEmailAddresses != null && ccEmailAddresses.Count() > 0)
			{
				foreach (var item in ccEmailAddresses)
				{
					message.CC.Add(new System.Net.Mail.MailAddress(item[0], item[1]));
				}
			}
			if (bCCEmailAddresses != null && bCCEmailAddresses.Count() > 0)
			{
				foreach (var item in bCCEmailAddresses)
				{
					message.Bcc.Add(new System.Net.Mail.MailAddress(item[0], item[1]));
				}
			}

			message.Subject = emailSubject;
			message.Body = emailMessage;
			message.IsBodyHtml = IsBodyHTML;

			using (var smtpClient = new System.Net.Mail.SmtpClient())
			{
				smtpClient.Host = WebConfigAppSettingsHelper.SMTPHost;
				smtpClient.Port = WebConfigAppSettingsHelper.PortNo;
				smtpClient.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
				smtpClient.Credentials = credentials;
				await smtpClient.SendMailAsync(message);
			}
		}
		public static void SendEmail(string[] fromEmailAddress, string[] toEmailAddress, string[][] ccEmailAddresses, string[][] bCCEmailAddresses, string emailSubject, string emailMessage, System.Net.NetworkCredential credentials, bool IsBodyHTML = true)
		{
			var fromAddress = new System.Net.Mail.MailAddress(fromEmailAddress[0], fromEmailAddress[1]);
			var toAddress = new System.Net.Mail.MailAddress(toEmailAddress[0], toEmailAddress[1]);
			var message = new System.Net.Mail.MailMessage(fromAddress, toAddress);

			if (ccEmailAddresses != null && ccEmailAddresses.Count() > 0)
			{
				foreach (var item in ccEmailAddresses)
				{
					message.CC.Add(new System.Net.Mail.MailAddress(item[0], item[1]));
				}
			}
			if (bCCEmailAddresses != null && bCCEmailAddresses.Count() > 0)
			{
				foreach (var item in bCCEmailAddresses)
				{
					message.Bcc.Add(new System.Net.Mail.MailAddress(item[0], item[1]));
				}
			}

			message.Subject = emailSubject;
			message.Body = emailMessage;
			message.IsBodyHtml = IsBodyHTML;

			var smtpClient = new System.Net.Mail.SmtpClient(WebConfigAppSettingsHelper.SMTPHost, WebConfigAppSettingsHelper.PortNo);

			smtpClient.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
			smtpClient.Credentials = credentials;

			smtpClient.SendCompleted += (sender, e) =>
			{
				smtpClient.Dispose();
				message.Dispose();
			};
			smtpClient.Send(message);
		}

		public static string SerializeXML(dynamic serializeableObject)
		{
			string xml = null;
			using (var xmlStream = new System.IO.MemoryStream())
			{
				var xmlDoc = new XmlDocument();
				var xmlSerializer = new XmlSerializer(serializeableObject.GetType());

				xmlSerializer.Serialize(xmlStream, serializeableObject);

				xmlStream.Position = 0;
				xmlDoc.Load(xmlStream);
				xml = xmlDoc.InnerXml;
			}
			return xml;
		}
		public static string Serialize<T>(T obj, bool omitXMLDeclaration = true, bool omitXMLNamespace = true) where T : class
		{
			var serializer = new XmlSerializer(typeof(T));
			using (MemoryStream memStream = new MemoryStream())
			{
				XmlWriterSettings settings = new XmlWriterSettings
				{
					Encoding = Encoding.UTF8,
					Indent = true,
					OmitXmlDeclaration = omitXMLDeclaration
				};

				using (XmlWriter writer = XmlWriter.Create(memStream, settings))
				{
					XmlSerializerNamespaces xns = new XmlSerializerNamespaces();
					if ((omitXMLNamespace))
						xns.Add("", "");
					serializer.Serialize(writer, obj, xns);
				}

				return Encoding.UTF8.GetString(memStream.ToArray());
			}
		}
		public static T Deserialize<T>(string xml) where T : class , new()
		{
			T result = default(T);
			var serializer = new XmlSerializer(typeof(T));

			using (MemoryStream memStream = new MemoryStream())
			{
				byte[] bytes = Encoding.UTF8.GetBytes(xml);
				memStream.Write(bytes, 0, bytes.Count());
				memStream.Seek(0, SeekOrigin.Begin);

				using (XmlReader reader = XmlReader.Create(memStream))
				{
					result = (T)serializer.Deserialize(reader);
				}
			}
			return result;
		}
	}
}