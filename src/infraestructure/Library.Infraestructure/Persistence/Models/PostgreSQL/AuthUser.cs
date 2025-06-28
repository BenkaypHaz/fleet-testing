using System;
using System.Collections.Generic;

namespace Library.Infraestructure.Persistence.Models.PostgreSQL;

public partial class AuthUser
{
    public long Id { get; set; }

    public string Dni { get; set; } = null!;

    public string? Gender { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string UserName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string? ProfilePicture { get; set; }

    public string? PhoneNumber { get; set; }

    public DateOnly? BirthDate { get; set; }

    public string Password { get; set; } = null!;

    public bool ResetPassword { get; set; }

    public long CreatedBy { get; set; }

    public DateTime CreatedDate { get; set; }

    public long? ModifiedBy { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public bool IsActive { get; set; }

    public virtual ICollection<AuthAuthorization> AuthAuthorizationCreatedByNavigations { get; set; } = new List<AuthAuthorization>();

    public virtual ICollection<AuthAuthorization> AuthAuthorizationModifiedByNavigations { get; set; } = new List<AuthAuthorization>();

    public virtual ICollection<AuthModule> AuthModuleCreatedByNavigations { get; set; } = new List<AuthModule>();

    public virtual ICollection<AuthModule> AuthModuleModifiedByNavigations { get; set; } = new List<AuthModule>();

    public virtual ICollection<AuthRoleAuthorization> AuthRoleAuthorizationCreatedByNavigations { get; set; } = new List<AuthRoleAuthorization>();

    public virtual ICollection<AuthRoleAuthorization> AuthRoleAuthorizationModifiedByNavigations { get; set; } = new List<AuthRoleAuthorization>();

    public virtual ICollection<AuthRole> AuthRoleCreatedByNavigations { get; set; } = new List<AuthRole>();

    public virtual ICollection<AuthRole> AuthRoleModifiedByNavigations { get; set; } = new List<AuthRole>();

    public virtual ICollection<AuthUserForgotPwdToken> AuthUserForgotPwdTokenCreatedByNavigations { get; set; } = new List<AuthUserForgotPwdToken>();

    public virtual ICollection<AuthUserForgotPwdToken> AuthUserForgotPwdTokenUsers { get; set; } = new List<AuthUserForgotPwdToken>();

    public virtual ICollection<AuthUserRole> AuthUserRoleCreatedByNavigations { get; set; } = new List<AuthUserRole>();

    public virtual ICollection<AuthUserRole> AuthUserRoleModifiedByNavigations { get; set; } = new List<AuthUserRole>();

    public virtual ICollection<AuthUserRole> AuthUserRoleUsers { get; set; } = new List<AuthUserRole>();

    public virtual ICollection<BusinessPartnerProviderDriver> BusinessPartnerProviderDriverCreatedByNavigations { get; set; } = new List<BusinessPartnerProviderDriver>();

    public virtual ICollection<BusinessPartnerProviderDriver> BusinessPartnerProviderDriverModifiedByNavigations { get; set; } = new List<BusinessPartnerProviderDriver>();

    public virtual ICollection<BusinessPartnerProviderProfileContactPerson> BusinessPartnerProviderProfileContactPersonCreatedByNavigations { get; set; } = new List<BusinessPartnerProviderProfileContactPerson>();

    public virtual ICollection<BusinessPartnerProviderProfileContactPerson> BusinessPartnerProviderProfileContactPersonModifiedByNavigations { get; set; } = new List<BusinessPartnerProviderProfileContactPerson>();

    public virtual ICollection<BusinessPartnerProviderProfile> BusinessPartnerProviderProfileCreatedByNavigations { get; set; } = new List<BusinessPartnerProviderProfile>();

    public virtual ICollection<BusinessPartnerProviderProfile> BusinessPartnerProviderProfileModifiedByNavigations { get; set; } = new List<BusinessPartnerProviderProfile>();

    public virtual ICollection<BusinessPartnerProviderTransportVehicle> BusinessPartnerProviderTransportVehicleCreatedByNavigations { get; set; } = new List<BusinessPartnerProviderTransportVehicle>();

    public virtual ICollection<BusinessPartnerProviderTransportVehicle> BusinessPartnerProviderTransportVehicleModifiedByNavigations { get; set; } = new List<BusinessPartnerProviderTransportVehicle>();

    public virtual AuthUser CreatedByNavigation { get; set; } = null!;

    public virtual ICollection<CustomerContactPerson> CustomerContactPersonCreatedByNavigations { get; set; } = new List<CustomerContactPerson>();

    public virtual ICollection<CustomerContactPerson> CustomerContactPersonModifiedByNavigations { get; set; } = new List<CustomerContactPerson>();

    public virtual ICollection<CustomerProfile> CustomerProfileCreatedByNavigations { get; set; } = new List<CustomerProfile>();

    public virtual ICollection<CustomerProfile> CustomerProfileModifiedByNavigations { get; set; } = new List<CustomerProfile>();

    public virtual ICollection<CustomerWarehouse> CustomerWarehouseCreatedByNavigations { get; set; } = new List<CustomerWarehouse>();

    public virtual ICollection<CustomerWarehouse> CustomerWarehouseModifiedByNavigations { get; set; } = new List<CustomerWarehouse>();

    public virtual ICollection<GeneralCity> GeneralCityCreatedByNavigations { get; set; } = new List<GeneralCity>();

    public virtual ICollection<GeneralCity> GeneralCityModifiedByNavigations { get; set; } = new List<GeneralCity>();

    public virtual ICollection<GeneralCountry> GeneralCountryCreatedByNavigations { get; set; } = new List<GeneralCountry>();

    public virtual ICollection<GeneralCountry> GeneralCountryModifiedByNavigations { get; set; } = new List<GeneralCountry>();

    public virtual ICollection<GeneralRegion> GeneralRegionCreatedByNavigations { get; set; } = new List<GeneralRegion>();

    public virtual ICollection<GeneralRegion> GeneralRegionModifiedByNavigations { get; set; } = new List<GeneralRegion>();

    public virtual ICollection<AuthUser> InverseCreatedByNavigation { get; set; } = new List<AuthUser>();

    public virtual ICollection<AuthUser> InverseModifiedByNavigation { get; set; } = new List<AuthUser>();

    public virtual AuthUser? ModifiedByNavigation { get; set; }

    public virtual ICollection<SettingDispatchBranch> SettingDispatchBranchCreatedByNavigations { get; set; } = new List<SettingDispatchBranch>();

    public virtual ICollection<SettingDispatchBranch> SettingDispatchBranchModifiedByNavigations { get; set; } = new List<SettingDispatchBranch>();

    public virtual ICollection<SettingFreightPricingPerCustomer> SettingFreightPricingPerCustomerCreatedByNavigations { get; set; } = new List<SettingFreightPricingPerCustomer>();

    public virtual ICollection<SettingFreightPricingPerCustomer> SettingFreightPricingPerCustomerModifiedByNavigations { get; set; } = new List<SettingFreightPricingPerCustomer>();

    public virtual ICollection<ShipmentExpense> ShipmentExpenseCreatedByNavigations { get; set; } = new List<ShipmentExpense>();

    public virtual ICollection<ShipmentExpense> ShipmentExpenseModifiedByNavigations { get; set; } = new List<ShipmentExpense>();

    public virtual ICollection<ShipmentFreight> ShipmentFreightCreatedByNavigations { get; set; } = new List<ShipmentFreight>();

    public virtual ICollection<ShipmentFreight> ShipmentFreightModifiedByNavigations { get; set; } = new List<ShipmentFreight>();

    public virtual ICollection<ShipmentFreightStatusLog> ShipmentFreightStatusLogCreatedByNavigations { get; set; } = new List<ShipmentFreightStatusLog>();

    public virtual ICollection<ShipmentFreightStatusLog> ShipmentFreightStatusLogModifiedByNavigations { get; set; } = new List<ShipmentFreightStatusLog>();

    public virtual ICollection<ShipmentFuelOrder> ShipmentFuelOrderCreatedByNavigations { get; set; } = new List<ShipmentFuelOrder>();

    public virtual ICollection<ShipmentFuelOrder> ShipmentFuelOrderModifiedByNavigations { get; set; } = new List<ShipmentFuelOrder>();

    public virtual ICollection<ShipmentProjectContract> ShipmentProjectContractCreatedByNavigations { get; set; } = new List<ShipmentProjectContract>();

    public virtual ICollection<ShipmentProjectContract> ShipmentProjectContractModifiedByNavigations { get; set; } = new List<ShipmentProjectContract>();
}
