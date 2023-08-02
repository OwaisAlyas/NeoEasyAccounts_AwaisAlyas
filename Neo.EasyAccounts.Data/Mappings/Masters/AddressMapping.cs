using Neo.EasyAccounts.Models.Domain;
using System.Data.Entity.ModelConfiguration;


namespace Neo.EasyAccounts.Data.Mappings.Masters
{
	public class AddressMapping : EntityTypeConfiguration<Address>, IMapper
	{
		public AddressMapping()
		{
			Property(d => d.Building).HasMaxLength(250);
			Property(d => d.FlatNo).HasMaxLength(250);
			Property(d => d.Floor).HasMaxLength(250);
			Property(d => d.HouseNo).HasMaxLength(250);
			Property(d => d.OfficeNo).HasMaxLength(250);
			Property(d => d.POBoxNo).HasMaxLength(250);
			Property(d => d.Street).HasMaxLength(250);

			Property(d => d.CreatedBy).IsRequired().HasMaxLength(250);
			Property(d => d.DateCreated).IsRequired();
			Property(d => d.IsDeleted).IsRequired();
			Property(d => d.IsActive).IsRequired();
			Property(d => d.ModifiedBy).HasMaxLength(250);
		}
	}
}
