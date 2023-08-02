using Neo.EasyAccounts.Models.Domain.Locations;
using Neo.EasyAccounts.Service.Locations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace Neo.EasyAccounts.Web.API.Controllers
{
	public class LocationsController : ApiController
	{
		private readonly Neo.Logging.ILogger logger;
		private readonly ICountryService countryService;
		private readonly IStateService stateService;

		public LocationsController()
		{

		}
		public LocationsController(ICountryService countryService, IStateService stateService)
		{
			this.countryService = countryService;
			this.stateService = stateService;
			this.logger = Neo.Logging.LoggerFactory.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
		}

		public async Task<IList<Country>> GetCountries(/*int? page*/)
		{
			var list = new List<Country>().AsEnumerable();
			try
			{
				//int currentPageIndex = page.HasValue ? page.Value - 1 : 0;
				list = await countryService.GetAllAsync();//(null, currentPageIndex, 10, null);
				return list.ToList();
			}
			catch (Exception ex)
			{
				Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
				logger.Fatal(ex);
				return list.ToList();
			}
		}
		[ResponseType(typeof(Country))]
		public async Task<IHttpActionResult> GetCountry(long ID)
		{
			try
			{
				if (ID < 0) return BadRequest();

				var entity = await countryService.GetAsync(ID);

				if (entity == null) return NotFound();

				return Ok(entity);
			}
			catch (Exception ex)
			{
				Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
				logger.Fatal(ex);
				//this.InternalServerError(ex);
				//ModelState.AddModelError("API Error", ex);
				return StatusCode(HttpStatusCode.NoContent);
			}
		}

		[ResponseType(typeof(void))]
		public async Task<IHttpActionResult> SaveCountry(int id, Country model)
		{
			if (!ModelState.IsValid) return BadRequest(ModelState);

			if (id != model.ID) return BadRequest();
			try
			{
				var output = await countryService.SaveAsync(model);
			}
			catch (Exception ex)
			{
				Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
				logger.Fatal(ex);
				return BadRequest();
			}
			return StatusCode(HttpStatusCode.NoContent);
		}

		[ResponseType(typeof(Country))]
		public async Task<IHttpActionResult> DeleteCountry(long ID)
		{
			try
			{
				if (ID < 0) return BadRequest();

				var entity = countryService.Get(ID);

				if (entity == null) return NotFound();

				int outPut = await countryService.DeleteAsync(entity, true);
				return Ok(entity);
			}
			catch (Exception ex)
			{
				Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
				logger.Fatal(ex);
				//this.InternalServerError(ex);
				//ModelState.AddModelError("API Error", ex);
				return StatusCode(HttpStatusCode.NoContent);
			}
		}
	}
}
