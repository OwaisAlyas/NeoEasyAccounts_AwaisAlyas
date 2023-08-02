using AutoMapper;
using Neo.EasyAccounts.Models.Domain;
using Neo.EasyAccounts.Models.Domain.Accounts;
using Neo.EasyAccounts.Models.Domain.Locations;
using Neo.EasyAccounts.Models.Domain.Vouchers;
using Neo.EasyAccounts.Web.UI.Areas.Accounts.ViewModels;
using Neo.EasyAccounts.Web.UI.Areas.Locations.ViewModels;
using Neo.EasyAccounts.Web.UI.Areas.Masters.ViewModels;
using Neo.EasyAccounts.Web.UI.Areas.Vouchers.ViewModels;

namespace Neo.EasyAccounts.Web.UI.Mappings
{
	public class AutoMapperConfiguration
	{
		public static void Configure()
		{
			Mapper.Initialize(x =>
			{
				x.AddProfile<DomainToViewModelMappingProfile>();
				x.AddProfile<ViewModelToDomainMappingProfile>();
			});
		}
	}
}