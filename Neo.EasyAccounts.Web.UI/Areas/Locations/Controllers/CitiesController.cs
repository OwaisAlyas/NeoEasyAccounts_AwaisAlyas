using AutoMapper;
using ClassifiedAds.Auth;
using Microsoft.AspNet.Identity;
using Neo.EasyAccounts.Models.Domain.Locations;
using Neo.EasyAccounts.Service.Locations;
using Neo.EasyAccounts.Web.UI.Areas.Locations.ViewModels;
using Neo.EasyAccounts.Web.UI.Helpers;
using System;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Neo.EasyAccounts.Web.UI.Areas.Locations.Controllers
{
	[Authorize]
	public class CitiesController : Controller
	{
		private List<UI.ViewModels.ActionOutput> actionMessges;
		private readonly Neo.Logging.ILogger logger;
		private readonly ICityService service;
		private readonly ICountryService countries;
		private readonly IStateService states;
		private readonly IAreaService areas;

		public CitiesController(ICityService service, ICountryService countryService, IStateService stateService, IAreaService areas)
		{
			this.service = service;
			this.states = stateService;
			this.countries = countryService;
			this.areas = areas;
			this.actionMessges = new List<UI.ViewModels.ActionOutput>();
			this.logger = Neo.Logging.LoggerFactory.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
		}

		public async Task<ActionResult> Index()
		{
			var list = new List<City>().AsEnumerable();
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
			var viewModel = new CityViewModel();
			try
			{
				if (id != null && id.HasValue)
				{
					var entity = service.Get(id.Value);

					if (entity == null) return HttpNotFound();

					viewModel = Mapper.Map<CityViewModel>(entity);

					viewModel.StateID = entity.StateID;
					viewModel.CountryID = entity.State.CountryID;

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
			viewModel.AreaListing = (id != null && id.HasValue) ? await areas.GetAllByCityAsync(id.Value) : await areas.GetAllAsync();
			viewModel.Countries = WebUtilHelper.GetCountries(countries);
			viewModel.States = WebUtilHelper.GetStates(states);
			return View(viewModel);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult RegEdit([Bind(Include = "ID,StateID,Name,Code,Description,IsActive,CreatedBy,DateCreated")] CityViewModel viewModel)
		{
			try
			{
				if (ModelState.IsValid)
				{
					bool isUpdate = viewModel.ID > 0;
					var entity = Mapper.Map<City>(viewModel);

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
			viewModel.AreaListing = viewModel.ID > 0 ? areas.GetAllByCity(viewModel.ID) : areas.GetAll();
			viewModel.Countries = WebUtilHelper.GetCountries(countries);
			viewModel.States = WebUtilHelper.GetStates(states);
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
		public string GetStatesByCountryID(long? countryID)
		{
			string json = null;
			if (countryID != null && countryID.HasValue)
			{
				var statesList = states.GetAllByCountryID(countryID.Value);
				var jsonData = statesList.Select(d => new { id = d.ID, text = d.Name });
				json = JsonConvert.SerializeObject(jsonData);
			}
			return json;
		}

		public string GetCountries()
		{
			var statesList = WebUtilHelper.GetCountries(countries);
			var jsonData = statesList.Select(d => new { id = d.Value, value = d.Text });
			var json = JsonConvert.SerializeObject(jsonData);
			return json;
		}
	}
}