using AutoMapper;
using ClassifiedAds.Auth;
using Microsoft.AspNet.Identity;
using Neo.EasyAccounts.Models.Domain.Vouchers;
using Neo.EasyAccounts.Service;
using Neo.EasyAccounts.Service.Accounts;
using Neo.EasyAccounts.Service.Masters;
using Neo.EasyAccounts.Service.Vouchers;
using Neo.EasyAccounts.Web.UI.Areas.Vouchers.ViewModels;
using Neo.EasyAccounts.Web.UI.Caching;
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
	public class PaymentVouchersController : Controller
	{
		private IdentityContext identityContext = new IdentityContext();
		private readonly ILogger logger = LoggerFactory.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
		private readonly IPaymentVoucherService service;
		private readonly IPaymentVoucherDetailService details;
		private readonly IAccountTitleService accountTitles;
		private readonly ICacheService cacheService;
		private readonly IAutoGenNoSettingService autoGenNoSettingsService;
		private readonly ISupplierService supplierService;
		List<UI.ViewModels.ActionOutput> actionMessges = new List<UI.ViewModels.ActionOutput>();
		private int getRegEditID
		{
			get
			{
				var regEditID = Convert.ToString(Request.QueryString["ID"]);

				return String.IsNullOrEmpty(regEditID) ? Convert.ToInt32(regEditID) : 0;
			}
		}

		public PaymentVouchersController(IPaymentVoucherService service, IPaymentVoucherDetailService details, IAccountTitleService accountTitles, ISupplierService supplierService,
			IAutoGenNoSettingService autoGenNoSettingsService, ICacheService cacheService)
		{
			this.service = service;
			this.details = details;
			this.accountTitles = accountTitles;
			this.cacheService = cacheService;
			this.autoGenNoSettingsService = autoGenNoSettingsService;
			this.supplierService = supplierService;
		}

		public async Task<ActionResult> Index()
		{
			IEnumerable<PaymentVoucher> list = null;
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
			return View(list ?? new List<PaymentVoucher>());
		}

		public async Task<ActionResult> RegEdit(long? id)
		{
			var viewModel = new PaymentVoucherViewModel();
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

					viewModel = Mapper.Map<PaymentVoucherViewModel>(entity);

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
			viewModel.Suppliers = WebUtilHelper.GetSuppliers(supplierService);
			viewModel.Accounts = WebUtilHelper.GetAccountTitles(accountTitles);
			return View(viewModel);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult RegEdit([Bind(Include = "ID,SupplierID,Number,Date,Description,IsActive,RowCount,CreatedBy,DateCreated")] PaymentVoucherViewModel viewModel)
		{
			try
			{
				if (ModelState.IsValid)
				{
					bool isUpdate = viewModel.ID > 0;
					var entity = Mapper.Map<PaymentVoucher>(viewModel);
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
			viewModel.Suppliers = WebUtilHelper.GetSuppliers(supplierService);
			viewModel.Accounts = WebUtilHelper.GetAccountTitles(accountTitles);
			return View(viewModel);
		}

		[HttpPost]
		[ValidateAntiForgeryTokenFromRequestHeader]
		public ActionResult RegEditDetail([Bind(Include = "VoucherID,Details")] KODetailsViewModel<PaymentVoucherDetailViewModel> viewModel)
		{
			var vM = new PaymentVoucherViewModel();
			try
			{
				if (ModelState.IsValid)
				{
					List<PaymentVoucherDetail> entityList = null;
					if (viewModel.Details != null && viewModel.Details.Count > 0)
					{
						var detailEntities = viewModel.Details.Select(d => Mapper.Map<PaymentVoucherDetail>(d)).ToList();
						entityList = new List<PaymentVoucherDetail>();

						PaymentVoucherDetail x = null;

						var currentUserID = Convert.ToInt32(HttpContext.User.Identity.GetUserId());

						foreach (var item in detailEntities)
						{
							x = new PaymentVoucherDetail();
							x = item;
							x.PaymentVoucherID = viewModel.VoucherID;

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
			vM = Mapper.Map<PaymentVoucherViewModel>(service.Get(viewModel.VoucherID));
			vM.DetailsList = viewModel.Details;
			vM.Suppliers = WebUtilHelper.GetSuppliers(supplierService);
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

					var detailsViewModelList = detailsList.Select(d => Mapper.Map<PaymentVoucherDetailViewModel>(d)).ToList();

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