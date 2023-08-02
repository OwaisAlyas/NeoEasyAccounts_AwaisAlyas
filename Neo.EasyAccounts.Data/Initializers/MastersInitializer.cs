using Neo.EasyAccounts.Models.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;

namespace Neo.EasyAccounts.Data.Initializers
{
	internal static class MastersInitializer
	{
		public static void SeedInitialData(DbEntities context)
		{
			var companies = new List<Company> { 
				new Company(){ Name="Jones Company", Code="C001", OwnerName="Jones", DateCreated = DateTime.Now, CreatedBy = "1", IsDeleted = false, IsActive = true },
				new Company(){ Name="Oliver Company", Code="C001", OwnerName="Oliver Queen", DateCreated = DateTime.Now, CreatedBy = "1", IsDeleted = false, IsActive = true },
				new Company(){ Name="Wayne Enterprises", Code="C001", OwnerName="Bruce Wayne", DateCreated = DateTime.Now, CreatedBy = "1", IsDeleted = false, IsActive = true },
				new Company(){ Name="Fish Enterprises", Code="C001", OwnerName="Fish", DateCreated = DateTime.Now, CreatedBy = "1", IsDeleted = false, IsActive = true }
			};
			companies.ForEach(d => context.Companies.AddOrUpdate(p => p.ID, d));
			context.SaveChanges();

			var employee = new List<Employee> { 
				new Employee(){ Name="Oliver Queen", Code="E001", Department="Dept 1", DateCreated = DateTime.Now, CreatedBy = "1", IsDeleted = false, IsActive = true },
				new Employee(){ Name="Thea Queen", Code="E002", Department="Dept B", DateCreated = DateTime.Now, CreatedBy = "1", IsDeleted = false, IsActive = true },
				new Employee(){ Name="Moira Queen", Code="E003", Department="Dept F", DateCreated = DateTime.Now, CreatedBy = "1", IsDeleted = false, IsActive = true }
			};
			employee.ForEach(d => context.Employees.AddOrUpdate(p => p.ID, d));
			context.SaveChanges();

			var customers = new List<Customer> { 
				new Customer(){ Name="Thomas Merlyn", Code="C001", Type="Jones", DateCreated = DateTime.Now, CreatedBy = "1", IsDeleted = false, IsActive = true },
				new Customer(){ Name="John Diggle", Code="C002", Type="Oliver Queen", DateCreated = DateTime.Now, CreatedBy = "1", IsDeleted = false, IsActive = true },
				new Customer(){ Name="Felicity Megan Smoak", Code="C003", Type="Bruce Wayne", DateCreated = DateTime.Now, CreatedBy = "1", IsDeleted = false, IsActive = true },
				new Customer(){ Name="Roy Harper", Code="C004", Type="Fish", DateCreated = DateTime.Now, CreatedBy = "1", IsDeleted = false, IsActive = true },
				new Customer(){ Name="Malcolm Merlyn", Code="C005", Type="Fish", DateCreated = DateTime.Now, CreatedBy = "1", IsDeleted = false, IsActive = true }
			};
			customers.ForEach(d => context.Customers.AddOrUpdate(p => p.ID, d));
			context.SaveChanges();

			var suppliers = new List<Supplier> { 
				new Supplier(){ Name="Queen Consolidated", Code="C001", Type="Jones", DateCreated = DateTime.Now, CreatedBy = "1", IsDeleted = false, IsActive = true },
				new Supplier(){ Name="Palmer Industries", Code="C001", Type="Oliver Queen", DateCreated = DateTime.Now, CreatedBy = "1", IsDeleted = false, IsActive = true },
				new Supplier(){ Name="Wayne Enterprises", Code="C001", Type="Bruce Wayne", DateCreated = DateTime.Now, CreatedBy = "1", IsDeleted = false, IsActive = true },
				new Supplier(){ Name="Fish Enterprises", Code="C001", Type="Fish", DateCreated = DateTime.Now, CreatedBy = "1", IsDeleted = false, IsActive = true }
			};
			suppliers.ForEach(d => context.Suppliers.AddOrUpdate(p => p.ID, d));
			context.SaveChanges();

			var lineOfBusinesses = new List<LineOfBusiness> { 
				new LineOfBusiness(){ Name="Tech Mega", Code="L001", ManagerName="Jones", DateCreated = DateTime.Now, CreatedBy = "1", IsDeleted = false, IsActive = true },
				new LineOfBusiness(){ Name="Accounting Firm", Code="L002", ManagerName="Oliver Queen", DateCreated = DateTime.Now, CreatedBy = "1", IsDeleted = false, IsActive = true },
				new LineOfBusiness(){ Name="AI BackBone", Code="L003", ManagerName="Bruce Wayne", DateCreated = DateTime.Now, CreatedBy = "1", IsDeleted = false, IsActive = true },
				new LineOfBusiness(){ Name="HR Cloud", Code="L004", ManagerName="Fish", DateCreated = DateTime.Now, CreatedBy = "1", IsDeleted = false, IsActive = true }
			};
			lineOfBusinesses.ForEach(d => context.LineOfBusinesses.AddOrUpdate(p => p.ID, d));
			context.SaveChanges();
		}
	}
}
