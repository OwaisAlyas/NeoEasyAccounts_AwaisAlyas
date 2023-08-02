using AutoMapper;
using Microsoft.AspNet.Identity;
using Neo.EasyAccounts.Models.Domain.Vouchers;
using Neo.EasyAccounts.Service;
using Neo.EasyAccounts.Service.Accounts;
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
	public class JournalVouchersController : Controller
	{
		private readonly IJournalVoucherDetailService details;
		private readonly IJournalVoucherService service;
		private readonly IAccountTitleService accountTitles;
		private readonly ICacheService cacheService;
		private readonly IAutoGenNoSettingService autoGenNoSettingsService;
		private readonly ILogger logger;
		private List<UI.ViewModels.ActionOutput> actionMessges;

		private int getRegEditID
		{
			get
			{
				var regEditID = Convert.ToString(Request.QueryString["ID"]);

				return String.IsNullOrEmpty(regEditID) ? Convert.ToInt32(regEditID) : 0;
			}
		}

		public JournalVouchersController(IJournalVoucherService service, IJournalVoucherDetailService details, IAccountTitleService accountTitles,
			IAutoGenNoSettingService autoGenNoSettingsService, ICacheService cacheService)
		{
			this.service = service;
			this.details = details;
			this.accountTitles = accountTitles;
			this.cacheService = cacheService;
			this.autoGenNoSettingsService = autoGenNoSettingsService;
			this.actionMessges = new List<UI.ViewModels.ActionOutput>();
			this.logger = LoggerFactory.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
		}

		public async Task<ActionResult> Index()
		{
			IEnumerable<JournalVoucher> list = null;
			try
			{
				if (TempData["ActionOutput"] != null)
				{
					var actionMessges = TempData["ActionOutput"] as List<Neo.EasyAccounts.Web.UI.ViewModels.ActionOutput>;
					this.AddErrorToViewBag(actionMessges);
				}
				list = await service.GetAllAsync();

				var k = service.GetAll(new string[] { "JournalVoucherDetails" }, 0, 2, null);
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
			return View(list ?? new List<JournalVoucher>());
		}

		public async Task<ActionResult> RegEdit(long? id)
		{
			var viewModel = new JournalVoucherViewModel();
			try
			{
				if (TempData["ActionOutput"] != null)
				{
					var actionMessges = TempData["ActionOutput"] as List<Neo.EasyAccounts.Web.UI.ViewModels.ActionOutput>;
					this.AddErrorToViewBag(actionMessges);
				}
				if (id != null && id.HasValue)
				{
					//var entity = cacheService.GetOrSet("journalVoucher.Accounts", 2, () => service.Get(id.Value));
					var entity = service.Get(id.Value);

					if (entity == null) return HttpNotFound();

					viewModel = Mapper.Map<JournalVoucherViewModel>(entity);

					//var detailsList = await cacheService.GetOrSet("vouchers.JVDetails", 2, () => details.GetAllByVoucherIDAsync(entity.ID));
					var detailsList = await details.GetAllByVoucherIDAsync(entity.ID);

					//viewModel.DetailsList = detailsList.Select(d => Mapper.Map<JournalVoucherDetailViewModel>(d));

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
			viewModel.Accounts = WebUtilHelper.GetAccountTitles(accountTitles);
			return View(viewModel);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult RegEdit([Bind(Include = "ID,Number,Date,Description,IsActive,RowCount,CreatedBy,DateCreated")] JournalVoucherViewModel viewModel)
		{
			try
			{
				if (ModelState.IsValid)
				{
					bool isUpdate = viewModel.ID > 0;
					var entity = Mapper.Map<JournalVoucher>(viewModel);
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
			viewModel.Accounts = WebUtilHelper.GetAccountTitles(accountTitles);
			return View(viewModel);
		}

		[HttpPost]
		[ValidateAntiForgeryTokenFromRequestHeader]
		public ActionResult RegEditDetail([Bind(Include = "voucherID,details")] KODetailsViewModel<JournalVoucherDetailViewModel> viewModel)
		{
			var vM = new JournalVoucherViewModel();
			try
			{
				if (ModelState.IsValid)
				{
					List<JournalVoucherDetail> entityList = null;
					if (viewModel.Details != null && viewModel.Details.Count > 0)
					{
						var entity = viewModel.Details.Select(d => Mapper.Map<JournalVoucherDetail>(d)).ToList();
						entityList = new List<JournalVoucherDetail>();

						JournalVoucherDetail x = null;

						var currentUserID = Convert.ToInt32(HttpContext.User.Identity.GetUserId());

						foreach (var item in entity)
						{
							x = new JournalVoucherDetail();
							x = item;
							x.JournalVoucherID = viewModel.VoucherID;
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
			//vM = Mapper.Map<JournalVoucherViewModel>(cacheService.GetOrSet("journalVoucher.Accounts", 2, () => service.Get(viewModel.voucherID)));
			vM = Mapper.Map<JournalVoucherViewModel>(service.Get(viewModel.VoucherID));
			vM.DetailsList = viewModel.Details;
			//vM.Accounts = cacheService.GetOrSet("journalVoucher.Accounts", 15, () => WebUtilHelper.GetAccountTitles(accountTitles));
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
				//var accountsList = await cacheService.GetOrSet("vouchers.AccountJSON", 2, () => accountTitles.GetAllAsync());
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
					//var detailsList = await cacheService.GetOrSet("vouchers.JVDetails", 2, () => details.GetAllByVoucherIDAsync(voucherID));
					var detailsList = await details.GetAllByVoucherIDAsync(voucherID);
					var detailsViewModelList = detailsList.Select(d => Mapper.Map<JournalVoucherDetailViewModel>(d)).ToList();

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