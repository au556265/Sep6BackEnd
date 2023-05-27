// TODO - Check comment in Program.cs
namespace Sep6BackEnd.DataAccess.DomainClasses.DatabaseModels
{
    public class Users
    {
        public int UserId { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}