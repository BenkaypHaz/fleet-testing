using Library.Infraestructure.Persistence.DTOs.Auth.Roles.Read;

namespace Library.Infraestructure.Persistence.DTOs.Auth.Users.Read
{
    public class UserReadFirstDTO
    {
        public long Id { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string UserName { get; set; }
        public required string Email { get; set; }
        public string? ProfilePicture { get; set; }
        public string? PhoneNumber { get; set; }
        public DateTime? BirthDate { get; set; }
        public string? Password { get; set; }
        public bool ResetPassword { get; set; }
        public IEnumerable<RoleReadDTO>? Roles { get; set; }
        public long? CreatedBy { get; set; }
        public string? CreatedByUserName { get; set; }
        public DateTime? CreatedDate { get; set; }
        public long? ModifiedBy { get; set; }
        public string? ModifiedByUserName { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public bool IsActive { get; set; }
    }
}
