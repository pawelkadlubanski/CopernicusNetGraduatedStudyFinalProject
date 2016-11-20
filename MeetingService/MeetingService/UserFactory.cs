namespace MeetingService
{
    public class UserFactory
    {
        public User create(string name, string surname, string login, string email)
        {
            var user = new User();
            user.Name = name;
            user.Surname = surname;
            user.Login = login;
            user.Email = email;
            return user;
        }
    }
}