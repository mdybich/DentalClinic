namespace DentalClinic.Services.Helpers
{
    public class UserToAdd
    {
        public string Login { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int RoleId { get; set; }
        public string Password { get; set; }
        public string PasswordConfirmation { get; set; }
    }
}
