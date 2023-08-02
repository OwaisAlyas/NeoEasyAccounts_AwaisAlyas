using Neo.EasyAccounts.Models.Domain.Locations;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;

namespace Neo.EasyAccounts.Data.Initializers
{
	internal static class LocationsInitializer
	{
		public static void SeedInitialData(DbEntities context)
		{
			var countries = new List<Country> { 
				new Country(){ Name = "Neverland", Code="NL", Description="Neverland (also called Never-Never-Land, Never Land and other variations) is the fictional island and dream world featured in the play Peter Pan" ,DateCreated = DateTime.Now, CreatedBy = "1",  IsDeleted = false, IsActive = true },
				new Country(){ Name = "The Land of Oz", Code="OZ", Description="Oz is, in the first book The Wonderful Wizard of Oz" ,DateCreated = DateTime.Now, CreatedBy = "1",  IsDeleted = false, IsActive = true },
				new Country(){ Name = "Middle-Earth", Code="ME", Description="Middle-earth refers to the fictional lands where most of the stories of author J. R. R. Tolkien take place.The main maps were those published in The Hobbit, The Lord of the Rings and The Silmarillion." ,DateCreated = DateTime.Now, CreatedBy = "1",  IsDeleted = false, IsActive = true },
				new Country(){ Name = "Narnia", Code="NR", Description="Narnia is a fantasy world created by C. S. Lewis as the primary location for his series of seven fantasy novels for children, The Chronicles of Narnia" ,DateCreated = DateTime.Now, CreatedBy = "1",  IsDeleted = false, IsActive = true },
				new Country(){ Name = "UAE", Code="AE", Description="United Arab Emirates" ,DateCreated = DateTime.Now, CreatedBy = "1",  IsDeleted = false, IsActive = true },
				new Country(){ Name = "Pakistan", Code="PK", Description="Islamic Republic of Pakistan" ,DateCreated = DateTime.Now, CreatedBy = "1",  IsDeleted = false, IsActive = true }
			};
			countries.ForEach(d => context.Countries.AddOrUpdate(p => p.Name, d));
			context.SaveChanges();

			var country = context.Countries.FirstOrDefault(d => d.Name.Equals("UAE"));
			var states = new List<State> { 
				new State(){ Country = country, Code="DXB", Name = "Dubai", Description="United Arab Emirates" ,DateCreated = DateTime.Now, CreatedBy = "1",  IsDeleted = false, IsActive = true },
				new State(){ Country = country, Code="AUH", Name = "Abu Dhabi", Description="United Arab Emirates" ,DateCreated = DateTime.Now, CreatedBy = "1",  IsDeleted = false, IsActive = true },
				new State(){ Country = country, Code="003", Name = "Al Ain", Description="United Arab Emirates" ,DateCreated = DateTime.Now, CreatedBy = "1",  IsDeleted = false, IsActive = true },
				new State(){ Country = country, Code="004", Name = "Ras Al Khaimah", Description="United Arab Emirates" ,DateCreated = DateTime.Now, CreatedBy = "1",  IsDeleted = false, IsActive = true },
				new State(){ Country = country, Code="005", Name = "Sharjah", Description="United Arab Emirates" ,DateCreated = DateTime.Now, CreatedBy = "1",  IsDeleted = false, IsActive = true },
				new State(){ Country = country, Code="006", Name = "Ajman", Description="United Arab Emirates" ,DateCreated = DateTime.Now, CreatedBy = "1",  IsDeleted = false, IsActive = true }
			};
			states.ForEach(d => context.States.AddOrUpdate(p => p.Name, d));
			context.SaveChanges();

			var stateDubai = context.States.FirstOrDefault(d => d.Name == "Dubai");
			var stateSharjah = context.States.FirstOrDefault(d => d.Name == "Sharjah");

			var cities = new List<City> { 
				new City(){ State = stateDubai, Code="001", Name = "Dubai", Description="United Arab Emirates" ,DateCreated = DateTime.Now, CreatedBy = "1",  IsDeleted = false, IsActive = true },
				new City(){ State = stateSharjah, Code="002", Name = "Sharjah", Description="United Arab Emirates" ,DateCreated = DateTime.Now, CreatedBy = "1",  IsDeleted = false, IsActive = true }
			};
			cities.ForEach(d => context.Cities.AddOrUpdate(p => p.Name, d));
			context.SaveChanges();

			var cityDubai = context.Cities.FirstOrDefault(d => d.Name.Equals("Dubai"));
			var citySharjah = context.Cities.FirstOrDefault(d => d.Name.Equals("Sharjah"));
			var areas = new List<Area> { 
				new Area(){ City=cityDubai, Code="001", Name = "Area 1", Description="United Arab Emirates" ,DateCreated = DateTime.Now, CreatedBy = "1",  IsDeleted = false, IsActive = true },
				new Area(){ City=cityDubai, Code="002", Name = "Area 2", Description="United Arab Emirates" ,DateCreated = DateTime.Now, CreatedBy = "1",  IsDeleted = false, IsActive = true },
				new Area(){ City=cityDubai, Code="003", Name = "Area 3", Description="United Arab Emirates" ,DateCreated = DateTime.Now, CreatedBy = "1",  IsDeleted = false, IsActive = true },
				new Area(){ City=citySharjah, Code="001", Name = "Area 1", Description="United Arab Emirates" ,DateCreated = DateTime.Now, CreatedBy = "1",  IsDeleted = false, IsActive = true },
				new Area(){ City=citySharjah, Code="002", Name = "Area 2", Description="United Arab Emirates" ,DateCreated = DateTime.Now, CreatedBy = "1",  IsDeleted = false, IsActive = true },
				new Area(){ City=citySharjah, Code="003", Name = "Area 3", Description="United Arab Emirates" ,DateCreated = DateTime.Now, CreatedBy = "1",  IsDeleted = false, IsActive = true }
			};
			areas.ForEach(d => context.Areas.AddOrUpdate(p => p.Name, d));

			context.SaveChanges();
		}
	}
}
