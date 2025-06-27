using Library.Infraestructure.Persistence.DTOs.Auth.Authorizations.Read;

namespace Library.Infraestructure.Persistence.DTOs.Auth.Roles.Read
{
    public class RoleReadDTO
    {
        public long Id { get; set; }
        public string? Description { get; set; }
        public bool IsActive { get; set; }
    }
}
