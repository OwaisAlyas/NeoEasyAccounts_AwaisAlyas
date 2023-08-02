using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Neo.EasyAccounts.Data;
using Neo.EasyAccounts.Models.Domain.Locations;
using Neo.EasyAccounts.Service.Locations;

namespace Neo.EasyAccounts.Web.API.Controllers
{
	public class CountriesController : ApiController
	{
		private DbEntities db = new DbEntities();

		private readonly Neo.Logging.ILogger logger;
		private readonly ICountryService countryService;
		private readonly IStateService stateService;

		public CountriesController()
		{

		}
		public CountriesController(ICountryService countryService, IStateService stateService)
		{
			this.countryService = countryService;
			this.stateService = stateService;
			this.logger = Neo.Logging.LoggerFactory.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
		}

		// GET: api/Countries
		public IEnumerable<Country> GetCountries()
		{
			var list = countryService.GetAll();
			return list;
		}

		// GET: api/Countries/5
		[ResponseType(typeof(Country))]
		public async Task<IHttpActionResult> GetCountry(long id)
		{
			Country country = await countryService.GetAsync(id);
			if (country == null)
			{
				return NotFound();
			}

			return Ok(country);
		}

		// PUT: api/Countries/5
		[ResponseType(typeof(void))]
		public async Task<IHttpActionResult> PutCountry(long id, Country country)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			if (id != country.ID)
			{
				return BadRequest();
			}

			db.Entry(country).State = EntityState.Modified;

			try
			{
				await db.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!CountryExists(id))
				{
					return NotFound();
				}
				else
				{
					throw;
				}
			}

			return StatusCode(HttpStatusCode.NoContent);
		}

		// POST: api/Countries
		[ResponseType(typeof(Country))]
		public async Task<IHttpActionResult> PostCountry(Country country)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			db.Countries.Add(country);
			await db.SaveChangesAsync();

			return CreatedAtRoute("DefaultApi", new { id = country.ID }, country);
		}

		// DELETE: api/Countries/5
		[ResponseType(typeof(Country))]
		public async Task<IHttpActionResult> DeleteCountry(long id)
		{
			Country country = await db.Countries.FindAsync(id);
			if (country == null)
			{
				return NotFound();
			}

			db.Countries.Remove(country);
			await db.SaveChangesAsync();

			return Ok(country);
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				db.Dispose();
			}
			base.Dispose(disposing);
		}

		private bool CountryExists(long id)
		{
			return db.Countries.Count(e => e.ID == id) > 0;
		}
	}
}