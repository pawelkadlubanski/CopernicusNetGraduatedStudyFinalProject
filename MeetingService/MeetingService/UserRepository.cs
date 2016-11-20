using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace MeetingService
{
    public class UserRepository : IUserRepository
    {
        private MeetingServiceEntities meetingEntities;

        public UserRepository(MeetingServiceEntities meetingServiceEntities)
        {
            this.meetingEntities = meetingServiceEntities;
        }

        public void addUser(User user)
        {
            meetingEntities.Users.Add(user);
            meetingEntities.SaveChanges();
        }

        public List<User> getAllUsers()
        {
            return meetingEntities.Users.ToList();
        }

        public User getUserByLogin(string login)
        {
            return meetingEntities.Users.Where(m => m.Login.Equals(login)).DefaultIfEmpty(null).Single();
        }

        public User getUserByEmail(string email)
        {
            return meetingEntities.Users.Where(m => m.Email.Equals(email, StringComparison.InvariantCultureIgnoreCase)).DefaultIfEmpty(null).Single();
        }

        public void removeUser(string name)
        {
            var user = getUserByLogin(name);
            if (user != null)
                meetingEntities.Users.Remove(user);
            meetingEntities.SaveChanges();
        }
    }
}
