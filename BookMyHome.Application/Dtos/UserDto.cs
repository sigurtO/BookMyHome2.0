namespace BookMyHome.Application.Dtos
{
    public class UserDto
    {
        public Guid UserID { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string AccountType { get; set; }
    }

}
