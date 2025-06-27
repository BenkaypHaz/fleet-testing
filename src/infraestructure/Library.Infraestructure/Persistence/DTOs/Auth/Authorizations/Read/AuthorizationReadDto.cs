namespace Library.Infraestructure.Persistence.DTOs.Auth.Authorizations.Read
{
    public class AuthorizationReadDto
    {
        public long Id { get; set; }
        public string? Description { get; set; }
        public string? RouteValue { get; set; }
        public bool IsActive { get; set; }
    }
}
