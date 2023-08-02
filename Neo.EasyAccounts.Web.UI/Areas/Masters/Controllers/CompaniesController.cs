using AutoMapper;
using ClassifiedAds.Auth;
using Microsoft.AspNet.Identity;
using Neo.EasyAccounts.Models.Domain;
using Neo.EasyAccounts.Service.Masters;
using Neo.EasyAccounts.Web.UI.Areas.Masters.ViewModels;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Net;

namespace Neo.EasyAccounts.Web.UI.Areas.Masters.Controllers
{
	[Authorize]
	public class CompaniesController : Controller
	{
		private List<UI.ViewModels.ActionOutput> actionMessges;
		private readonly Neo.Logging.ILogger logger;
		private readonly ICompanyService service;

		public CompaniesController(ICompanyService service)
		{
			this.service = service;
			this.actionMessges = new List<UI.ViewModels.ActionOutput>();
			this.logger = Neo.Logging.LoggerFactory.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
		}

		// GET: Countries
		public async Task<ActionResult> Index()
		{
			var list = new List<Company>().AsEnumerable();
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

		public ActionResult RegEdit(long? id)
		{
			var viewModel = new CompanyViewModel();
			
			try
			{
				if (id != null && id.HasValue)
				{
					var entity = service.Get(id.Value);

					if (entity == null) return HttpNotFound();

					viewModel = Mapper.Map<CompanyViewModel>(entity);

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
			return View(viewModel);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult RegEdit([Bind(Include = "ID,Name,Code,OwnerName,Description,IsActive,CreatedBy,DateCreated")] CompanyViewModel viewModel)
		{
			try
			{
				if (ModelState.IsValid)
				{
					bool isUpdate = viewModel.ID > 0;
					var entity = Mapper.Map<Company>(viewModel);

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