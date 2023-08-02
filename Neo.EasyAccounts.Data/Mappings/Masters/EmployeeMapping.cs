using Neo.EasyAccounts.Models.Domain;
using System.Data.Entity.ModelConfiguration;


namespace Neo.EasyAccounts.Data.Mappings.Masters
{
	public class EmployeeMapping : EntityTypeConfiguration<Employee>, IMapper
	{
		public EmployeeMapping()
		{
			Property(d => d.Name).IsRequired().HasMaxLength(250);
			Property(d => d.Code).IsRequired().HasMaxLength(250);
			Property(d => d.Department).IsRequired().HasMaxLength(250);
			Property(d => d.Description).HasMaxLength(500);
			
			Property(d => d.CreatedBy).IsRequired().HasMaxLength(250);
			Property(d => d.DateCreated).IsRequired();
			Property(d => d.IsDeleted).IsRequired();
			Property(d => d.IsActive).IsRequired();
			Property(d => d.ModifiedBy).HasMaxLength(250);

			//ToTable("Companies");

			//HasRequired<Model.Location.City>(d => d.City)
			//	.WithMany(d => d.Areas)
			//	.HasForeignKey(d => d.CityID);

			//HasMany(d => d.Spots)
			//	.WithOptional(d=> d.Area)
			//	.HasForeignKey(d => d.AreaID);
		}
	}
}
