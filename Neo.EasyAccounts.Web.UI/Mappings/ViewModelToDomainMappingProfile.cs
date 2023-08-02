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
	public class ViewModelToDomainMappingProfile : Profile
	{
		public override string ProfileName
		{
			get { return "ViewModelToDomainMappings"; }
		}

		protected override void Configure()
		{
			CreateMap<CityViewModel, City>();
			CreateMap<CountryViewModel, Country>();
			CreateMap<StateViewModel, State>();
			CreateMap<AreaViewModel, Area>();

			CreateMap<AccountTypeViewModel, AccountType>();
			CreateMap<AccountGroupViewModel, AccountGroup>();
			CreateMap<AccountSubGroupViewModel, AccountSubGroup>();
			CreateMap<AccountTitleViewModel, AccountTitle>();

			CreateMap<JournalVoucherViewModel, JournalVoucher>();
			CreateMap<JournalVoucherDetailViewModel, JournalVoucherDetail>();
			CreateMap<PaymentVoucherViewModel, PaymentVoucher>();
			CreateMap<PaymentVoucherDetailViewModel, PaymentVoucherDetail>();
			CreateMap<ReceiptVoucherViewModel, ReceiptVoucher>();
			CreateMap<ReceiptVoucherDetailViewModel, ReceiptVoucherDetail>();

			CreateMap<AddressViewModel, Address>();
			CreateMap<ContactInfoViewModel, ContactInfo>();
			CreateMap<CompanyViewModel, Company>();
			CreateMap<CustomerViewModel, Customer>();
			CreateMap<EmployeeViewModel, Employee>();
			CreateMap<LineOfBusinessViewModel, LineOfBusiness>();
			CreateMap<SupplierViewModel, Supplier>();
		}
	}
}