using AutoMapper;
using Neo.EasyAccounts.Models.Domain.Accounts;
using Neo.EasyAccounts.Service.Accounts;
using Neo.EasyAccounts.Web.UI.Areas.Accounts.ViewModels;
using Neo.EasyAccounts.Web.UI.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Neo.EasyAccounts.Web.UI.Areas.Accounts.Controllers
{
	[Authorize]
	public class AccountGroupsController : Controller
	{
		private List<UI.ViewModels.ActionOutput> actionMessges;
		private readonly Neo.Logging.ILogger logger;
		private readonly IAccountGroupService service;
		private readonly IAccountTitleService accountTitles;
		private readonly IAccountSubGroupService subGroups;

		public AccountGroupsController(IAccountGroupService service, IAccountSubGroupService subGroups, IAccountTitleService accountTitles)
		{
			this.service = service;
			this.accountTitles = accountTitles;
			this.subGroups = subGroups;
			this.actionMessges = new List<UI.ViewModels.ActionOutput>();
			this.logger = Neo.Logging.LoggerFactory.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
		}

		public async Task<ActionResult> Index()
		{
			var list = new List<AccountGroup>().AsEnumerable();
			try
			{
				if (TempData["ActionOutput"] != null)
				{
					var actionMessges = TempData["ActionOutput"] as List<Neo.EasyAccounts.Web.UI.ViewModels.ActionOutput>;
					this.AddErrorToViewBag(actionMessges);
				}
				list = await service.GetAllAsync();
				return View(list);
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
						MessageDetails = Helpers.AlertMessages.GeneralErrorTitle,
						ActionType = Helpers.AlertActionTypes.Read,
						AlertType = Helpers.AlertTypes.Error,
						Exception = ex
					}
				);
				this.AddErrorToViewBag(actionMessges);
				return View(list);
			}
		}

		public async Task<ActionResult> RegEdit(long? id)
		{
			var viewModel = new AccountGroupViewModel();
			try
			{
				if (id != null && id.HasValue)
				{
					var entity = service.Get(id.Value);

					if (entity == null) return HttpNotFound();

					viewModel = Mapper.Map<AccountGroupViewModel>(entity);

					string metaInfo = string.Format("Created By {0} on {1}", viewModel.CreatedBy, viewModel.DateCreated.ToString("dd MMM yyyy hh:mm:ss"));

					metaInfo += viewModel.ModifiedBy.HasValue() ? " \n Modified By :" + viewModel.ModifiedBy : "";
					metaInfo += viewModel.DateModified.HasValue ? " on :" + viewModel.DateModified.Value.ToString("dd MMM yyyy hh:mm:ss") : "";

					ViewBag.MetaInfo = metaInfo;
				}
				viewModel.SubGroupListing = (id != null && id.HasValue) ? await subGroups.GetAllByAccountSubGroupAsync(id.Value) : await subGroups.GetAllAsync();
				viewModel.AccountTypes = WebUtilHelper.GetAccountTitles(accountTitles);
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
			return View(viewModel);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult RegEdit([Bind(Include = "ID,AccountTypeID,Name,Code,Description,IsActive,CreatedBy,DateCreated")] AccountGroupViewModel viewModel)
		{
			try
			{
				if (ModelState.IsValid)
				{
					bool isUpdate = viewModel.ID > 0;
					var entity = Mapper.Map<AccountGroup>(viewModel);

					service.Save(entity);
					actionMessges.Add(new UI.ViewModels.ActionOutput()
					{
						MessageTitle = isUpdate ? Helpers.AlertMessages.UpdateTitle : Helpers.AlertMessages.SaveTitle,
						MessageDetails = isUpdate ? Helpers.AlertMessages.UpdateSuccessfullMessage : Helpers.AlertMessages.SaveSuccessfullMessage,
						ActionType = isUpdate ? Helpers.AlertActionTypes.Updated : Helpers.AlertActionTypes.Saved,
						AlertType = Helpers.AlertTypes.Success
					});
					TempData["ActionOutput"] = actionMessges;
					return RedirectToAction("Index");
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
			viewModel.SubGroupListing = (viewModel.ID > 0) ? subGroups.GetAllByAccountSubGroup(viewModel.ID) : subGroups.GetAll();
			viewModel.AccountTypes = WebUtilHelper.GetAccountTitles(accountTitles);
			return View(viewModel);
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
	}
}