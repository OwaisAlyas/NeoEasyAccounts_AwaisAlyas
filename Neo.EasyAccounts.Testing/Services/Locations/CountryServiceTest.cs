using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Neo.EasyAccounts.Data.Repositories.Locations;
using Neo.EasyAccounts.Service.Locations;
using Neo.EasyAccounts.Models.Domain.Locations;
using Neo.EasyAccounts.Data.Repositories;
using Neo.EasyAccounts.Data;
using System.Linq.Expressions;
using System.Data.Entity;

namespace Neo.EasyAccounts.Testing.Services.Locations
{
	[TestClass]
	public class CountryServiceTest
	{
		private Mock<ICountryRepository> repoConcreteMocked;
		private Mock<IRepository<Country>> repoMocked;
		private Mock<IUnitofWork> unitofWorkMocked;
		private ICountryService service;

		List<Country> countries;

		[TestInitialize()]
		public void MyTestInitialize()
		{
			countries = new List<Country>() {
				new Country(){ ID = 1, Name = "Neverland", Code="NL", Description="Neverland (also called Never-Never-Land, Never Land and other variations) is the fictional island and dream world featured in the play Peter Pan" ,DateCreated = DateTime.Now, CreatedBy = "1", IsDeleted = false, IsActive = true },
				new Country(){ ID = 2, Name = "The Land of Oz", Code="OZ", Description="Oz is, in the first book The Wonderful Wizard of Oz" ,DateCreated = DateTime.Now, CreatedBy = "1", IsDeleted = false, IsActive = true },
				new Country(){ ID = 3, Name = "Middle-Earth", Code="ME", Description="Middle-earth refers to the fictional lands where most of the stories of author J. R. R. Tolkien take place.The main maps were those published in The Hobbit, The Lord of the Rings and The Silmarillion." ,DateCreated = DateTime.Now, CreatedBy = "1", IsDeleted = false, IsActive = true },
				new Country(){ ID = 4, Name = "Narnia", Code="NR", Description="Narnia is a fantasy world created by C. S. Lewis as the primary location for his series of seven fantasy novels for children, The Chronicles of Narnia" ,DateCreated = DateTime.Now, CreatedBy = "1", IsDeleted = false, IsActive = true },
				new Country(){ ID = 5, Name = "UAE", Code="AE", Description="United Arab Emirates" ,DateCreated = DateTime.Now, CreatedBy = "1", IsDeleted = false, IsActive = true },
				new Country(){ ID = 6, Name = "Pakistan", Code="PK", Description="Islamic Republic of Pakistan" ,DateCreated = DateTime.Now, CreatedBy = "1", IsDeleted = false, IsActive = true }
			};

			repoConcreteMocked = new Mock<ICountryRepository>();
			repoMocked = new Mock<IRepository<Country>>();
			unitofWorkMocked = new Mock<IUnitofWork>();

			service = new CountryService(repoConcreteMocked.Object, repoMocked.Object, unitofWorkMocked.Object);

			//var mockSet = new Mock<DbSet<Country>>();
			//mockSet.As<IQueryable<Country>>().Setup(m => m.Provider).Returns(countries.Provider);
			//mockSet.As<IQueryable<Country>>().Setup(m => m.Expression).Returns(countries.Expression);
			//mockSet.As<IQueryable<Country>>().Setup(m => m.ElementType).Returns(countries.ElementType);
			//mockSet.As<IQueryable<Country>>().Setup(m => m.GetEnumerator()).Returns(countries.GetEnumerator());

			//var mockContext = new Mock<DbEntities>();
			//mockContext.Setup(d => d.Countries).Returns(mockSet.Object);


			// Return all the countries
			repoMocked.Setup(d => d.GetAll()).Returns(countries);

			//// return a country by Id
			//repoMocked.Setup(mr => mr.Get(It.IsAny<int>())).Returns((int i) => countries.Where(x => x.ID == i).Single());

			////// return a product by LINQ expression
			////repoMocked.Setup(mr => mr.Get(It.IsAny<System.Linq.Expressions.Expression>())).Returns((System.Linq.Expressions.Expression<Func<Country, bool>> condition) => products.Where(condition).Single());

			//// Allows us to test saving a product

		}

		[TestMethod]
		public void Country_GetAll()
		{
			repoMocked.Setup(d => d.GetAll()).Returns(countries);//Arrange

			List<Country> results = service.GetAll().ToList();//Act

			//Assert
			Assert.IsNotNull(results);
			Assert.AreEqual(countries.Count, results.Count);
		}

		//[TestMethod]
		//public void Country_GetAllByLinqExpression()
		//{
		//	Expression<Func<Country, bool>> condition = d => d.Name.Contains("N");
		//	List<Country> expectedResult = countries.Where(d => d.Name.Contains("n")).ToList();

		//	repoMocked.Setup(d => d.GetAll(condition)).Returns(expectedResult);//Arrange

		//	List<Country> results = service.GetAll(condition) as List<Country>;//Act

		//	//Assert
		//	Assert.IsNotNull(results);
		//	Assert.AreEqual(expectedResult.Count, results.Count);
		//	Assert.AreEqual(expectedResult[0].ID, results[0].ID);
		//	Assert.AreEqual(expectedResult[1].ID, results[1].ID);
		//	Assert.AreEqual(expectedResult[2].ID, results[2].ID);
		//	Assert.AreEqual(expectedResult[3].ID, results[3].ID);
		//}

