using Neo.EasyAccounts.Data.Mappings;
using Neo.EasyAccounts.Data.Mappings.Accounts;
using Neo.EasyAccounts.Data.Mappings.Locations;
using Neo.EasyAccounts.Data.Mappings.Masters;
using Neo.EasyAccounts.Data.Mappings.Vouchers;
using Neo.EasyAccounts.Models.Domain;
using Neo.EasyAccounts.Models.Domain.Accounts;
using Neo.EasyAccounts.Models.Domain.Locations;
using Neo.EasyAccounts.Models.Domain.Vouchers;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neo.EasyAccounts.Data
{
	public class DbEntities : DbContext
	{
		private static readonly Neo.Logging.ILogger logger = Neo.Logging.LoggerFactory.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
		public DbEntities() : base("MSSQLConnection") { }

		public DbSet<Country> Countries { get; set; }
		public DbSet<State> States { get; set; }
		public DbSet<City> Cities { get; set; }
		public DbSet<Area> Areas { get; set; }
		public DbSet<AccountType> AccountTypes { get; set; }
		public DbSet<AccountGroup> AccountGroups { get; set; }
		public DbSet<AccountSubGroup> AccountSubGroups { get; set; }
		public DbSet<AccountTitle> AccountTitles { get; set; }
		public DbSet<Address> Addresses { get; set; }
		public DbSet<Company> Companies { get; set; }
		public DbSet<Customer> Customers { get; set; }
		public DbSet<Employee> Employees { get; set; }
		public DbSet<Supplier> Suppliers { get; set; }
		public DbSet<LineOfBusiness> LineOfBusinesses { get; set; }
		public DbSet<JournalVoucher> JournalVouchers { get; set; }
		public DbSet<JournalVoucherDetail> JournalVoucherDetails { get; set; }
		public DbSet<ReceiptVoucher> ReceiptVouchers { get; set; }
		public DbSet<ReceiptVoucherDetail> ReceiptVoucherDetails { get; set; }
		public DbSet<PaymentVoucher> PaymentVouchers { get; set; }
		public DbSet<PaymentVoucherDetail> PaymentVoucherDetails { get; set; }

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			var typesToMap = (from x in System.Reflection.Assembly.GetExecutingAssembly().GetTypes()
							  where x.IsClass && typeof(IMapper).IsAssignableFrom(x)
							  select x).ToList();

			foreach (var mappingType in typesToMap)
			{
				dynamic mapperClass = Activator.CreateInstance(mappingType);
				modelBuilder.Configurations.Add(mapperClass);
			}
		}

		public override int SaveChanges()
		{
			try
			{
				ChangeTracker.DetectChanges();
				var modifiedEntries = ChangeTracker.Entries().Where(x => x.Entity is IAuditableEntity && (x.State == EntityState.Added || x.State == EntityState.Modified));

				foreach (var entry in modifiedEntries)
				{
					if (((IAuditableEntity)entry.Entity) != null)
					{
						var identityName = System.Threading.Thread.CurrentPrincipal.Identity.Name;
						var now = DateTime.UtcNow;

						if (entry.State == EntityState.Added)
						{
							((IAuditableEntity)entry.Entity).CreatedBy = identityName;
							((IAuditableEntity)entry.Entity).DateCreated = now;
						}
						else
						{
							((IAuditableEntity)entry.Entity).ModifiedBy = identityName;
							((IAuditableEntity)entry.Entity).DateModified = now;
							Entry(((IAuditableEntity)entry.Entity)).Property(x => x.CreatedBy).IsModified = false;
							Entry(((IAuditableEntity)entry.Entity)).Property(x => x.DateCreated).IsModified = false;
						}
					}
				}
				return base.SaveChanges();
			}
			catch (System.Data.Entity.Validation.DbEntityValidationException ex)
			{
				// Retrieve the error messages as a list of strings.
				var errorMessages = ex.EntityValidationErrors.SelectMany(x => x.ValidationErrors).Select(x => x.ErrorMessage);

				// Join the list to a single string.
				var fullErrorMessage = string.Join("; ", errorMessages);

				// Combine the original exception message with the new one.
				var exceptionMessage = string.Concat(ex.Message, "Validation errors are: ", fullErrorMessage);

				var newValidationException = new System.Data.Entity.Validation.DbEntityValidationException(exceptionMessage, ex.EntityValidationErrors);

				throw newValidationException;
			}
			catch (Exception)
			{
				throw;
			}
		}
		public override Task<int> SaveChangesAsync()
		{
			return base.SaveChangesAsync();
		}

		public virtual void Commit()
		{
			base.SaveChanges();
		}
		public virtual void CommitAsync()
		{
			base.SaveChangesAsync();
		}
	}
}
