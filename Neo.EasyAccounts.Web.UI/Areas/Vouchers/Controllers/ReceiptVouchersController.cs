using AutoMapper;
using ClassifiedAds.Auth;
using Microsoft.AspNet.Identity;
using Neo.EasyAccounts.Models.Domain.Vouchers;
using Neo.EasyAccounts.Service;
using Neo.EasyAccounts.Service.Accounts;
using Neo.EasyAccounts.Service.Masters;
using Neo.EasyAccounts.Service.Vouchers;
using Neo.EasyAccounts.Web.UI.Areas.Vouchers.ViewModels;
using Neo.EasyAccounts.Web.UI.Filters;
using Neo.EasyAccounts.Web.UI.Helpers;
using Neo.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Neo.EasyAccounts.Web.UI.Areas.Vouchers.Controllers
{
	[Authorize]
	public class ReceiptVouchersController : Controller
	{
		private readonly IReceiptVoucherService service;
		private readonly IReceiptVoucherDetailService details;
		private readonly IAccountTitleService accountTitles;
		private readonly IAutoGenNoSettingService autoGenNoSettingsService;
		private readonly ICustomerService customerService;
		private readonly ILogger logger;
		List<UI.ViewModels.ActionOutput> actionMessges;
		private int getRegEditID
		{
			get
			{
				var regEditID = Convert.ToString(Request.QueryString["ID"]);

				return String.IsNullOrEmpty(regEditID) ? Convert.ToInt32(regEditID) : 0;
			}
		}

		public ReceiptVouchersController(IReceiptVoucherService service, IReceiptVoucherDetailService details, IAccountTitleService accountTitles, ICustomerService customerService, IAutoGenNoSettingService autoGenNoSettingsService)
		{
			this.service = service;
			this.details = details;
			this.accountTitles = accountTitles;
			this.autoGenNoSettingsService = autoGenNoSettingsService;
			this.customerService = customerService;
			this.actionMessges = new List<UI.ViewModels.ActionOutput>();
			this.logger = LoggerFactory.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
		}

		public async Task<ActionResult> Index()
		{
			IEnumerable<ReceiptVoucher> list = null;
			try
			{
				if (TempData["ActionOutput"] != null)
				{
					var actionMessges = TempData["ActionOutput"] as List<Neo.EasyAccounts.Web.UI.ViewModels.ActionOutput>;
					this.AddErrorToViewBag(actionMessges);
				}
				list = await service.GetAllAsync();
			}
			catch (Exception ex)
			{
				Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
				logger.Fatal(ex);

				actionMessges.Add(
					new UI.ViewModels.ActionOutput()
					{
						MessageTitle = Helpers.AlertMessages.LoadErrorTitle,
						MessageDetails = Helpers.AlertMessages.LoadErrorMessage,
						ActionType = Helpers.AlertActionTypes.Read,
						AlertType = Helpers.AlertTypes.Warning
					}
				);
				actionMessges.Add(
					new UI.ViewModels.ActionOutput()
					{
						MessageDetails = Helpers.AlertMessages.LoadErrorMessage,
						ActionType = Helpers.AlertActionTypes.Read,
						AlertType = Helpers.AlertTypes.Error,
						Exception = ex
					}
				);
				ViewBag.ActionOutput = actionMessges;
			}
			return View(list ?? new List<ReceiptVoucher>());
		}

		public async Task<ActionResult> RegEdit(long? id)
		{
			var viewModel = new ReceiptVoucherViewModel();
			try
			{
				if (TempData["ActionOutput"] != null)
				{
					var actionMessges = TempData["ActionOutput"] as List<Neo.EasyAccounts.Web.UI.ViewModels.ActionOutput>;
					this.AddErrorToViewBag(actionMessges);
				}
				if (id != null && id.HasValue)
				{
					var entity = service.Get(id.Value);

					if (entity == null) return HttpNotFound();

					viewModel = Mapper.Map<ReceiptVoucherViewModel>(entity);

					var detailsList = await details.GetAllByVoucherIDAsync(entity.ID);

					if (detailsList != null && detailsList.Count() > 0)
					{
						viewModel.RowCount = detailsList.Count();
					}
					else
						viewModel.RowCount = Convert.ToInt32(Request.QueryString["rowCount"]);
					string metaInfo = string.Format("Created By {0} on {1}", viewModel.CreatedBy, viewModel.DateCreated.ToString("dd MMM yyyy hh:mm:ss"));

					metaInfo += viewModel.ModifiedBy.HasValue() ? " \n Modified By :" + viewModel.ModifiedBy : "";
					metaInfo += viewModel.DateModified.HasValue ? " on :" + viewModel.DateModified.Value.ToString("dd MMM yyyy hh:mm:ss") : "";

					ViewBag.MetaInfo = metaInfo;
				}
			}
			catch (Exception ex)
			{
				Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
				logger.Fatal(ex);
				actionMessges.Add(new UI.ViewModels.ActionOutput()
				{
					MessageTitle = Helpers.AlertMessages.GeneralErrorTitle,
					MessageDetails = Helpers.AlertMessages.GeneralErrorMessage,
					ActionType = Helpers.AlertActionTypes.Saved,
					AlertType = Helpers.AlertTypes.Warning
				});
				actionMessges.Add(new UI.ViewModels.ActionOutput()
				{
					MessageDetails = Helpers.AlertMessages.GeneralErrorTitle,
					ActionType = Helpers.AlertActionTypes.Read,
					AlertType = Helpers.AlertTypes.Error,
					Exception = ex
				});
				this.AddErrorToViewBag(actionMessges);
			}
			viewModel.Cutomers = WebUtilHelper.GetCustomers(customerService);
			viewModel.Accounts = WebUtilHelper.GetAccountTitles(accountTitles);
			return View(viewModel);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult RegEdit([Bind(Include = "ID,CustomerID,Number,Date,Description,IsActive,RowCount,CreatedBy,DateCreated")] ReceiptVoucherViewModel viewModel)
		{
			try
			{
				if (ModelState.IsValid)
				{
					bool isUpdate = viewModel.ID > 0;
					var entity = Mapper.Map<ReceiptVoucher>(viewModel);
					entity.Number = ServiceUtility.GenerateNumber(entity, service, autoGenNoSettingsService);
					service.Save(entity);

					actionMessges.Add(new UI.ViewModels.ActionOutput()
					{
						MessageTitle = isUpdate ? Helpers.AlertMessages.UpdateTitle : Helpers.AlertMessages.SaveMasterSuccessfullMessage,
						MessageDetails = isUpdate ? Helpers.AlertMessages.UpdateSuccessfullMessage : Helpers.AlertMessages.SaveSuccessfullMessage,
						ActionType = isUpdate ? Helpers.AlertActionTypes.Updated : Helpers.AlertActionTypes.Saved,
						AlertType = Helpers.AlertTypes.Success
					});
					TempData["ActionOutput"] = actionMessges;

					return RedirectToAction("RegEdit", new { id = entity.ID, rowCount = viewModel.RowCount });
				}
			}
			catch (Exception ex)
			{
				Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
				logger.Fatal(ex);
				actionMessges.Add(new UI.ViewModels.ActionOutput()
				{
					MessageTitle = Helpers.AlertMessages.GeneralErrorTitle,
					MessageDetails = Helpers.AlertMessages.GeneralErrorMessage,
					ActionType = Helpers.AlertActionTypes.Saved,
					AlertType = Helpers.AlertTypes.Warning
				});
				actionMessges.Add(new UI.ViewModels.ActionOutput()
				{
					MessageDetails = Helpers.AlertMessages.GeneralErrorTitle,
					ActionType = Helpers.AlertActionTypes.Read,
					AlertType = Helpers.AlertTypes.Error,
					Exception = ex
				});
				this.AddErrorToViewBag(actionMessges);
			}
			viewModel.Cutomers = WebUtilHelper.GetCustomers(customerService);
			viewModel.Accounts = WebUtilHelper.GetAccountTitles(accountTitles);
			return View(viewModel);
		}

		[HttpPost]
		[ValidateAntiForgeryTokenFromRequestHeader]
		public ActionResult RegEditDetail([Bind(Include = "VoucherID,Details")] KODetailsViewModel<PaymentVoucherDetailViewModel> viewModel)
		{
			var vM = new ReceiptVoucherViewModel();
			try
			{
				if (ModelState.IsValid)
				{
					List<ReceiptVoucherDetail> entityList = null;
					if (viewModel.Details != null && viewModel.Details.Count > 0)
					{
						var detailEntities = viewModel.Details.Select(d => Mapper.Map<ReceiptVoucherDetail>(d)).ToList();
						entityList = new List<ReceiptVoucherDetail>();

						ReceiptVoucherDetail x = null;
						
						foreach (var item in detailEntities)
						{
							x = new ReceiptVoucherDetail();
							x = item;
							x.ReceiptVoucherID = viewModel.VoucherID;

							entityList.Add(x);
						}
						details.Save(entityList);
						actionMessges.Add(new UI.ViewModels.ActionOutput()
						{
							MessageTitle = Helpers.AlertMessages.SaveMasterSuccessfullMessage,
							MessageDetails = Helpers.AlertMessages.SaveSuccessfullMessage,
							ActionType = Helpers.AlertActionTypes.Saved,
							AlertType = Helpers.AlertTypes.Success
						});
						TempData["ActionOutput"] = actionMessges;

						return RedirectToAction("Index");
					}
					actionMessges.Add(new UI.ViewModels.ActionOutput()
					{
						MessageTitle = Helpers.AlertMessages.GeneralErrorTitle,
						MessageDetails = Helpers.AlertMessages.SaveErrorDataNotReceived,
						ActionType = Helpers.AlertActionTypes.Saved,
						AlertType = Helpers.AlertTypes.Warning
					});
				}
			}
			catch (Exception ex)
			{
				Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
				logger.Fatal(ex);
				actionMessges.Add(new UI.ViewModels.ActionOutput()
				{
					MessageTitle = Helpers.AlertMessages.GeneralErrorTitle,
					MessageDetails = Helpers.AlertMessages.GeneralErrorMessage,
					ActionType = Helpers.AlertActionTypes.Saved,
					AlertType = Helpers.AlertTypes.Warning
				});
				actionMessges.Add(new UI.ViewModels.ActionOutput()
				{
					MessageDetails = Helpers.AlertMessages.GeneralErrorTitle,
					ActionType = Helpers.AlertActionTypes.Read,
					AlertType = Helpers.AlertTypes.Error,
					Exception = ex
				});
			}
			this.AddErrorToViewBag(actionMessges);
			vM = Mapper.Map<ReceiptVoucherViewModel>(service.Get(viewModel.VoucherID));
			vM.DetailsList = viewModel.Details;
			vM.Cutomers = WebUtilHelper.GetCustomers(customerService);
			vM.Accounts = WebUtilHelper.GetAccountTitles(accountTitles);
			return View("RegEdit", vM);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Delete(long id)
		{
			try
			{
				if (id < 0) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

				int outPut = service.Delete(id, true);
				actionMessges.Add(new UI.ViewModels.ActionOutput()
				{
					MessageTitle = Helpers.AlertMessages.DeleteTitle,
					MessageDetails = Helpers.AlertMessages.DeleteSuccessfullMessage,
					ActionType = Helpers.AlertActionTypes.Saved,
					AlertType = Helpers.AlertTypes.Success
				});
				TempData["ActionOutput"] = actionMessges;
				return RedirectToAction("Index");
			}
			catch (Exception ex)
			{
				Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
				logger.Fatal(ex);
				actionMessges.Add(new UI.ViewModels.ActionOutput()
				{
					MessageTitle = Helpers.AlertMessages.DeleteErrorTitle,
					MessageDetails = Helpers.AlertMessages.DeleteErrorMessage,
					ActionType = Helpers.AlertActionTypes.Deleted,
					AlertType = Helpers.AlertTypes.Warning
				});
				actionMessges.Add(new UI.ViewModels.ActionOutput()
				{
					MessageDetails = Helpers.AlertMessages.GeneralErrorTitle,
					ActionType = Helpers.AlertActionTypes.Deleted,
					AlertType = Helpers.AlertTypes.Error,
					Exception = ex
				});
				this.AddErrorToViewBag(actionMessges);
				TempData["ActionOutput"] = actionMessges;
				return RedirectToAction("Index");
			}
		}

		[HttpGet]
		public async Task<string> GetAccounts()
		{
			string output = null;
			try
			{
				var accountsList = await accountTitles.GetAllAsync();
				var jsonData = accountsList.Select(d => new { id = d.ID, text = String.Format("{0} - {2}"), d.Code, d.Name });
				output = Newtonsoft.Json.JsonConvert.SerializeObject(jsonData);
			}
			catch (Exception ex)
			{
				Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
				logger.Fatal(ex);
				actionMessges.Add(new UI.ViewModels.ActionOutput()
				{
					MessageTitle = Helpers.AlertMessages.GeneralErrorTitle,
					MessageDetails = Helpers.AlertMessages.GeneralErrorMessage,
					ActionType = Helpers.AlertActionTypes.Saved,
					AlertType = Helpers.AlertTypes.Warning
				});
				actionMessges.Add(new UI.ViewModels.ActionOutput()
				{
					MessageDetails = Helpers.AlertMessages.GeneralErrorTitle,
					ActionType = Helpers.AlertActionTypes.Read,
					AlertType = Helpers.AlertTypes.Error,
					Exception = ex
				});
				this.AddErrorToViewBag(actionMessges);
			}
			return output;
		}

		[HttpPost]
		public async Task<JsonResult> GetVoucherDetails(int voucherID)
		{
			string jsonString = null;
			try
			{
				if (voucherID > 0)
				{
					var detailsList = await details.GetAllByVoucherIDAsync(voucherID);

					var detailsViewModelList = detailsList.Select(d => Mapper.Map<ReceiptVoucherDetailViewModel>(d)).ToList();

					jsonString = Newtonsoft.Json.JsonConvert.SerializeObject(detailsViewModelList);
				}
			}
			catch (Exception ex)
			{
				Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
				logger.Fatal(ex);
				actionMessges.Add(new UI.ViewModels.ActionOutput()
				{
					MessageTitle = Helpers.AlertMessages.GeneralErrorTitle,
					MessageDetails = Helpers.AlertMessages.GeneralErrorMessage,
					ActionType = Helpers.AlertActionTypes.Saved,
					AlertType = Helpers.AlertTypes.Warning
				});
				actionMessges.Add(new UI.ViewModels.ActionOutput()
				{
					MessageDetails = Helpers.AlertMessages.GeneralErrorTitle,
					ActionType = Helpers.AlertActionTypes.Read,
					AlertType = Helpers.AlertTypes.Error,
					Exception = ex
				});
				this.AddErrorToViewBag(actionMessges);
			}
			return Json(jsonString, JsonRequestBehavior.AllowGet);
		}
	}
}