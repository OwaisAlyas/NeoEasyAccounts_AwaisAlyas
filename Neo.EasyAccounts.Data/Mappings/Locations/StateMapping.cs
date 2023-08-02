using Neo.EasyAccounts.Models.Domain.Locations;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neo.EasyAccounts.Data.Mappings.Locations
{
	//Standrd has many students, student = state

	////configure one-to-many
	//modelBuilder.Entity<Standard>()
	//			.HasMany<Student>(s => s.Students) Standard has many Students
	//			.WithRequired(s => s.Standard)  Student require one Standard
	//			.HasForeignKey(s => s.StdId);Student includes specified foreignkey property name for Standard


	//one-to-many 
	//modelBuilder.Entity<Student>()
	//			.HasRequired<Standard>(s => s.Standard)
	//			.WithMany(s => s.Students)
	//			.HasForeignKey(s => s.StdId);
	public class StateMapping : EntityTypeConfiguration<State>, IMapper
	{
		public StateMapping()
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
