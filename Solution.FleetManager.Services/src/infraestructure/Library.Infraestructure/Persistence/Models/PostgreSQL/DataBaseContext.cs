using System;
using System.Collections.Generic;
using Library.Infraestructure.Common.Helpers;
using Microsoft.EntityFrameworkCore;

namespace Library.Infraestructure.Persistence.Models.PostgreSQL;

public partial class DataBaseContext : DbContext
{
    public DataBaseContext()
    {
    }

    public DataBaseContext(DbContextOptions<DataBaseContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AuthAuthorization> AuthAuthorizations { get; set; }

    public virtual DbSet<AuthModule> AuthModules { get; set; }

    public virtual DbSet<AuthRole> AuthRoles { get; set; }

    public virtual DbSet<AuthRoleAuthorization> AuthRoleAuthorizations { get; set; }

    public virtual DbSet<AuthUser> AuthUsers { get; set; }

    public virtual DbSet<AuthUserForgotPwdToken> AuthUserForgotPwdTokens { get; set; }

    public virtual DbSet<AuthUserRole> AuthUserRoles { get; set; }

    public virtual DbSet<BusinessPositionType> BusinessPositionTypes { get; set; }

    public virtual DbSet<BusinessProviderDriver> BusinessProviderDrivers { get; set; }

    public virtual DbSet<BusinessProviderProfile> BusinessProviderProfiles { get; set; }

    public virtual DbSet<BusinessProviderProfileContactPerson> BusinessProviderProfileContactPeople { get; set; }

    public virtual DbSet<BusinessProviderProfileType> BusinessProviderProfileTypes { get; set; }

    public virtual DbSet<BusinessProviderTransportVehicle> BusinessProviderTransportVehicles { get; set; }

    public virtual DbSet<BusinessTransportVehicleStatusType> BusinessTransportVehicleStatusTypes { get; set; }

    public virtual DbSet<CustomerContactPerson> CustomerContactPeople { get; set; }

    public virtual DbSet<CustomerProfile> CustomerProfiles { get; set; }

    public virtual DbSet<CustomerWarehouse> CustomerWarehouses { get; set; }

    public virtual DbSet<GeneralCity> GeneralCities { get; set; }

    public virtual DbSet<GeneralCountry> GeneralCountries { get; set; }

    public virtual DbSet<GeneralErrorLog> GeneralErrorLogs { get; set; }

    public virtual DbSet<GeneralRegion> GeneralRegions { get; set; }

    public virtual DbSet<SettingDispatchBranch> SettingDispatchBranches { get; set; }

    public virtual DbSet<SettingFreightPricingPerCustomer> SettingFreightPricingPerCustomers { get; set; }

    public virtual DbSet<ShipmentExpense> ShipmentExpenses { get; set; }

    public virtual DbSet<ShipmentExpenseType> ShipmentExpenseTypes { get; set; }

    public virtual DbSet<ShipmentFreight> ShipmentFreights { get; set; }

    public virtual DbSet<ShipmentFreightStatus> ShipmentFreightStatuses { get; set; }

    public virtual DbSet<ShipmentFreightStatusLog> ShipmentFreightStatusLogs { get; set; }

    public virtual DbSet<ShipmentFreightType> ShipmentFreightTypes { get; set; }

    public virtual DbSet<ShipmentFuelOrder> ShipmentFuelOrders { get; set; }

    public virtual DbSet<ShipmentFuelOrderType> ShipmentFuelOrderTypes { get; set; }

    public virtual DbSet<ShipmentGasStation> ShipmentGasStations { get; set; }

    public virtual DbSet<ShipmentProjectContract> ShipmentProjectContracts { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseNpgsql(BaseHelper.GetConnectionString());

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AuthAuthorization>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("pk_auth_authorization_id");

            entity.ToTable("auth_authorization", "auth");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.CreatedDate).HasColumnName("created_date");
            entity.Property(e => e.Description)
                .HasMaxLength(300)
                .HasColumnName("description");
            entity.Property(e => e.IsActive).HasColumnName("is_active");
            entity.Property(e => e.ModifiedBy).HasColumnName("modified_by");
            entity.Property(e => e.ModifiedDate).HasColumnName("modified_date");
            entity.Property(e => e.ModuleId).HasColumnName("module_id");
            entity.Property(e => e.RouteValue)
                .HasMaxLength(300)
                .HasColumnName("route_value");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.AuthAuthorizationCreatedByNavigations)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_auth_authorization_created_by");

            entity.HasOne(d => d.ModifiedByNavigation).WithMany(p => p.AuthAuthorizationModifiedByNavigations)
                .HasForeignKey(d => d.ModifiedBy)
                .HasConstraintName("fk_auth_authorization_modified_by");