		//[TestMethod]
		//public void Country_GetByLinqExpression()
		//{
		//	const string countryName = "Neverland";

		//	Expression<Func<Country, bool>> condition = d => d.Name.Equals(countryName);

		//	Country expectedResult = countries.AsQueryable().FirstOrDefault(condition); //countries.FirstOrDefault(d => d.Name.Equals(countryName));

		//	repoMocked.Setup(d => d.Get(condition)).Returns(expectedResult);//Arrange

		//	Country result = service.Get(condition);

		//	Assert.IsNotNull(result);
		//	Assert.AreEqual(expectedResult, result);
		//	Assert.AreEqual(expectedResult.ID, result.ID);
		//	Assert.AreEqual(expectedResult.Code, result.Code);
		//}

		[TestMethod]
		public void Country_GetByID() 
		{
			const long countryID = 1;

			repoMocked.Setup(mr => mr.Get(It.IsAny<int>())).Returns((int i) => countries.Where(x => x.ID == i).Single());
			repoConcreteMocked.Setup(mr => mr.Get(It.IsAny<int>())).Returns((int i) => countries.Where(x => x.ID == i).Single());

			var expectedResult = countries.Find(d => d.ID == countryID);

			var result = service.Get(countryID);

			Assert.IsNotNull(result);
			Assert.AreEqual(expectedResult, result);
			Assert.AreEqual(expectedResult.ID, result.ID);
			Assert.AreEqual(expectedResult.Code, result.Code);
		}

		[TestMethod]
		public void Country_Insert()
		{
			//Arrange			
			repoMocked.Setup(d => d.Insert(It.IsAny<Country>())).Returns(
				(Country target) =>
				{
					if (target.ID.Equals(default(long)))
					{
						target.DateCreated = DateTime.Now;
						target.CreatedBy = "Testing";
						target.ID = countries.Count() + 1;
						countries.Add(target);
					}
					return target;
				});

			var entity = new Country() { ID = 0, Name = "UK", Description = "United Kingdom", Code = "GB", IsActive = false };
			//Act
			var k = service.Save(entity);

			//Assert
			Assert.IsNotNull(k);
			Assert.IsTrue(k.ID > 0);
			unitofWorkMocked.Verify(m => m.Commit(), Times.Once);
		}

		[TestMethod]
		public void Country_Update()
		{
			//Arrange			
			repoMocked.Setup(d => d.Update(It.IsAny<Country>())).Returns(
				(Country target) =>
				{
					if (target.ID > 0)
					{
						var original = countries.FirstOrDefault(d => d.ID == target.ID);

						if (original == null) return null;

						original.Name = target.Name;
						original.Code = target.Code;
						original.Description = target.Description;
						original.DateModified = DateTime.Now;
						original.ModifiedBy = "Testing";
						original.IsActive = target.IsActive;
						original.IsDeleted = target.IsDeleted;
						return original;
					}
					return null;
				});

			var entity = new Country() { ID = 4, Description = "I don't like this ", ModifiedBy = "Admin Tester", IsActive = false };
			//Act
			var k = service.Save(entity);
			
			//Assert
			Assert.IsNotNull(k);
			Assert.AreEqual(k.ID, entity.ID,"IDs are not equal");
			Assert.AreEqual(k.Description, "I don't like this ", false);
			Assert.AreEqual(k.ModifiedBy, "Admin Tester", false);
			Assert.AreEqual(k.IsActive, false, "IsActive Property is not updated");
			
			unitofWorkMocked.Verify(m => m.Commit(), Times.Once);
		}
		//public int Save(IEnumerable<Country> entities)
		//{
		//	throw new NotImplementedException();
		//}

		//public int Delete(long id, bool isPremanent = false)
		//{
		//	throw new NotImplementedException();
		//}

		//public int Delete(Country entity, bool isPremanent = false)
		//{
		//	throw new NotImplementedException();
		//}

		//public int Delete(IEnumerable<Country> entities, bool isPremanent = false)
		//{
		//	throw new NotImplementedException();
		//}

		//public System.Threading.Tasks.Task<Country> GetAsync(string Name)
		//{
		//	throw new NotImplementedException();
		//}

		//public System.Threading.Tasks.Task<IEnumerable<Country>> GetAllAsync(string Name)
		//{
		//	throw new NotImplementedException();
		//}

		//public System.Threading.Tasks.Task<Country> GetAsync(long ID)
		//{
		//	throw new NotImplementedException();
		//}

		//public System.Threading.Tasks.Task<Country> GetAsync(System.Linq.Expressions.Expression<Func<Country, bool>> condition)
		//{
		//	throw new NotImplementedException();
		//}

		//public System.Threading.Tasks.Task<IEnumerable<Country>> GetAllAsync()
		//{
		//	throw new NotImplementedException();
		//}

		//public System.Threading.Tasks.Task<IEnumerable<Country>> GetAllAsync(System.Linq.Expressions.Expression<Func<Country, bool>> condition)
		//{
		//	throw new NotImplementedException();
		//}
	}
}
