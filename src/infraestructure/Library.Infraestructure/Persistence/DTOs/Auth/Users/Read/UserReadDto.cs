using Library.Infraestructure.Persistence.DTOs.Auth.Roles.Read;

namespace Library.Infraestructure.Persistence.DTOs.Auth.Users.Read
{
    public class UserReadDTO
    {
        public long Id { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string UserName { get; set; }
        public required string Email { get; set; }
        public string? ProfilePicture { get; set; }
        public bool IsActive { get; set; }
    }
}
