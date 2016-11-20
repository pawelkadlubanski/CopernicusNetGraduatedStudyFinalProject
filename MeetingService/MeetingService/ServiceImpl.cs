using System;
using System.Collections.Generic;

namespace MeetingService
{
    public class ServiceImpl
    {

        private MeetingFactory meetingFactory;
        private UserFactory userFactory;
        private ParticipationFactory participationFactory;

        private IMeetingsRepository meetingsRepository;
        private IUserRepository userRepository;
        private IParticipationRepository participationRepository;

        public ServiceImpl(IMeetingsRepository meetingsRepository, IUserRepository userRepository, IParticipationRepository participationRepository)
        {
            meetingFactory = new MeetingFactory();
            userFactory = new UserFactory();
            participationFactory = new ParticipationFactory();

            this.meetingsRepository = meetingsRepository;
            this.userRepository = userRepository;
            this.participationRepository = participationRepository;
        }

        public void addMeeting(String name, String location, string date, int maxNumberOfParticipants)
        {
            if (meetingsRepository.getMeetingsByName(name) != null)
                return;

            var meeting = meetingFactory.create(name, location, date, maxNumberOfParticipants);
            if (meeting != null)
            {
                meetingsRepository.addMetting(meeting);
            }
        }

        public List<Meeting> getAllMeetingsList()
        {
            return meetingsRepository.getAllMeetings();
        }

        public Meeting getMeetingsByName(string name)
        {
            return meetingsRepository.getMeetingsByName(name);
        }

        public List<Meeting> getMeetingsByDate(DateTime? startDate = null, DateTime? endDate = null)
        {
            return meetingsRepository.getMeetingByDate(startDate, endDate);
        }

        public void removeMeeting(String name)
        {
            var meetingParticipations = participationRepository.getMeetingParticipants(name);
            foreach(var participation in meetingParticipations)
                participationRepository.removeParticipations(participation);
            meetingsRepository.removeMeeting(name);
        }

        public List<User> getAllUsers()
        {
            return userRepository.getAllUsers();
        }

        public void addUser(string name, string surname, string login, string email)
        {
            if (userRepository.getUserByLogin(login) == null && userRepository.getUserByEmail(email) == null)
            {
                var user = userFactory.create(name, surname, login, email);
                userRepository.addUser(user);
            }
        }

        public User getUserByLogin(string login)
        {
            return userRepository.getUserByLogin(login);
        }

        public void removeUser(string name)
        {
            var userParticipations = participationRepository.getUserMeetings(name);
            foreach(var participation in userParticipations)
                participationRepository.removeParticipations(participation);
            userRepository.removeUser(name);
        }

        public void signUpUserToMeeting(string login, string name)
        {

            var meeting = this.getMeetingsByName(name);
            if (meeting == null || meeting.NumberOfFreePlaces == 0)
                return;

            var user = this.getUserByLogin(login);
            if (user == null)
                return;

            var participation = participationFactory.create(login, name);
            participationRepository.addParticipations(participation);
            meetingsRepository.decreaseNumberOfFreePlaces(meeting);
        }

        public void signOutUserFromMeeting(string login, string name)
        {

            var meeting = this.getMeetingsByName(name);
            if (meeting == null || meeting.NumberOfFreePlaces == 0)
                return;

            var user = this.getUserByLogin(login);
            if (user == null)
                return;

            var participation = participationFactory.create(login, name);
            participationRepository.removeParticipations(participation);
            meeting.NumberOfFreePlaces -= 1;
        }


        public List<Meeting> getUserMeetings(string UserLogin)
        {
            var usersMeeting = new List<Meeting>();
            var usersParticipations = participationRepository.getUserMeetings(UserLogin);
            foreach (var participation in usersParticipations)
            {
                usersMeeting.Add(this.getMeetingsByName(participation.MeetingName));
            }
            return usersMeeting;
        }

        public List<User> getMeetingParticipants(string meetingName)
        {
            var participans = new List<User>();
            var participations = participationRepository.getMeetingParticipants(meetingName);
            foreach (var participation in participations)
            {
                participans.Add(this.getUserByLogin(participation.UserLogin));
            }

            return participans;
        }
    }
}