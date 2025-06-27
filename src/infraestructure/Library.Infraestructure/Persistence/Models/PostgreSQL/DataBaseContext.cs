using Library.Infraestructure.Common.Helpers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

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

    public virtual DbSet<BusinessPartnerPositionType> BusinessPartnerPositionTypes { get; set; }

    public virtual DbSet<BusinessPartnerProviderDriver> BusinessPartnerProviderDrivers { get; set; }

    public virtual DbSet<BusinessPartnerProviderProfile> BusinessPartnerProviderProfiles { get; set; }

    public virtual DbSet<BusinessPartnerProviderProfileCategory> BusinessPartnerProviderProfileCategories { get; set; }

    public virtual DbSet<BusinessPartnerProviderProfileContactPerson> BusinessPartnerProviderProfileContactPeople { get; set; }

    public virtual DbSet<BusinessPartnerProviderProfileType> BusinessPartnerProviderProfileTypes { get; set; }

    public virtual DbSet<BusinessPartnerTransportVehicleStatusType> BusinessPartnerTransportVehicleStatusTypes { get; set; }

    public virtual DbSet<BusinessPartnerproviderTransportVehicle> BusinessPartnerproviderTransportVehicles { get; set; }

    public virtual DbSet<CustomerContactPerson> CustomerContactPeople { get; set; }

    public virtual DbSet<CustomerProfile> CustomerProfiles { get; set; }

    public virtual DbSet<CustomerWarehouse> CustomerWarehouses { get; set; }

    public virtual DbSet<GeneralCity> GeneralCities { get; set; }

    public virtual DbSet<GeneralCountry> GeneralCountries { get; set; }

    public virtual DbSet<GeneralErrorLog> GeneralErrorLogs { get; set; }

    public virtual DbSet<GeneralRegion> GeneralRegions { get; set; }

    public virtual DbSet<SettingDispatchBranch> SettingDispatchBranches { get; set; }

    public virtual DbSet<SettingFreightPricingPerCustomer> SettingFreightPricingPerCustomers { get; set; }

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
            entity.Property(e => e.Dni)
                .HasMaxLength(15)
                .HasColumnName("dni");
            entity.Property(e => e.Email)
                .HasMaxLength(80)
                .HasColumnName("email");
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .HasColumnName("first_name");
            entity.Property(e => e.Gender)
                .HasMaxLength(1)
                .HasColumnName("gender");
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
            entity.Property(e => e.IsActive).HasColumnName("is_active");
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

        modelBuilder.Entity<BusinessPartnerPositionType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("pk_business_partner_position_type_id");

            entity.ToTable("business_partner_position_type", "business_partner");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.IsActive).HasColumnName("is_active");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
        });

        modelBuilder.Entity<BusinessPartnerProviderDriver>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("pk_business_partner_provider_driver_id");

            entity.ToTable("business_partner_provider_driver", "business_partner");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.BusinessPartnerProviderProfileId).HasColumnName("business_partner_provider_profile_id");
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

            entity.HasOne(d => d.BusinessPartnerProviderProfile).WithMany(p => p.BusinessPartnerProviderDrivers)
                .HasForeignKey(d => d.BusinessPartnerProviderProfileId)
                .HasConstraintName("fk_business_partner_provider_driver_profile_business_provider_p");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.BusinessPartnerProviderDriverCreatedByNavigations)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_business_partner_provider_driver_created_by");

            entity.HasOne(d => d.ModifiedByNavigation).WithMany(p => p.BusinessPartnerProviderDriverModifiedByNavigations)
                .HasForeignKey(d => d.ModifiedBy)
                .HasConstraintName("fk_business_partner_provider_driver_modified_by");
        });

        modelBuilder.Entity<BusinessPartnerProviderProfile>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("pk_business_partner_provider_profile_id");

            entity.ToTable("business_partner_provider_profile", "business_partner");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Address).HasColumnName("address");
            entity.Property(e => e.BusinessName)
                .HasMaxLength(100)
                .HasColumnName("business_name");
            entity.Property(e => e.BusinessPartnerProviderProfileCategoryId).HasColumnName("business_partner_provider_profile_category_id");
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

            entity.HasOne(d => d.BusinessPartnerProviderProfileCategory).WithMany(p => p.BusinessPartnerProviderProfiles)
                .HasForeignKey(d => d.BusinessPartnerProviderProfileCategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_business_partner_provider_profile_category");

            entity.HasOne(d => d.BusinessProviderProfileType).WithMany(p => p.BusinessPartnerProviderProfiles)
                .HasForeignKey(d => d.BusinessProviderProfileTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_business_partner_provider_profile_type");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.BusinessPartnerProviderProfileCreatedByNavigations)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_business_partner_provider_profile_created_by");

            entity.HasOne(d => d.GeneralCity).WithMany(p => p.BusinessPartnerProviderProfiles)
                .HasForeignKey(d => d.GeneralCityId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_business_partner_provider_profile_general_city_id");

            entity.HasOne(d => d.ModifiedByNavigation).WithMany(p => p.BusinessPartnerProviderProfileModifiedByNavigations)
                .HasForeignKey(d => d.ModifiedBy)
                .HasConstraintName("fk_business_partner_provider_profile_modified_by");
        });

        modelBuilder.Entity<BusinessPartnerProviderProfileCategory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("pk_business_partner_provider_profile_category_id");

            entity.ToTable("business_partner_provider_profile_category", "business_partner");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.IsActive).HasColumnName("is_active");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
        });

        modelBuilder.Entity<BusinessPartnerProviderProfileContactPerson>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("pk_business_partner_provider_profile_contact_person_id");

            entity.ToTable("business_partner_provider_profile_contact_person", "business_partner");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.BusinessPartnerPositionTypeId).HasColumnName("business_partner_position_type_id");
            entity.Property(e => e.BusinessPartnerProviderProfileId).HasColumnName("business_partner_provider_profile_id");
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

            entity.HasOne(d => d.BusinessPartnerPositionType).WithMany(p => p.BusinessPartnerProviderProfileContactPeople)
                .HasForeignKey(d => d.BusinessPartnerPositionTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_business_partner_provider_profile_contact_person_position_ty");

            entity.HasOne(d => d.BusinessPartnerProviderProfile).WithMany(p => p.BusinessPartnerProviderProfileContactPeople)
                .HasForeignKey(d => d.BusinessPartnerProviderProfileId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_business_partner_provider_profile_contact_person");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.BusinessPartnerProviderProfileContactPersonCreatedByNavigations)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_business_partner_provider_profile_contact_person_created_by");

            entity.HasOne(d => d.ModifiedByNavigation).WithMany(p => p.BusinessPartnerProviderProfileContactPersonModifiedByNavigations)
                .HasForeignKey(d => d.ModifiedBy)
                .HasConstraintName("fk_business_partner_provider_profile_contact_person_modified_by");
        });

        modelBuilder.Entity<BusinessPartnerProviderProfileType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("pk_business_partner_provider_profile_type_id");

            entity.ToTable("business_partner_provider_profile_type", "business_partner");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.IsActive).HasColumnName("is_active");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
        });

        modelBuilder.Entity<BusinessPartnerTransportVehicleStatusType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("pk_business_partner_transport_vehicle_status_type_id");

            entity.ToTable("business_partner_transport_vehicle_status_type", "business_partner");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.IsActive).HasColumnName("is_active");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
        });

        modelBuilder.Entity<BusinessPartnerproviderTransportVehicle>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("pk_business_partner_provider_transport_vehicle_id");

            entity.ToTable("business_partnerprovider_transport_vehicle", "business_partner");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Axles).HasColumnName("axles");
            entity.Property(e => e.Brand)
                .HasMaxLength(50)
                .HasColumnName("brand");
            entity.Property(e => e.BusinessPartnerProviderDriverId).HasColumnName("business_partner_provider_driver_id");
            entity.Property(e => e.BusinessPartnerProviderProfileId).HasColumnName("business_partner_provider_profile_id");
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

            entity.HasOne(d => d.BusinessPartnerProviderDriver).WithMany(p => p.BusinessPartnerproviderTransportVehicles)
                .HasForeignKey(d => d.BusinessPartnerProviderDriverId)
                .HasConstraintName("fk_business_partner_provider_vehicle_driver_business_provider_d");

            entity.HasOne(d => d.BusinessPartnerProviderProfile).WithMany(p => p.BusinessPartnerproviderTransportVehicles)
                .HasForeignKey(d => d.BusinessPartnerProviderProfileId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_business_partner_provider_vehicle_profile_business_provider_");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.BusinessPartnerproviderTransportVehicleCreatedByNavigations)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_business_partner_provider_vehicle_created_by");

            entity.HasOne(d => d.ModifiedByNavigation).WithMany(p => p.BusinessPartnerproviderTransportVehicleModifiedByNavigations)
                .HasForeignKey(d => d.ModifiedBy)
                .HasConstraintName("fk_business_partner_provider_vehicle_modified_by");
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

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
