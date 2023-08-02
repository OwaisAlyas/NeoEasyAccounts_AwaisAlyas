using Neo.EasyAccounts.Models.Domain.Locations;
using System.Data.Entity.ModelConfiguration;

namespace Neo.EasyAccounts.Data.Mappings.Locations
{
	public class CountryMapping : EntityTypeConfiguration<Country>, IMapper
	{
		public CountryMapping()
		{
			Property(d => d.Name).IsRequired().HasMaxLength(250);
			Property(d => d.Code).IsRequired().HasMaxLength(250);
			Property(d => d.Description).HasMaxLength(500);

			Property(d => d.CreatedBy).IsRequired().HasMaxLength(250);
			Property(d => d.DateCreated).IsRequired();
			Property(d => d.IsDeleted).IsRequired();
			Property(d => d.IsActive).IsRequired();
			Property(d => d.ModifiedBy).HasMaxLength(250);

			//ToTable("Countries");

			//HasMany(d => d.States)
			//	.WithOptional(d => d.Country)
			//	.HasForeignKey(d => d.CountryID);
		}
	}
}
