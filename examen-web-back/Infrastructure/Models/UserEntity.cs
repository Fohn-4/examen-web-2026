namespace Infrastructure.Models
{
    public class UserEntity
    {
        public string UUID { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PasswordHash { get; set; }
        public string RoleNames { get; set; }

    }
}