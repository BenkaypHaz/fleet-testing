namespace Library.Infraestructure.Persistence.DTOs.Auth.Login.Update
{
    public class ResetPasswordDto
    {
        public long UserId { get; set; }
        public string Token { get; set; }
        public string Password { get; set; }
    }
}
