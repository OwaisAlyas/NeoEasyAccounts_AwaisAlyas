using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ClassifiedAds.Auth;
using AutoMapper;
using Microsoft.AspNet.Identity;
using Neo.EasyAccounts.Service.Locations;
using Neo.EasyAccounts.Models.Domain.Locations;
using Neo.EasyAccounts.Web.UI.Areas.Locations.ViewModels;
using Neo.Common.Data.PagedData;

namespace Neo.EasyAccounts.Web.UI.Areas.Locations.Controllers
{
	[Authorize]
	public class CountriesController : Controller
	{
		private readonly ICountryService service;
		private readonly IStateService states;
		private readonly Neo.Logging.ILogger logger;
		private List<UI.ViewModels.ActionOutput> actionMessges;
		private const int DefaultPageSize = 2;

		private SortExpression<Country>[] sortExpression = new SortExpression<Country>[]{ 
				new SortExpression<Country>(d => d.ID, System.ComponentModel.ListSortDirection.Ascending)
			};

		public CountriesController(ICountryService service, IStateService states)
		{
			this.service = service;
			this.states = states;
			this.actionMessges = new List<UI.ViewModels.ActionOutput>();
			this.logger = Neo.Logging.LoggerFactory.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
		}

		public ActionResult IndexAjax()
		{
			int currentPageIndex = 1;

			var list = service.GetAll(currentPageIndex, 3, sortExpression);
			return View(list);
		}

		public ActionResult Pager(int? page)
		{
			int currentPageIndex = page.HasValue ? page.Value - 1 : 0;
			var cities = service.GetAll(currentPageIndex, 3, sortExpression);
			return PartialView("_countryListingPartial", cities);
		}

		public ActionResult Index(int? page)
		{
			var list = new List<Country>().ToPagedList(0,0,0);
			try
			{
				if (TempData["ActionOutput"] != null)
				{
					var actionMessges = TempData["ActionOutput"] as List<Neo.EasyAccounts.Web.UI.ViewModels.ActionOutput>;
					this.AddErrorToViewBag(actionMessges);
				}
				int currentPageIndex = page.HasValue ? page.Value - 1 : 0;
				list = service.GetAll(currentPageIndex, 3, sortExpression);
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

		public ActionResult Create()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create([Bind(Include = "Name,Code,Description,IsActive")] CountryViewModel viewModel)
		{
			try
			{
				if (ModelState.IsValid)
				{
					var x = Mapper.Map<Country>(viewModel);
					var results = service.Save(x);
					actionMessges.Add(new UI.ViewModels.ActionOutput()
					{
						MessageTitle = Helpers.AlertMessages.SaveTitle,
						MessageDetails = Helpers.AlertMessages.SaveSuccessfullMessage,
						ActionType = Helpers.AlertActionTypes.Saved,
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
					MessageTitle = Helpers.AlertMessages.SaveErrorTitle,
					MessageDetails = Helpers.AlertMessages.SaveErrorMessage,
					ActionType = Helpers.AlertActionTypes.Saved,
					AlertType = Helpers.AlertTypes.Warning
				});
				actionMessges.Add(new UI.ViewModels.ActionOutput()
				{
					MessageDetails = Helpers.AlertMessages.GeneralErrorTitle,
					ActionType = Helpers.AlertActionTypes.Saved,
					AlertType = Helpers.AlertTypes.Error,
					Exception = ex
				});
				this.AddErrorToViewBag(actionMessges);
			}
			return View(viewModel);
		}

		public async Task<ActionResult> Edit(long? id)
		{
			var viewModel = new CountryViewModel();
			try
			{
				if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

				var x = service.Get(id.Value);

				if (x == null) return HttpNotFound();

				viewModel = Mapper.Map<CountryViewModel>(x);
			}
			catch (Exception ex)
			{
				Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
				logger.Fatal(ex);
				actionMessges.Add(new UI.ViewModels.ActionOutput()
				{
					MessageTitle = Helpers.AlertMessages.LoadErrorTitle,
					MessageDetails = Helpers.AlertMessages.LoadErrorMessage,
					ActionType = Helpers.AlertActionTypes.Read,
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
			viewModel.StatesListing = await states.GetAllByCountryIDAsync(id.Value);

			return View(viewModel);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit([Bind(Include = "ID,Name,Code,Description,IsActive,CreatedBy,DateCreated")] CountryViewModel viewModel)
		{
			try
			{
				if (ModelState.IsValid)
				{
					var x = Mapper.Map<Country>(viewModel);
					service.Save(x);

					actionMessges.Add(new UI.ViewModels.ActionOutput()
					{
						MessageTitle = Helpers.AlertMessages.UpdateTitle,
						MessageDetails = Helpers.AlertMessages.UpdateSuccessfullMessage,
						ActionType = Helpers.AlertActionTypes.Saved,
						AlertType = Helpers.AlertTypes.Success
					});
					TempData["ActionOutput"] = actionMessges;
					//ViewBag.ActionOutput = actionMessges;
					return RedirectToAction("Index");
				}
			}
			catch (Exception ex)
			{
				Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
				logger.Fatal(ex);
				actionMessges.Add(
					new UI.ViewModels.ActionOutput()
					{
						MessageTitle = Helpers.AlertMessages.SaveErrorTitle,
						MessageDetails = Helpers.AlertMessages.SaveErrorMessage,
						ActionType = Helpers.AlertActionTypes.Saved,
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
			}
			viewModel.StatesListing = states.GetAllByCountryID(viewModel.ID);
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