            entity.HasOne(d => d.Module).WithMany(p => p.AuthAuthorizations)
                .HasForeignKey(d => d.ModuleId)
                .HasConstraintName("fk_auth_authorization_module_id");
        });

        modelBuilder.Entity<AuthModule>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("pk_auth_module_id");

            entity.ToTable("auth_module", "auth");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.CreatedDate).HasColumnName("created_date");
            entity.Property(e => e.IsActive).HasColumnName("is_active");
            entity.Property(e => e.ModifiedBy).HasColumnName("modified_by");
            entity.Property(e => e.ModifiedDate).HasColumnName("modified_date");
            entity.Property(e => e.Name)
                .HasMaxLength(60)
                .HasColumnName("name");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.AuthModuleCreatedByNavigations)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_auth_module_created_by");

            entity.HasOne(d => d.ModifiedByNavigation).WithMany(p => p.AuthModuleModifiedByNavigations)
                .HasForeignKey(d => d.ModifiedBy)
                .HasConstraintName("fk_auth_module_modified_by");
        });

        modelBuilder.Entity<AuthRole>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("pk_auth_role_id");

            entity.ToTable("auth_role", "auth");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.CreatedDate).HasColumnName("created_date");
            entity.Property(e => e.Description)
                .HasMaxLength(200)
                .HasColumnName("description");
            entity.Property(e => e.IsActive).HasColumnName("is_active");
            entity.Property(e => e.ModifiedBy).HasColumnName("modified_by");
            entity.Property(e => e.ModifiedDate).HasColumnName("modified_date");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.AuthRoleCreatedByNavigations)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_auth_role_created_by");

            entity.HasOne(d => d.ModifiedByNavigation).WithMany(p => p.AuthRoleModifiedByNavigations)
                .HasForeignKey(d => d.ModifiedBy)
                .HasConstraintName("fk_auth_role_modified_by");
        });

        modelBuilder.Entity<AuthRoleAuthorization>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("pk_auth_role_authorization_id");

            entity.ToTable("auth_role_authorization", "auth");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AuthId).HasColumnName("auth_id");
            entity.Property(e => e.Create).HasColumnName("create");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.CreatedDate).HasColumnName("created_date");
            entity.Property(e => e.Delete).HasColumnName("delete");
            entity.Property(e => e.IsActive).HasColumnName("is_active");
            entity.Property(e => e.ModifiedBy).HasColumnName("modified_by");
            entity.Property(e => e.ModifiedDate).HasColumnName("modified_date");
            entity.Property(e => e.Read).HasColumnName("read");
            entity.Property(e => e.RoleId).HasColumnName("role_id");
            entity.Property(e => e.Update).HasColumnName("update");

            entity.HasOne(d => d.Auth).WithMany(p => p.AuthRoleAuthorizations)
                .HasForeignKey(d => d.AuthId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_auth_role_authorization_auth_id");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.AuthRoleAuthorizationCreatedByNavigations)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_auth_role_authorization_created_by");

            entity.HasOne(d => d.ModifiedByNavigation).WithMany(p => p.AuthRoleAuthorizationModifiedByNavigations)
                .HasForeignKey(d => d.ModifiedBy)
                .HasConstraintName("fk_auth_role_authorization_modified_by");

            entity.HasOne(d => d.Role).WithMany(p => p.AuthRoleAuthorizations)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_auth_role_authorization_role_id");
        });

        modelBuilder.Entity<AuthUser>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("pk_auth_user_id");

            entity.ToTable("auth_user", "auth");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.BirthDate).HasColumnName("birth_date");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.CreatedDate).HasColumnName("created_date");
            entity.Property(e => e.Email)
                .HasMaxLength(80)
                .HasColumnName("email");
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .HasColumnName("first_name");
            entity.Property(e => e.IsActive).HasColumnName("is_active");
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .HasColumnName("last_name");
            entity.Property(e => e.ModifiedBy).HasColumnName("modified_by");
            entity.Property(e => e.ModifiedDate).HasColumnName("modified_date");
            entity.Property(e => e.Password).HasColumnName("password");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(15)
                .HasColumnName("phone_number");
            entity.Property(e => e.ProfilePicture).HasColumnName("profile_picture");
            entity.Property(e => e.ResetPassword).HasColumnName("reset_password");
            entity.Property(e => e.UserName)
                .HasMaxLength(30)
                .HasColumnName("user_name");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.InverseCreatedByNavigation)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_auth_user_created_by");

            entity.HasOne(d => d.ModifiedByNavigation).WithMany(p => p.InverseModifiedByNavigation)
                .HasForeignKey(d => d.ModifiedBy)
                .HasConstraintName("fk_auth_user_modified_by");
        });

        modelBuilder.Entity<AuthUserForgotPwdToken>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("pk_auth_user_forgot_pwd_token_id");

            entity.ToTable("auth_user_forgot_pwd_token", "auth");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.CreatedDate).HasColumnName("created_date");
            entity.Property(e => e.ExpirationDate).HasColumnName("expiration_date");
            entity.Property(e => e.Token).HasColumnName("token");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.AuthUserForgotPwdTokenCreatedByNavigations)
                .HasForeignKey(d => d.CreatedBy)
                .HasConstraintName("fk_auth_user_forgot_pwd_token_created_by");

            entity.HasOne(d => d.User).WithMany(p => p.AuthUserForgotPwdTokenUsers)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_auth_user_forgot_pwd_token_user_id");
        });

        modelBuilder.Entity<AuthUserRole>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("pk_auth_user_role_id");

            entity.ToTable("auth_user_role", "auth");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.CreatedDate).HasColumnName("created_date");
            entity.Property(e => e.IsActive).HasColumnName("is_active");
            entity.Property(e => e.ModifiedBy).HasColumnName("modified_by");
            entity.Property(e => e.ModifiedDate).HasColumnName("modified_date");
            entity.Property(e => e.RoleId).HasColumnName("role_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.AuthUserRoleCreatedByNavigations)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_auth_user_role_created_by");

            entity.HasOne(d => d.ModifiedByNavigation).WithMany(p => p.AuthUserRoleModifiedByNavigations)
                .HasForeignKey(d => d.ModifiedBy)
                .HasConstraintName("fk_auth_user_role_modified_by");

            entity.HasOne(d => d.Role).WithMany(p => p.AuthUserRoles)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_auth_user_role_role_id");

            entity.HasOne(d => d.User).WithMany(p => p.AuthUserRoleUsers)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_auth_user_role_user_id");
        });

        modelBuilder.Entity<BusinessPositionType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("pk_business_position_type_id");

            entity.ToTable("business_position_type", "business");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.IsActive).HasColumnName("is_active");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
        });

        modelBuilder.Entity<BusinessProviderDriver>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("pk_business_provider_driver_id");

            entity.ToTable("business_provider_driver", "business");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.BusinessProviderProfileId).HasColumnName("business_provider_profile_id");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.CreatedDate).HasColumnName("created_date");
            entity.Property(e => e.DateOfBirth).HasColumnName("date_of_birth");
            entity.Property(e => e.Email)
                .HasMaxLength(80)
                .HasColumnName("email");
            entity.Property(e => e.EndDate).HasColumnName("end_date");
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .HasColumnName("first_name");
            entity.Property(e => e.HireDate).HasColumnName("hire_date");
            entity.Property(e => e.IsActive).HasColumnName("is_active");
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .HasColumnName("last_name");
            entity.Property(e => e.ModifiedBy).HasColumnName("modified_by");
            entity.Property(e => e.ModifiedDate).HasColumnName("modified_date");
            entity.Property(e => e.NationalId)
                .HasMaxLength(20)
                .HasColumnName("national_id");
            entity.Property(e => e.Nationality)
                .HasMaxLength(50)
                .HasColumnName("nationality");
            entity.Property(e => e.Phone)
                .HasMaxLength(20)
                .HasColumnName("phone");

            entity.HasOne(d => d.BusinessProviderProfile).WithMany(p => p.BusinessProviderDrivers)
                .HasForeignKey(d => d.BusinessProviderProfileId)
                .HasConstraintName("fk_business_provider_driver_profile_business_provider_profile");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.BusinessProviderDriverCreatedByNavigations)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_business_provider_driver_created_by");

            entity.HasOne(d => d.ModifiedByNavigation).WithMany(p => p.BusinessProviderDriverModifiedByNavigations)
                .HasForeignKey(d => d.ModifiedBy)
                .HasConstraintName("fk_business_provider_driver_modified_by");
        });

        modelBuilder.Entity<BusinessProviderProfile>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("pk_business_provider_profile_id");

            entity.ToTable("business_provider_profile", "business");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Address).HasColumnName("address");
            entity.Property(e => e.BusinessName)
                .HasMaxLength(100)
                .HasColumnName("business_name");
            entity.Property(e => e.BusinessProviderProfileTypeId).HasColumnName("business_provider_profile_type_id");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.CreatedDate).HasColumnName("created_date");
            entity.Property(e => e.Email)
                .HasMaxLength(80)
                .HasColumnName("email");
            entity.Property(e => e.GeneralCityId).HasColumnName("general_city_id");
            entity.Property(e => e.IsActive).HasColumnName("is_active");
            entity.Property(e => e.LegalId)
                .HasMaxLength(20)
                .HasColumnName("legal_id");
            entity.Property(e => e.LegalName)
                .HasMaxLength(100)
                .HasColumnName("legal_name");
            entity.Property(e => e.ModifiedBy).HasColumnName("modified_by");
            entity.Property(e => e.ModifiedDate).HasColumnName("modified_date");
            entity.Property(e => e.Phone)
                .HasMaxLength(20)
                .HasColumnName("phone");

            entity.HasOne(d => d.BusinessProviderProfileType).WithMany(p => p.BusinessProviderProfiles)
                .HasForeignKey(d => d.BusinessProviderProfileTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_business_provider_profile_type");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.BusinessProviderProfileCreatedByNavigations)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_business_provider_profile_created_by");

            entity.HasOne(d => d.GeneralCity).WithMany(p => p.BusinessProviderProfiles)
                .HasForeignKey(d => d.GeneralCityId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_business_provider_profile_general_city_id");

            entity.HasOne(d => d.ModifiedByNavigation).WithMany(p => p.BusinessProviderProfileModifiedByNavigations)
                .HasForeignKey(d => d.ModifiedBy)
                .HasConstraintName("fk_business_provider_profile_modified_by");
        });

        modelBuilder.Entity<BusinessProviderProfileContactPerson>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("pk_provider_profile_contact_person_id");

            entity.ToTable("business_provider_profile_contact_person", "business");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.BusinessPositionTypeId).HasColumnName("business_position_type_id");
            entity.Property(e => e.BusinessProviderProfileId).HasColumnName("business_provider_profile_id");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.CreatedDate).HasColumnName("created_date");
            entity.Property(e => e.Email)
                .HasMaxLength(80)
                .HasColumnName("email");
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .HasColumnName("first_name");
            entity.Property(e => e.IsActive).HasColumnName("is_active");
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .HasColumnName("last_name");
            entity.Property(e => e.ModifiedBy).HasColumnName("modified_by");
            entity.Property(e => e.ModifiedDate).HasColumnName("modified_date");
            entity.Property(e => e.NationalId)
                .HasMaxLength(20)
                .HasColumnName("national_id");
            entity.Property(e => e.Phone)
                .HasMaxLength(20)
                .HasColumnName("phone");

            entity.HasOne(d => d.BusinessPositionType).WithMany(p => p.BusinessProviderProfileContactPeople)
                .HasForeignKey(d => d.BusinessPositionTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_provider_profile_contact_person_business_position_type_id");

            entity.HasOne(d => d.BusinessProviderProfile).WithMany(p => p.BusinessProviderProfileContactPeople)
                .HasForeignKey(d => d.BusinessProviderProfileId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_provider_profile_contact_person");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.BusinessProviderProfileContactPersonCreatedByNavigations)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_provider_profile_contact_person_created_by");

            entity.HasOne(d => d.ModifiedByNavigation).WithMany(p => p.BusinessProviderProfileContactPersonModifiedByNavigations)
                .HasForeignKey(d => d.ModifiedBy)
                .HasConstraintName("fk_provider_profile_contact_person_modified_by");
        });

        modelBuilder.Entity<BusinessProviderProfileType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("pk_business_provider_profile_type_id");

            entity.ToTable("business_provider_profile_type", "business");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.IsActive).HasColumnName("is_active");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
        });

        modelBuilder.Entity<BusinessProviderTransportVehicle>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("pk_business_provider_transport_vehicle_id");

            entity.ToTable("business_provider_transport_vehicle", "business");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Axles).HasColumnName("axles");
            entity.Property(e => e.Brand)
                .HasMaxLength(50)
                .HasColumnName("brand");
            entity.Property(e => e.BusinessProviderDriverId).HasColumnName("business_provider_driver_id");
            entity.Property(e => e.BusinessProviderProfileId).HasColumnName("business_provider_profile_id");
            entity.Property(e => e.Color)
                .HasMaxLength(30)
                .HasColumnName("color");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.CreatedDate).HasColumnName("created_date");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.IsActive).HasColumnName("is_active");
            entity.Property(e => e.Model)
                .HasMaxLength(50)
                .HasColumnName("model");
            entity.Property(e => e.ModifiedBy).HasColumnName("modified_by");
            entity.Property(e => e.ModifiedDate).HasColumnName("modified_date");
            entity.Property(e => e.PlateNumber)
                .HasMaxLength(20)
                .HasColumnName("plate_number");
            entity.Property(e => e.Vin)
                .HasMaxLength(50)
                .HasColumnName("vin");
            entity.Property(e => e.Year).HasColumnName("year");

            entity.HasOne(d => d.BusinessProviderDriver).WithMany(p => p.BusinessProviderTransportVehicles)
                .HasForeignKey(d => d.BusinessProviderDriverId)
                .HasConstraintName("fk_business_provider_vehicle_driver_business_provider_driver");

            entity.HasOne(d => d.BusinessProviderProfile).WithMany(p => p.BusinessProviderTransportVehicles)
                .HasForeignKey(d => d.BusinessProviderProfileId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_business_provider_vehicle_profile_business_provider_profile");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.BusinessProviderTransportVehicleCreatedByNavigations)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_business_provider_vehicle_created_by");

            entity.HasOne(d => d.ModifiedByNavigation).WithMany(p => p.BusinessProviderTransportVehicleModifiedByNavigations)
                .HasForeignKey(d => d.ModifiedBy)
                .HasConstraintName("fk_business_provider_vehicle_modified_by");
        });

        modelBuilder.Entity<BusinessTransportVehicleStatusType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("pk_business_transport_vehicle_status_type_id");

            entity.ToTable("business_transport_vehicle_status_type", "business");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.IsActive).HasColumnName("is_active");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
        });

        modelBuilder.Entity<CustomerContactPerson>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("pk_customer_contact_person_id");

            entity.ToTable("customer_contact_person", "customer");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.CreatedDate).HasColumnName("created_date");
            entity.Property(e => e.CustomerProfileId).HasColumnName("customer_profile_id");
            entity.Property(e => e.Email)
                .HasMaxLength(80)
                .HasColumnName("email");
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .HasColumnName("first_name");
            entity.Property(e => e.IsActive).HasColumnName("is_active");
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .HasColumnName("last_name");
            entity.Property(e => e.ModifiedBy).HasColumnName("modified_by");
            entity.Property(e => e.ModifiedDate).HasColumnName("modified_date");
            entity.Property(e => e.NationalId)
                .HasMaxLength(20)
                .HasColumnName("national_id");
            entity.Property(e => e.Phone)
                .HasMaxLength(20)
                .HasColumnName("phone");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.CustomerContactPersonCreatedByNavigations)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_customer_contact_person_created_by");

            entity.HasOne(d => d.CustomerProfile).WithMany(p => p.CustomerContactPeople)
                .HasForeignKey(d => d.CustomerProfileId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_customer_contact_person_profile_customer_profile");

            entity.HasOne(d => d.ModifiedByNavigation).WithMany(p => p.CustomerContactPersonModifiedByNavigations)
                .HasForeignKey(d => d.ModifiedBy)
                .HasConstraintName("fk_customer_contact_person_modified_by");
        });

        modelBuilder.Entity<CustomerProfile>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("pk_customer_profile_id");

            entity.ToTable("customer_profile", "customer");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Address).HasColumnName("address");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.CreatedDate).HasColumnName("created_date");
            entity.Property(e => e.Email)
                .HasMaxLength(80)
                .HasColumnName("email");
            entity.Property(e => e.GeneralCityId).HasColumnName("general_city_id");
            entity.Property(e => e.IsActive).HasColumnName("is_active");
            entity.Property(e => e.LegalId)
                .HasMaxLength(20)
                .HasColumnName("legal_id");
            entity.Property(e => e.LegalName)
                .HasMaxLength(100)
                .HasColumnName("legal_name");
            entity.Property(e => e.ModifiedBy).HasColumnName("modified_by");
            entity.Property(e => e.ModifiedDate).HasColumnName("modified_date");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
            entity.Property(e => e.Phone)
                .HasMaxLength(20)
                .HasColumnName("phone");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.CustomerProfileCreatedByNavigations)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_customer_profile_created_by");

            entity.HasOne(d => d.GeneralCity).WithMany(p => p.CustomerProfiles)
                .HasForeignKey(d => d.GeneralCityId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_customer_profile_general_city_id");

            entity.HasOne(d => d.ModifiedByNavigation).WithMany(p => p.CustomerProfileModifiedByNavigations)
                .HasForeignKey(d => d.ModifiedBy)
                .HasConstraintName("fk_customer_profile_modified_by");
        });

        modelBuilder.Entity<CustomerWarehouse>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("pk_customer_warehouse_id");

            entity.ToTable("customer_warehouse", "customer");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Address).HasColumnName("address");
            entity.Property(e => e.Code).HasColumnName("code");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.CreatedDate).HasColumnName("created_date");
            entity.Property(e => e.CustomerProfileId).HasColumnName("customer_profile_id");
            entity.Property(e => e.GeneralCityId).HasColumnName("general_city_id");
            entity.Property(e => e.IsActive).HasColumnName("is_active");
            entity.Property(e => e.Latitud)
                .HasPrecision(11, 8)
                .HasColumnName("latitud");
            entity.Property(e => e.Longitud)
                .HasPrecision(11, 8)
                .HasColumnName("longitud");
            entity.Property(e => e.ModifiedBy).HasColumnName("modified_by");
            entity.Property(e => e.ModifiedDate).HasColumnName("modified_date");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.CustomerWarehouseCreatedByNavigations)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_customer_warehouse_created_by");

            entity.HasOne(d => d.CustomerProfile).WithMany(p => p.CustomerWarehouses)
                .HasForeignKey(d => d.CustomerProfileId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_customer_warehouse_profile_customer_profile_id");

            entity.HasOne(d => d.GeneralCity).WithMany(p => p.CustomerWarehouses)
                .HasForeignKey(d => d.GeneralCityId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_customer_warehouse_general_city_id");

            entity.HasOne(d => d.ModifiedByNavigation).WithMany(p => p.CustomerWarehouseModifiedByNavigations)
                .HasForeignKey(d => d.ModifiedBy)
                .HasConstraintName("fk_customer_warehouse_modified_by");
        });

        modelBuilder.Entity<GeneralCity>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("pk_general_city_id");

            entity.ToTable("general_city", "general");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.CreatedDate).HasColumnName("created_date");
            entity.Property(e => e.IsActive).HasColumnName("is_active");
            entity.Property(e => e.ModifiedBy).HasColumnName("modified_by");
            entity.Property(e => e.ModifiedDate).HasColumnName("modified_date");
            entity.Property(e => e.Name)
                .HasMaxLength(150)
                .HasColumnName("name");
            entity.Property(e => e.RegionId).HasColumnName("region_id");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.GeneralCityCreatedByNavigations)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_general_city_created_by");

            entity.HasOne(d => d.ModifiedByNavigation).WithMany(p => p.GeneralCityModifiedByNavigations)
                .HasForeignKey(d => d.ModifiedBy)
                .HasConstraintName("fk_general_city_modified_by");

            entity.HasOne(d => d.Region).WithMany(p => p.GeneralCities)
                .HasForeignKey(d => d.RegionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_general_city_region_id");
        });

        modelBuilder.Entity<GeneralCountry>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("pk_general_country_id");

            entity.ToTable("general_country", "general");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Code)
                .HasMaxLength(10)
                .HasColumnName("code");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.CreatedDate).HasColumnName("created_date");
            entity.Property(e => e.IsActive).HasColumnName("is_active");
            entity.Property(e => e.IsoCode)
                .HasMaxLength(10)
                .HasColumnName("iso_code");
            entity.Property(e => e.ModifiedBy).HasColumnName("modified_by");
            entity.Property(e => e.ModifiedDate).HasColumnName("modified_date");
            entity.Property(e => e.Name)
                .HasMaxLength(150)
                .HasColumnName("name");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.GeneralCountryCreatedByNavigations)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_general_country_created_by");

            entity.HasOne(d => d.ModifiedByNavigation).WithMany(p => p.GeneralCountryModifiedByNavigations)
                .HasForeignKey(d => d.ModifiedBy)
                .HasConstraintName("fk_general_country_modified_by");
        });

        modelBuilder.Entity<GeneralErrorLog>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("pk_general_error_log_id");

            entity.ToTable("general_error_log", "general");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Class)
                .HasMaxLength(255)
                .HasColumnName("class");
            entity.Property(e => e.CreatedDate).HasColumnName("created_date");
            entity.Property(e => e.ErrorDescription).HasColumnName("error_description");
            entity.Property(e => e.LineNumber).HasColumnName("line_number");
            entity.Property(e => e.Method)
                .HasMaxLength(255)
                .HasColumnName("method");
            entity.Property(e => e.Project)
                .HasMaxLength(255)
                .HasColumnName("project");
        });

        modelBuilder.Entity<GeneralRegion>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("pk_general_region_id");

            entity.ToTable("general_region", "general");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CountryId).HasColumnName("country_id");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.CreatedDate).HasColumnName("created_date");
            entity.Property(e => e.IsActive).HasColumnName("is_active");
            entity.Property(e => e.ModifiedBy).HasColumnName("modified_by");
            entity.Property(e => e.ModifiedDate).HasColumnName("modified_date");
            entity.Property(e => e.Name)
                .HasMaxLength(150)
                .HasColumnName("name");

            entity.HasOne(d => d.Country).WithMany(p => p.GeneralRegions)
                .HasForeignKey(d => d.CountryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_general_region_country_id");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.GeneralRegionCreatedByNavigations)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_general_region_created_by");

            entity.HasOne(d => d.ModifiedByNavigation).WithMany(p => p.GeneralRegionModifiedByNavigations)
                .HasForeignKey(d => d.ModifiedBy)
                .HasConstraintName("fk_general_region_modified_by");
        });

        modelBuilder.Entity<SettingDispatchBranch>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("pk_setting_dispatch_branch_id");

            entity.ToTable("setting_dispatch_branch", "setting");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Address).HasColumnName("address");
            entity.Property(e => e.Code).HasColumnName("code");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.CreatedDate).HasColumnName("created_date");
            entity.Property(e => e.GeneralCityId).HasColumnName("general_city_id");
            entity.Property(e => e.IsActive).HasColumnName("is_active");
            entity.Property(e => e.Latitud)
                .HasPrecision(11, 8)
                .HasColumnName("latitud");
            entity.Property(e => e.Longitud)
                .HasPrecision(11, 8)
                .HasColumnName("longitud");
            entity.Property(e => e.ModifiedBy).HasColumnName("modified_by");
            entity.Property(e => e.ModifiedDate).HasColumnName("modified_date");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.SettingDispatchBranchCreatedByNavigations)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_setting_dispatch_branch_created_by_user");

            entity.HasOne(d => d.GeneralCity).WithMany(p => p.SettingDispatchBranches)
                .HasForeignKey(d => d.GeneralCityId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_setting_dispatch_branch_general_city_id");

            entity.HasOne(d => d.ModifiedByNavigation).WithMany(p => p.SettingDispatchBranchModifiedByNavigations)
                .HasForeignKey(d => d.ModifiedBy)
                .HasConstraintName("fk_setting_dispatch_branch_modified_by_user");
        });

        modelBuilder.Entity<SettingFreightPricingPerCustomer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("pk_setting_freight_pricing_per_customer_id");

            entity.ToTable("setting_freight_pricing_per_customer", "setting");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Cost)
                .HasPrecision(12, 2)
                .HasColumnName("cost");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.CreatedDate).HasColumnName("created_date");
            entity.Property(e => e.CustomerWarehouseId).HasColumnName("customer_warehouse_id");
            entity.Property(e => e.IsActive).HasColumnName("is_active");
            entity.Property(e => e.ModifiedBy).HasColumnName("modified_by");
            entity.Property(e => e.ModifiedDate).HasColumnName("modified_date");
            entity.Property(e => e.Price)
                .HasPrecision(12, 2)
                .HasColumnName("price");
            entity.Property(e => e.SettingDispatchBranchId).HasColumnName("setting_dispatch_branch_id");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.SettingFreightPricingPerCustomerCreatedByNavigations)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_setting_freight_created_by_user");

            entity.HasOne(d => d.CustomerWarehouse).WithMany(p => p.SettingFreightPricingPerCustomers)
                .HasForeignKey(d => d.CustomerWarehouseId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_setting_freight_customer_warehouse");

            entity.HasOne(d => d.ModifiedByNavigation).WithMany(p => p.SettingFreightPricingPerCustomerModifiedByNavigations)
                .HasForeignKey(d => d.ModifiedBy)
                .HasConstraintName("fk_setting_freight_modified_by_user");

            entity.HasOne(d => d.SettingDispatchBranch).WithMany(p => p.SettingFreightPricingPerCustomers)
                .HasForeignKey(d => d.SettingDispatchBranchId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_setting_freight_dispatch_branch");
        });

        modelBuilder.Entity<ShipmentExpense>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("pk_shipment_expense_id");

            entity.ToTable("shipment_expense", "shipment");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Amount)
                .HasPrecision(12, 2)
                .HasColumnName("amount");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.CreatedDate).HasColumnName("created_date");
            entity.Property(e => e.Currency)
                .HasMaxLength(10)
                .HasColumnName("currency");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.ExpenseDate).HasColumnName("expense_date");
            entity.Property(e => e.IsActive).HasColumnName("is_active");
            entity.Property(e => e.ModifiedBy).HasColumnName("modified_by");
            entity.Property(e => e.ModifiedDate).HasColumnName("modified_date");
            entity.Property(e => e.ShipmentExpenseTypeId).HasColumnName("shipment_expense_type_id");
            entity.Property(e => e.ShipmentFreightId).HasColumnName("shipment_freight_id");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.ShipmentExpenseCreatedByNavigations)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_shipment_expense_created_by");

            entity.HasOne(d => d.ModifiedByNavigation).WithMany(p => p.ShipmentExpenseModifiedByNavigations)
                .HasForeignKey(d => d.ModifiedBy)
                .HasConstraintName("fk_shipment_expense_modified_by");

            entity.HasOne(d => d.ShipmentExpenseType).WithMany(p => p.ShipmentExpenses)
                .HasForeignKey(d => d.ShipmentExpenseTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_shipment_expense_shipment_expense_type_id");

            entity.HasOne(d => d.ShipmentFreight).WithMany(p => p.ShipmentExpenses)
                .HasForeignKey(d => d.ShipmentFreightId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_shipment_expense_shipment_freight_id");
        });

        modelBuilder.Entity<ShipmentExpenseType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("pk_shipment_expense_type_id");

            entity.ToTable("shipment_expense_type", "shipment");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.IsActive).HasColumnName("is_active");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
        });

        modelBuilder.Entity<ShipmentFreight>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("pk_freight_id");

            entity.ToTable("shipment_freight", "shipment");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Cost)
                .HasPrecision(12, 2)
                .HasColumnName("cost");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.CreatedDate).HasColumnName("created_date");
            entity.Property(e => e.CustomerWarehouseId).HasColumnName("customer_warehouse_id");
            entity.Property(e => e.IsActive).HasColumnName("is_active");
            entity.Property(e => e.ModifiedBy).HasColumnName("modified_by");
            entity.Property(e => e.ModifiedDate).HasColumnName("modified_date");
            entity.Property(e => e.Observations).HasColumnName("observations");
            entity.Property(e => e.Price)
                .HasPrecision(12, 2)
                .HasColumnName("price");
            entity.Property(e => e.ProviderDriverId).HasColumnName("provider_driver_id");
            entity.Property(e => e.ProviderTransportVehicleId).HasColumnName("provider_transport_vehicle_id");
            entity.Property(e => e.ShipmentFreightStatusId).HasColumnName("shipment_freight_status_id");
            entity.Property(e => e.ShipmentFreightTypeId).HasColumnName("shipment_freight_type_id");
            entity.Property(e => e.ShipmentProjectContractId).HasColumnName("shipment_project_contract_id");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.ShipmentFreightCreatedByNavigations)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_shipment_freight_created_by");

            entity.HasOne(d => d.CustomerWarehouse).WithMany(p => p.ShipmentFreights)
                .HasForeignKey(d => d.CustomerWarehouseId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_shipment_freight_customer_warehouse");

            entity.HasOne(d => d.ModifiedByNavigation).WithMany(p => p.ShipmentFreightModifiedByNavigations)
                .HasForeignKey(d => d.ModifiedBy)
                .HasConstraintName("fk_shipment_freight_modified_by");

            entity.HasOne(d => d.ProviderDriver).WithMany(p => p.ShipmentFreights)
                .HasForeignKey(d => d.ProviderDriverId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_shipment_freight_business_provider_driver");

            entity.HasOne(d => d.ProviderTransportVehicle).WithMany(p => p.ShipmentFreights)
                .HasForeignKey(d => d.ProviderTransportVehicleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_shipment_freight_provider_transport_vehicle");

            entity.HasOne(d => d.ShipmentFreightStatus).WithMany(p => p.ShipmentFreights)
                .HasForeignKey(d => d.ShipmentFreightStatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_shipment_freight_status");

            entity.HasOne(d => d.ShipmentFreightType).WithMany(p => p.ShipmentFreights)
                .HasForeignKey(d => d.ShipmentFreightTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_shipment_freight_type");

            entity.HasOne(d => d.ShipmentProjectContract).WithMany(p => p.ShipmentFreights)
                .HasForeignKey(d => d.ShipmentProjectContractId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_shipment_freight_project_contract");
        });

        modelBuilder.Entity<ShipmentFreightStatus>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("pk_shipment_freight_status_id");

            entity.ToTable("shipment_freight_status", "shipment");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.IsActive).HasColumnName("is_active");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
        });

        modelBuilder.Entity<ShipmentFreightStatusLog>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("pk_shipment_freight_status_log_id");

            entity.ToTable("shipment_freight_status_log", "shipment");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Comments).HasColumnName("comments");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.CreatedDate).HasColumnName("created_date");
            entity.Property(e => e.IsActive).HasColumnName("is_active");
            entity.Property(e => e.ModifiedBy).HasColumnName("modified_by");
            entity.Property(e => e.ModifiedDate).HasColumnName("modified_date");
            entity.Property(e => e.ShipmentFreightId).HasColumnName("shipment_freight_id");
            entity.Property(e => e.ShipmentFreightStatusId).HasColumnName("shipment_freight_status_id");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.ShipmentFreightStatusLogCreatedByNavigations)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_shipment_freight_status_log_created_by");

            entity.HasOne(d => d.ModifiedByNavigation).WithMany(p => p.ShipmentFreightStatusLogModifiedByNavigations)
                .HasForeignKey(d => d.ModifiedBy)
                .HasConstraintName("fk_shipment_freight_status_log_modified_by");

            entity.HasOne(d => d.ShipmentFreight).WithMany(p => p.ShipmentFreightStatusLogs)
                .HasForeignKey(d => d.ShipmentFreightId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_shipment_freight_status_log_freight");

            entity.HasOne(d => d.ShipmentFreightStatus).WithMany(p => p.ShipmentFreightStatusLogs)
                .HasForeignKey(d => d.ShipmentFreightStatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_shipment_freight_status_log_status");
        });

        modelBuilder.Entity<ShipmentFreightType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("pk_shipment_freight_type_id");

            entity.ToTable("shipment_freight_type", "shipment");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.IsActive).HasColumnName("is_active");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
        });

        modelBuilder.Entity<ShipmentFuelOrder>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("pk_shipment_fuel_order_id");

            entity.ToTable("shipment_fuel_order", "shipment");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CostPerGallon)
                .HasPrecision(12, 2)
                .HasColumnName("cost_per_gallon");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.CreatedDate).HasColumnName("created_date");
            entity.Property(e => e.Currency)
                .HasMaxLength(10)
                .HasColumnName("currency");
            entity.Property(e => e.FuelOrderIssuerId).HasColumnName("fuel_order_issuer_id");
            entity.Property(e => e.IsActive).HasColumnName("is_active");
            entity.Property(e => e.IssuedDate).HasColumnName("issued_date");
            entity.Property(e => e.ModifiedBy).HasColumnName("modified_by");
            entity.Property(e => e.ModifiedDate).HasColumnName("modified_date");
            entity.Property(e => e.ProviderDriverId).HasColumnName("provider_driver_id");
            entity.Property(e => e.ProviderTransportVehicleId).HasColumnName("provider_transport_vehicle_id");
            entity.Property(e => e.QuantityGallon)
                .HasPrecision(12, 2)
                .HasColumnName("quantity_gallon");
            entity.Property(e => e.ShipmentFreightId).HasColumnName("shipment_freight_id");
            entity.Property(e => e.ShipmentFuelOrderTypeId).HasColumnName("shipment_fuel_order_type_id");
            entity.Property(e => e.ShipmentGasStationId).HasColumnName("shipment_gas_station_id");
            entity.Property(e => e.TotalCost)
                .HasPrecision(12, 2)
                .HasColumnName("total_cost");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.ShipmentFuelOrderCreatedByNavigations)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_shipment_fuel_order_created_by");

            entity.HasOne(d => d.FuelOrderIssuer).WithMany(p => p.ShipmentFuelOrders)
                .HasForeignKey(d => d.FuelOrderIssuerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_shipment_fuel_order_fuel_order_issuer_id");

            entity.HasOne(d => d.ModifiedByNavigation).WithMany(p => p.ShipmentFuelOrderModifiedByNavigations)
                .HasForeignKey(d => d.ModifiedBy)
                .HasConstraintName("fk_shipment_fuel_order_modified_by");

            entity.HasOne(d => d.ProviderDriver).WithMany(p => p.ShipmentFuelOrders)
                .HasForeignKey(d => d.ProviderDriverId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_shipment_fuel_order_provider_driver_id");

            entity.HasOne(d => d.ProviderTransportVehicle).WithMany(p => p.ShipmentFuelOrders)
                .HasForeignKey(d => d.ProviderTransportVehicleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_shipment_fuel_order_provider_transport_vehicle_id");

            entity.HasOne(d => d.ShipmentFreight).WithMany(p => p.ShipmentFuelOrders)
                .HasForeignKey(d => d.ShipmentFreightId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_shipment_fuel_order_shipment_freight_id");

            entity.HasOne(d => d.ShipmentFuelOrderType).WithMany(p => p.ShipmentFuelOrders)
                .HasForeignKey(d => d.ShipmentFuelOrderTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_shipment_fuel_order_shipment_fuel_order_type_id");

            entity.HasOne(d => d.ShipmentGasStation).WithMany(p => p.ShipmentFuelOrders)
                .HasForeignKey(d => d.ShipmentGasStationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_shipment_fuel_order_shipment_gas_station_id");
        });

        modelBuilder.Entity<ShipmentFuelOrderType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("pk_shipment_fuel_order_type_id");

            entity.ToTable("shipment_fuel_order_type", "shipment");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.IsActive).HasColumnName("is_active");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
        });

        modelBuilder.Entity<ShipmentGasStation>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("pk_shipment_gas_station_id");

            entity.ToTable("shipment_gas_station", "shipment");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Address).HasColumnName("address");
            entity.Property(e => e.GeneralCityId).HasColumnName("general_city_id");
            entity.Property(e => e.IsActive).HasColumnName("is_active");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");

            entity.HasOne(d => d.GeneralCity).WithMany(p => p.ShipmentGasStations)
                .HasForeignKey(d => d.GeneralCityId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_shipment_gas_station_general_city_id");
        });

        modelBuilder.Entity<ShipmentProjectContract>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("pk_shipment_project_contract_id");

            entity.ToTable("shipment_project_contract", "shipment");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ContractNumber).HasColumnName("contract_number");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.CreatedDate).HasColumnName("created_date");
            entity.Property(e => e.CustomerProfileId).HasColumnName("customer_profile_id");
            entity.Property(e => e.EndDate).HasColumnName("end_date");
            entity.Property(e => e.ExpectedFreight).HasColumnName("expected_freight");
            entity.Property(e => e.IsActive).HasColumnName("is_active");
            entity.Property(e => e.ModifiedBy).HasColumnName("modified_by");
            entity.Property(e => e.ModifiedDate).HasColumnName("modified_date");
            entity.Property(e => e.SettingDispatchBranchId).HasColumnName("setting_dispatch_branch_id");
            entity.Property(e => e.StartDate).HasColumnName("start_date");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.ShipmentProjectContractCreatedByNavigations)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_shipment_contract_created_by");

            entity.HasOne(d => d.CustomerProfile).WithMany(p => p.ShipmentProjectContracts)
                .HasForeignKey(d => d.CustomerProfileId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_shipment_contract_customer_profile");

            entity.HasOne(d => d.ModifiedByNavigation).WithMany(p => p.ShipmentProjectContractModifiedByNavigations)
                .HasForeignKey(d => d.ModifiedBy)
                .HasConstraintName("fk_shipment_contract_modified_by");

            entity.HasOne(d => d.SettingDispatchBranch).WithMany(p => p.ShipmentProjectContracts)
                .HasForeignKey(d => d.SettingDispatchBranchId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_shipment_contract_setting_dispatch_branch");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
