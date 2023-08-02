using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Neo.EasyAccounts.Data;
using Neo.EasyAccounts.Data.Repositories;
using Neo.EasyAccounts.Data.Repositories.Locations;
using Neo.EasyAccounts.Models.Domain.Locations;
using Neo.EasyAccounts.Service.Locations;
using Neo.EasyAccounts.Web.UI.Areas.Locations.Controllers;
using Neo.EasyAccounts.Web.UI.Areas.Locations.ViewModels;
using Neo.EasyAccounts.Web.UI.Mappings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Neo.EasyAccounts.Testing.Controllers.Locations
{
	[TestClass]
	public class CountryControllerTest
	{
		private Mock<ICountryService> countryServiceMock;
		private Mock<IStateService> stateServiceMock;
		private CountriesController controller;
		private List<Country> countries;

		[TestInitialize]
		public void Initialize()
		{
			countryServiceMock = new Mock<ICountryService>();
			stateServiceMock = new Mock<IStateService>();
			controller = new CountriesController(countryServiceMock.Object, stateServiceMock.Object);
			countries = new List<Country>() {
				new Country(){ ID = 1, Name = "Neverland", Code="NL", Description="Neverland (also called Never-Never-Land, Never Land and other variations) is the fictional island and dream world featured in the play Peter Pan" ,DateCreated = DateTime.Now, CreatedBy = "1", IsDeleted = false, IsActive = true },
				new Country(){ ID = 2, Name = "The Land of Oz", Code="OZ", Description="Oz is, in the first book The Wonderful Wizard of Oz" ,DateCreated = DateTime.Now, CreatedBy = "1", IsDeleted = false, IsActive = true },
				new Country(){ ID = 3, Name = "Middle-Earth", Code="ME", Description="Middle-earth refers to the fictional lands where most of the stories of author J. R. R. Tolkien take place.The main maps were those published in The Hobbit, The Lord of the Rings and The Silmarillion." ,DateCreated = DateTime.Now, CreatedBy = "1", IsDeleted = false, IsActive = true },
				new Country(){ ID = 4, Name = "Narnia", Code="NR", Description="Narnia is a fantasy world created by C. S. Lewis as the primary location for his series of seven fantasy novels for children, The Chronicles of Narnia" ,DateCreated = DateTime.Now, CreatedBy = "1", IsDeleted = false, IsActive = true },
				new Country(){ ID = 5, Name = "UAE", Code="AE", Description="United Arab Emirates" ,DateCreated = DateTime.Now, CreatedBy = "1", IsDeleted = false, IsActive = true },
				new Country(){ ID = 6, Name = "Pakistan", Code="PK", Description="Islamic Republic of Pakistan" ,DateCreated = DateTime.Now, CreatedBy = "1", IsDeleted = false, IsActive = true }
			};
		}

		//[TestMethod]
		//public void Country_Index()
		//{
		//	//Arrange
		//	countryServiceMock.Setup(repo => repo.GetAllAsync()).Returns(Task.FromResult(countries.AsEnumerable()));
		//	//Act
		//	var index = controller.Index(0) as Task<ActionResult>;

		//	// Get the model from the ViewResult and cast it to the correct type 
		//	// (notice in the first line I changed ActionResult to ViewResult to make sure we can access the model.
		//	var model = (List<Country>)(((ViewResult)index.Result).Model);

		//	//Assert
		//	Assert.IsNotNull(model);
		//	Assert.AreEqual(model.Count, 6);
		//	Assert.AreEqual("Neverland", model[0].Name);
		//	Assert.AreEqual("Narnia", model[3].Name);
		//	Assert.AreEqual("Pakistan", model[5].Name);
		//}

		[TestMethod]
		public void Valid_Country_Create()
		{
			//Arrange
			var viewModel = new CountryViewModel() { ID = 1, Name = "Neverland", Code = "NL", Description = "Neverland (also called Never-Never-Land, Never Land and other variations) is the fictional island and dream world featured in the play Peter Pan", DateCreated = DateTime.Now, CreatedBy = "1", IsDeleted = false, IsActive = true };
			var model = new Country()
			{
				ID = viewModel.ID,
				Name = viewModel.Name,
				Code = viewModel.Code,
				Description = viewModel.Description,
				DateCreated = viewModel.DateCreated,
				CreatedBy = viewModel.CreatedBy,
				IsDeleted = viewModel.IsDeleted,
				IsActive = viewModel.IsActive
			};

			AutoMapperConfiguration.Configure();

			//Act
			var result = (RedirectToRouteResult)controller.Create(viewModel);

			//Assert 
			countryServiceMock.Verify(repo => repo.Save(model), Times.Once);
			Assert.AreEqual("Index", result.RouteValues["action"]);
		}
		[TestMethod]
		public void Invalid_Country_Create()
		{
			// Arrange
			var viewModel = new CountryViewModel() { Name = "" };
			var model = new Country() { Name = "" };
			controller.ModelState.AddModelError("Error", "Something went wrong");

			//Act
			var result = (ViewResult)controller.Create(viewModel);

			//Assert
			countryServiceMock.Verify(m => m.Save(model), Times.Never);
			Assert.AreEqual("", result.ViewName);
		}
	}
}
