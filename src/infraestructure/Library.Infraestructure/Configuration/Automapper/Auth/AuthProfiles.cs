using Library.Infraestructure.Persistence.DTOs.Auth.Authorizations.Read;
using Library.Infraestructure.Persistence.DTOs.Auth.Roles.Create;
using Library.Infraestructure.Persistence.DTOs.Auth.Roles.Read;
using Library.Infraestructure.Persistence.DTOs.Auth.Roles.Update;
using Library.Infraestructure.Persistence.DTOs.Auth.Users.Read;
using Library.Infraestructure.Persistence.Models.PostgreSQL;

namespace Library.Infraestructure.Configuration.Automapper.Auth
{
    public class AuthProfiles : AutoMapper.Profile
    {
        public AuthProfiles()
        {
            #region Authorizations

            CreateMap<AuthAuthorization, AuthorizationReadDto>();

            #endregion

            #region Modules

            CreateMap<AuthModule, ModuleReadDto>()
                .ForMember(dest => dest.Authorizations, opt => opt.MapFrom(src => src.AuthAuthorizations));

            #endregion

            #region Roles

            CreateMap<AuthRole, RoleReadDTO>();
            CreateMap<AuthRole, RoleReadFirstDto>()
                .ForMember(dest => dest.Authorizations, opt => opt.MapFrom(src => src.AuthRoleAuthorizations.Select(c => c.Auth)))
                .ForMember(dest => dest.CreatedByUserName, opt => opt.MapFrom(src => src.CreatedByNavigation.UserName))
                .ForMember(dest => dest.ModifiedByUserName, opt => opt.MapFrom(src => src.ModifiedByNavigation.UserName));

            CreateMap<RoleCreateDto, AuthRole>()
                .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(_ => DateTime.UtcNow))
                .ForMember(dest => dest.IsActive, opt => opt.MapFrom(_ => true));

            CreateMap<RoleUpdateDto, AuthRole>()
                .ForMember(dest => dest.ModifiedDate, opt => opt.MapFrom(_ => DateTime.UtcNow));

            #endregion

            #region Users

            CreateMap<AuthUser, AssignedUserReadFirstDto>();

            #endregion

        }
    }
}
