using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace MeetingService
{
    /// <summary>
    /// Summary description for WebInterface
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class WebInterface : System.Web.Services.WebService
    {
        private MeetingServiceEntities meetingServiceEntities;
        private IMeetingsRepository meetingRepository;
        private IUserRepository userRepository;
        private IParticipationRepository participationRepository;

        private ServiceImpl serviceImpl;

        public WebInterface()
        {
            meetingServiceEntities = new MeetingServiceEntities();
            meetingRepository = new MeetingsRepository(meetingServiceEntities);
            userRepository = new UserRepository(meetingServiceEntities);
            participationRepository = new ParticipationRepository(meetingServiceEntities);

            serviceImpl = new ServiceImpl(meetingRepository, userRepository, participationRepository);
        }

        [WebMethod]
        public void addMeeting(String name, String location, string date, int maxNumberOfParticipants)
        {
            serviceImpl.addMeeting(name, location, date, maxNumberOfParticipants);
        }

        [WebMethod]
        public void removeMeeting(String name)
        {
            serviceImpl.removeMeeting(name);
        }

        [WebMethod]
        public List<Meeting> getAllMeetingsList()
        {
            return serviceImpl.getAllMeetingsList();
        }

        [WebMethod]
        public Meeting getMeetingsByName(string name)
        {
            return serviceImpl.getMeetingsByName(name);
        }

        [WebMethod]
        public List<Meeting> getMeetingsByDate(DateTime? startDate = null, DateTime? endDate = null)
        {
            return serviceImpl.getMeetingsByDate(startDate, endDate);
        }

        [WebMethod]
        public List<User> getAllUsers()
        {
            return serviceImpl.getAllUsers();
        }

        [WebMethod]
        public void addUser(string name, string surname, string login, string email)
        {
            serviceImpl.addUser(name, surname, login, email);
        }

        [WebMethod]
        public User getUserByLogin(string login)
        {
            return serviceImpl.getUserByLogin(login);
        }

        [WebMethod]
        public void removeUser(string name)
        {
            serviceImpl.removeUser(name);
        }

        [WebMethod]
        public void signUpUserToMeeting(string login, string name)
        {
            serviceImpl.signUpUserToMeeting(login, name);
        }

        [WebMethod]
        public void signOutUserFromMeeting(string login, string name)
        {
            serviceImpl.signOutUserFromMeeting(login, name);
        }

        [WebMethod]
        public List<Meeting> getUserMeetings(string userLogin)
        {
            return serviceImpl.getUserMeetings(userLogin);
        }

        [WebMethod]
        public List<User> getMeetingParticipants(string meetingName)
        {
            return serviceImpl.getMeetingParticipants(meetingName);
        }
    }
}
