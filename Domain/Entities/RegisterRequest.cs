namespace Domain.Entities
{
    public class RegisterRequest
    {
        public string Token { get; set; } = "";
        public string Password { get; set; } = "";
        public string FirstName { get; set; } = "";
        public string LastName { get; set; } = "";
    }
}
