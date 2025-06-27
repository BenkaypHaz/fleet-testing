namespace Library.Infraestructure.Persistence.DTOs.Auth.Users.Update
{
    public class ForgotPwdTokenUpdateDto
    {
        public int Id { get; set; }
        public string Value { get; set; }
        public long User_Id { get; set; }
        public DateTime ExpirationDate { get; set; }
    }
}
