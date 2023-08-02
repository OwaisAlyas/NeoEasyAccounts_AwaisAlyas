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
	public class DomainToViewModelMappingProfile : Profile
	{
		public override string ProfileName
		{
			get { return "DomainToViewModelMappings"; }
		}

		protected override void Configure()
		{
			CreateMap<Country, CountryViewModel>();
			CreateMap<State, StateViewModel>();
			CreateMap<City, CityViewModel>();
			CreateMap<Area, AreaViewModel>();

			CreateMap<AccountType, AccountTypeViewModel>();
			CreateMap<AccountGroup, AccountGroupViewModel>();
			CreateMap<AccountSubGroup, AccountSubGroupViewModel>();
			CreateMap<AccountTitle, AccountTitleViewModel>();

			CreateMap<JournalVoucher, JournalVoucherViewModel>();
			CreateMap<JournalVoucherDetail, JournalVoucherDetailViewModel>();
			CreateMap<PaymentVoucher, PaymentVoucherViewModel>();
			CreateMap<PaymentVoucherDetail, PaymentVoucherDetailViewModel>();
			CreateMap<ReceiptVoucher, ReceiptVoucherViewModel>();
			CreateMap<ReceiptVoucherDetail, ReceiptVoucherDetailViewModel>();

			CreateMap<Address, AddressViewModel>();
			CreateMap<ContactInfo, ContactInfoViewModel>();
			CreateMap<Company, CompanyViewModel>();
			CreateMap<Customer, CustomerViewModel>();
			CreateMap<Employee, EmployeeViewModel>();
			CreateMap<LineOfBusiness, LineOfBusinessViewModel>();
			CreateMap<Supplier, SupplierViewModel>();
		}
	}	
}