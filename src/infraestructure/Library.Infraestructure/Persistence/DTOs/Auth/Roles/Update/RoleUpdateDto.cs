namespace Library.Infraestructure.Persistence.DTOs.Auth.Roles.Update
{
    public class RoleUpdateDto
    {
        public required string Description { get; set; }
        public long[] Auth_Id { get; set; }
        public bool IsActive { get; set; }
    }
}
