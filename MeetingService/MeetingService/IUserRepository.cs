using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingService
{
    public interface IUserRepository
    {
        List<User> getAllUsers();
        void addUser(User user);
        User getUserByLogin(string login);
        User getUserByEmail(string email);
        void removeUser(string name);
    }
}
