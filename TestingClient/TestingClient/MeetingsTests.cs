using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace TestingClient
{
    [TestClass]
    public class MeetingsTests
    {
        MeetingData meetingDataA;
        MeetingData meetingDataB;
        MeetingData meetingDataC;
        MeetingData meetingDataD;

        private MeetingService.WebInterface service;

        private bool checkIfMeetingAreEquel(MeetingService.Meeting meetingA, MeetingService.Meeting meetingB)
        {
            if (!meetingA.Location.Equals(meetingB.Location))
                return false;
            if (!meetingA.Name.Equals(meetingB.Name))
                return false;
            if (!meetingA.Time.Equals(meetingB.Time))
                return false;
            if (!meetingA.MaxNumberOfParticipants.Equals(meetingB.MaxNumberOfParticipants))
                return false;
            return true;

        }

        private bool checkIfListOfMeetingAreEqual(List<MeetingService.Meeting> listA, List<MeetingService.Meeting> listB)
        {
            if (listA.Count != listB.Count)
                return false;
            for (int i = 0; i < listA.Count; i++)
                if (!checkIfMeetingAreEquel(listA.ElementAt(i), listB.ElementAt(i)))
                    return false;
            return true;
        }

        private bool checkIfUserAreEqual(MeetingService.User userA, MeetingService.User userB)
        {
            return userA.Login.Equals(userB.Login)
                && userA.Name.Equals(userB.Name)
                && userA.Surname.Equals(userB.Surname)
                && userA.Email.Equals(userB.Email);
        }

        private bool checkIfUsersListAreEqual(List<MeetingService.User> listA, List<MeetingService.User> listB)
        {
            if (listA.Count != listB.Count)
                return false;
            for(var i = 1; i< listA.Count; i++)
                if (!checkIfUserAreEqual(listA.ElementAt(i), listB.ElementAt(i)))
                    return false;
            return true;
        }

        [TestInitialize]
        public void Initialize()
        {
            meetingDataA = new MeetingData("AA", "AA", "2005-03-05 22:12", 10);
            meetingDataB = new MeetingData("BB", "BB", "2005-04-05 22:12", 10);
            meetingDataC = new MeetingData("CC", "CC", "2005-05-05 22:12", 10);
            meetingDataD = new MeetingData("DD", "DD", "2005-06-05 22:12", 10);

            service = new MeetingService.WebInterface();
        }

        [TestMethod]
        public void AddMeetingWithCorrectData()
        {
            Assert.IsNull(service.getMeetingsByName(meetingDataA.Name));
            service.addMeeting(meetingDataA.Name, meetingDataA.Location, meetingDataA.Date, meetingDataA.MaxNumberOfParticipants);
            Assert.IsTrue(checkIfMeetingAreEquel(meetingDataA.convertToMeeting(), service.getMeetingsByName(meetingDataA.Name)));
            service.removeMeeting(meetingDataA.Name);
            Assert.IsNull(service.getMeetingsByName(meetingDataA.Name));
        }


        [TestMethod]
        public void AddTwoMeetingWithCorrectData()
        {
            Assert.IsNull(service.getMeetingsByName(meetingDataA.Name));
            Assert.IsNull(service.getMeetingsByName(meetingDataB.Name));

            service.addMeeting(meetingDataA.Name, meetingDataA.Location, meetingDataA.Date, meetingDataA.MaxNumberOfParticipants);
            service.addMeeting(meetingDataB.Name, meetingDataB.Location, meetingDataB.Date, meetingDataB.MaxNumberOfParticipants);

            Assert.IsTrue(checkIfMeetingAreEquel(meetingDataA.convertToMeeting(), service.getMeetingsByName(meetingDataA.Name)));
            Assert.IsTrue(checkIfMeetingAreEquel(meetingDataB.convertToMeeting(), service.getMeetingsByName(meetingDataB.Name)));

            service.removeMeeting(meetingDataA.Name);
            service.removeMeeting(meetingDataB.Name);

            Assert.IsNull(service.getMeetingsByName(meetingDataA.Name));
            Assert.IsNull(service.getMeetingsByName(meetingDataB.Name));
        }

        [TestMethod]
        public void AddwiceTeSame()
        {
            Assert.IsNull(service.getMeetingsByName(meetingDataA.Name));
            var expextedMeetingListFromService = service.getAllMeetingsList().ToList();
            expextedMeetingListFromService.Add(meetingDataA.convertToMeeting());

            service.addMeeting(meetingDataA.Name, meetingDataA.Location, meetingDataA.Date, meetingDataA.MaxNumberOfParticipants);
            service.addMeeting(meetingDataA.Name, meetingDataA.Location, meetingDataA.Date, meetingDataA.MaxNumberOfParticipants);

            Assert.IsTrue(checkIfListOfMeetingAreEqual(expextedMeetingListFromService, service.getAllMeetingsList().ToList()));

            service.removeMeeting(meetingDataA.Name);
            Assert.IsNull(service.getMeetingsByName(meetingDataA.Name));
        }
        [TestMethod]
        public void AddMeetingWitOneNullData()
        {
            Assert.IsNull(service.getMeetingsByName(meetingDataA.Name));
            meetingDataA.Location = null;
            service.addMeeting(meetingDataA.Name, meetingDataA.Location, meetingDataA.Date, meetingDataA.MaxNumberOfParticipants);
            Assert.IsNull(service.getMeetingsByName(meetingDataA.Name));
        }

        [TestMethod]
        public void AddMeetingDateInWrongFormat()
        {
            Assert.IsNull(service.getMeetingsByName(meetingDataA.Name));
            meetingDataA.Date = "2005/05/02 22:12";
            service.addMeeting(meetingDataA.Name, meetingDataA.Location, meetingDataA.Date, meetingDataA.MaxNumberOfParticipants);
            Assert.IsNull(service.getMeetingsByName(meetingDataA.Name));
        }

        [TestMethod]
        public void AddMeetingDateIsNull()
        {
            Assert.IsNull(service.getMeetingsByName(meetingDataA.Name));
            meetingDataA.Date = null;
            service.addMeeting(meetingDataA.Name, meetingDataA.Location, meetingDataA.Date, meetingDataA.MaxNumberOfParticipants);
            Assert.IsNull(service.getMeetingsByName(meetingDataA.Name));
        }

        [TestMethod]
        public void GetAllMeetingWithoutGivenDate()
        {
            Assert.IsNull(service.getMeetingsByName(meetingDataA.Name));
            Assert.IsNull(service.getMeetingsByName(meetingDataB.Name));
            Assert.IsNull(service.getMeetingsByName(meetingDataC.Name));
            Assert.IsNull(service.getMeetingsByName(meetingDataD.Name));

            service.addMeeting(meetingDataA.Name, meetingDataA.Location, meetingDataA.Date, meetingDataA.MaxNumberOfParticipants);
            service.addMeeting(meetingDataB.Name, meetingDataB.Location, meetingDataB.Date, meetingDataB.MaxNumberOfParticipants);
            service.addMeeting(meetingDataC.Name, meetingDataC.Location, meetingDataC.Date, meetingDataC.MaxNumberOfParticipants);
            service.addMeeting(meetingDataD.Name, meetingDataD.Location, meetingDataD.Date, meetingDataD.MaxNumberOfParticipants);

            DateTime? startDate = null;
            DateTime? endDate = null;

            var expectedListOfMeeting = new List<MeetingService.Meeting>
            {
                meetingDataA.convertToMeeting(),
                meetingDataB.convertToMeeting(),
                meetingDataC.convertToMeeting(),
                meetingDataD.convertToMeeting()
            };

            Assert.IsTrue(checkIfListOfMeetingAreEqual(expectedListOfMeeting, service.getMeetingsByDate(startDate, endDate).ToList()));

            service.removeMeeting(meetingDataA.Name);
            service.removeMeeting(meetingDataB.Name);
            service.removeMeeting(meetingDataC.Name);
            service.removeMeeting(meetingDataD.Name);

            Assert.IsNull(service.getMeetingsByName(meetingDataA.Name));
            Assert.IsNull(service.getMeetingsByName(meetingDataB.Name));
            Assert.IsNull(service.getMeetingsByName(meetingDataC.Name));
            Assert.IsNull(service.getMeetingsByName(meetingDataD.Name));
        }


        [TestMethod]
        public void GetAllMeetingBetweenGivenDate()
        {
            Assert.IsNull(service.getMeetingsByName(meetingDataA.Name));
            Assert.IsNull(service.getMeetingsByName(meetingDataB.Name));
            Assert.IsNull(service.getMeetingsByName(meetingDataC.Name));
            Assert.IsNull(service.getMeetingsByName(meetingDataD.Name));

            service.addMeeting(meetingDataA.Name, meetingDataA.Location, meetingDataA.Date, meetingDataA.MaxNumberOfParticipants);
            service.addMeeting(meetingDataB.Name, meetingDataB.Location, meetingDataB.Date, meetingDataB.MaxNumberOfParticipants);
            service.addMeeting(meetingDataC.Name, meetingDataC.Location, meetingDataC.Date, meetingDataC.MaxNumberOfParticipants);
            service.addMeeting(meetingDataD.Name, meetingDataD.Location, meetingDataD.Date, meetingDataD.MaxNumberOfParticipants);

            DateTime startDate = meetingDataA.convertToMeeting().Time.AddDays(2);
            DateTime endDate = meetingDataD.convertToMeeting().Time.AddDays(-2);

            var expectedListOfMeeting = new List<MeetingService.Meeting>
            {
                meetingDataB.convertToMeeting(),
                meetingDataC.convertToMeeting(),
            };

            Assert.IsTrue(checkIfListOfMeetingAreEqual(expectedListOfMeeting, service.getMeetingsByDate(startDate, endDate).ToList()));

            service.removeMeeting(meetingDataA.Name);
            service.removeMeeting(meetingDataB.Name);
            service.removeMeeting(meetingDataC.Name);
            service.removeMeeting(meetingDataD.Name);

            Assert.IsNull(service.getMeetingsByName(meetingDataA.Name));
            Assert.IsNull(service.getMeetingsByName(meetingDataB.Name));
            Assert.IsNull(service.getMeetingsByName(meetingDataC.Name));
            Assert.IsNull(service.getMeetingsByName(meetingDataD.Name));
        }

        [TestMethod]
        public void GetAllMeetingBeforGivenDate()
        {
            Assert.IsNull(service.getMeetingsByName(meetingDataA.Name));
            Assert.IsNull(service.getMeetingsByName(meetingDataB.Name));
            Assert.IsNull(service.getMeetingsByName(meetingDataC.Name));
            Assert.IsNull(service.getMeetingsByName(meetingDataD.Name));

            service.addMeeting(meetingDataA.Name, meetingDataA.Location, meetingDataA.Date, meetingDataA.MaxNumberOfParticipants);
            service.addMeeting(meetingDataB.Name, meetingDataB.Location, meetingDataB.Date, meetingDataB.MaxNumberOfParticipants);
            service.addMeeting(meetingDataC.Name, meetingDataC.Location, meetingDataC.Date, meetingDataC.MaxNumberOfParticipants);
            service.addMeeting(meetingDataD.Name, meetingDataD.Location, meetingDataD.Date, meetingDataD.MaxNumberOfParticipants);

            DateTime? startDate = null;
            DateTime endDate = meetingDataD.convertToMeeting().Time.AddDays(-2);

            var expectedListOfMeeting = new List<MeetingService.Meeting>
            {
                meetingDataA.convertToMeeting(),
                meetingDataB.convertToMeeting(),
                meetingDataC.convertToMeeting(),
            };

            Assert.IsTrue(checkIfListOfMeetingAreEqual(expectedListOfMeeting, service.getMeetingsByDate(startDate, endDate).ToList()));

            service.removeMeeting(meetingDataA.Name);
            service.removeMeeting(meetingDataB.Name);
            service.removeMeeting(meetingDataC.Name);
            service.removeMeeting(meetingDataD.Name);

            Assert.IsNull(service.getMeetingsByName(meetingDataA.Name));
            Assert.IsNull(service.getMeetingsByName(meetingDataB.Name));
            Assert.IsNull(service.getMeetingsByName(meetingDataC.Name));
            Assert.IsNull(service.getMeetingsByName(meetingDataD.Name));
        }

        [TestMethod]
        public void GetAllMeetingAfterGivenDate()
        {
            Assert.IsNull(service.getMeetingsByName(meetingDataA.Name));
            Assert.IsNull(service.getMeetingsByName(meetingDataB.Name));
            Assert.IsNull(service.getMeetingsByName(meetingDataC.Name));
            Assert.IsNull(service.getMeetingsByName(meetingDataD.Name));

            service.addMeeting(meetingDataA.Name, meetingDataA.Location, meetingDataA.Date, meetingDataA.MaxNumberOfParticipants);
            service.addMeeting(meetingDataB.Name, meetingDataB.Location, meetingDataB.Date, meetingDataB.MaxNumberOfParticipants);
            service.addMeeting(meetingDataC.Name, meetingDataC.Location, meetingDataC.Date, meetingDataC.MaxNumberOfParticipants);
            service.addMeeting(meetingDataD.Name, meetingDataD.Location, meetingDataD.Date, meetingDataD.MaxNumberOfParticipants);
            
            DateTime startDate = meetingDataA.convertToMeeting().Time.AddDays(2);
            DateTime? endDate = null;

            var expectedListOfMeeting = new List<MeetingService.Meeting>
            {
                meetingDataB.convertToMeeting(),
                meetingDataC.convertToMeeting(),
                meetingDataD.convertToMeeting()
            };

            Assert.IsTrue(checkIfListOfMeetingAreEqual(expectedListOfMeeting, service.getMeetingsByDate(startDate, endDate).ToList()));

            service.removeMeeting(meetingDataA.Name);
            service.removeMeeting(meetingDataB.Name);
            service.removeMeeting(meetingDataC.Name);
            service.removeMeeting(meetingDataD.Name);

            Assert.IsNull(service.getMeetingsByName(meetingDataA.Name));
            Assert.IsNull(service.getMeetingsByName(meetingDataB.Name));
            Assert.IsNull(service.getMeetingsByName(meetingDataC.Name));
            Assert.IsNull(service.getMeetingsByName(meetingDataD.Name));
        }

        [TestMethod]
        public void GetListOfUsersWhenRepositoryIsEmpty()
        {
           Assert.AreEqual(0, service.getAllUsers().ToList().Count);
        }

        [TestMethod]
        public void AddUserWithCorrectData()
        {
            var givenUserData = new UserData("Jan", "Kowalski", "jankow", "jan.kowalski@gmail.com");
            Assert.AreEqual(null, service.getUserByLogin(givenUserData.Login));

            service.addUser(givenUserData.Name, givenUserData.Surname, givenUserData.Login, givenUserData.Email);

            Assert.IsTrue(checkIfUserAreEqual(givenUserData.convertToUser(), service.getUserByLogin(givenUserData.Login)));

            service.removeUser(givenUserData.Login);
            Assert.AreEqual(null, service.getUserByLogin(givenUserData.Login));
        }

        [TestMethod]
        public void AddUserWithLoginThatExists()
        {
            var givenUserDataA = new UserData("Jan", "Kowalski", "jankow", "jan.kowalski@gmail.com");
            var givenUserDataB = new UserData("Adam", "Mrozek", givenUserDataA.Login, "adam.mrozek@gmail.com");

            Assert.AreEqual(null, service.getUserByLogin(givenUserDataA.Login));

            service.addUser(givenUserDataA.Name, givenUserDataA.Surname, givenUserDataA.Login, givenUserDataA.Email);
            service.addUser(givenUserDataB.Name, givenUserDataB.Surname, givenUserDataB.Login, givenUserDataB.Email);

            var expectedUserList = new List<MeetingService.User> { givenUserDataA.convertToUser() }; 
            Assert.IsTrue(checkIfUsersListAreEqual(expectedUserList, service.getAllUsers().ToList()));

            service.removeUser(givenUserDataA.Login);
            Assert.AreEqual(null, service.getUserByLogin(givenUserDataA.Login));
        }

        [TestMethod]
        public void AddUserWithEmailThatExists()
        {
            var givenUserDataA = new UserData("Jan", "Kowalski", "jankow", "jan.kowalski@gmail.com");
            var givenUserDataB = new UserData("Adam", "Mrozek", "adamek", givenUserDataA.Email);

            Assert.AreEqual(null, service.getUserByLogin(givenUserDataA.Login));

            service.addUser(givenUserDataA.Name, givenUserDataA.Surname, givenUserDataA.Login, givenUserDataA.Email);
            service.addUser(givenUserDataB.Name, givenUserDataB.Surname, givenUserDataB.Login, givenUserDataB.Email);

            var expectedUserList = new List<MeetingService.User> { givenUserDataA.convertToUser() };
            Assert.IsTrue(checkIfUsersListAreEqual(expectedUserList, service.getAllUsers().ToList()));

            service.removeUser(givenUserDataA.Login);
            Assert.AreEqual(null, service.getUserByLogin(givenUserDataA.Login));
        }

        [TestMethod]
        public void AddUserWithEmailThatExistsButEmailIsGivenWithUppercase()
        {
            var givenUserDataA = new UserData("Jan", "Kowalski", "jankow", "jan.kowalski@gmail.com");
            var givenUserDataB = new UserData("Adam", "Mrozek", "adamek", givenUserDataA.Email.ToUpper());

            Assert.AreEqual(null, service.getUserByLogin(givenUserDataA.Login));

            service.addUser(givenUserDataA.Name, givenUserDataA.Surname, givenUserDataA.Login, givenUserDataA.Email);
            service.addUser(givenUserDataB.Name, givenUserDataB.Surname, givenUserDataB.Login, givenUserDataB.Email);

            var expectedUserList = new List<MeetingService.User> { givenUserDataA.convertToUser() };
            Assert.IsTrue(checkIfUsersListAreEqual(expectedUserList, service.getAllUsers().ToList()));

            service.removeUser(givenUserDataA.Login);
            Assert.AreEqual(null, service.getUserByLogin(givenUserDataA.Login));
        }
        [TestMethod]
        public void AddTwoUserWithCorrectData()
        {
            var givenUserDataA = new UserData("Jan", "Kowalski", "jankow", "jan.kowalski@gmail.com");
            var givenUserDataB = new UserData("Adam", "Mrozek", "adamek", "adam.mrozek@mail.com");

            Assert.AreEqual(null, service.getUserByLogin(givenUserDataA.Login));
            Assert.AreEqual(null, service.getUserByLogin(givenUserDataB.Login));

            service.addUser(givenUserDataA.Name, givenUserDataA.Surname, givenUserDataA.Login, givenUserDataA.Email);
            service.addUser(givenUserDataB.Name, givenUserDataB.Surname, givenUserDataB.Login, givenUserDataB.Email);

            var expectedUserList = new List<MeetingService.User>
            {
                givenUserDataA.convertToUser(),
                givenUserDataB.convertToUser()
            };

            Assert.IsTrue(checkIfUsersListAreEqual(expectedUserList, service.getAllUsers().ToList()));

            service.removeUser(givenUserDataA.Login);
            service.removeUser(givenUserDataB.Login);

            Assert.AreEqual(null, service.getUserByLogin(givenUserDataA.Login));
            Assert.AreEqual(null, service.getUserByLogin(givenUserDataB.Login));
        }

        [TestMethod]
        public void GetUsersMeetingWhenUserHasNoMeeting()
        {
            var givenUserData = new UserData("Jan", "Kowalski", "jankow", "jan.kowalski@gmail.com");
            service.addUser(givenUserData.Name, givenUserData.Surname, givenUserData.Login, givenUserData.Email);

            Assert.AreEqual(0, service.getUserMeetings(givenUserData.Login).ToList().Count);

            service.removeUser(givenUserData.Login);
            Assert.AreEqual(null, service.getUserByLogin(givenUserData.Login));
        }

        [TestMethod]
        public void GetUsersMeeting()
        {
            var userData = new UserData("Jan", "Kowalski", "jankow", "jan.kowalski@gmail.com");
            var meetingDataA = new MeetingData("AA", "AA", "2005-03-05 22:12", 10);
            var meetingDataB = new MeetingData("BB", "BB", "2005-04-05 22:12", 10);

            service.addUser(userData.Name, userData.Surname, userData.Login, userData.Email);
            service.addMeeting(meetingDataA.Name, meetingDataA.Location, meetingDataA.Date, meetingDataA.MaxNumberOfParticipants);
            service.addMeeting(meetingDataB.Name, meetingDataB.Location, meetingDataB.Date, meetingDataB.MaxNumberOfParticipants);

            service.signUpUserToMeeting(userData.Login, meetingDataA.Name);
            service.signUpUserToMeeting(userData.Login, meetingDataB.Name);

            var expectedMeetingList = new List<MeetingService.Meeting>
            {
                meetingDataA.convertToMeeting(),
                meetingDataB.convertToMeeting()
            };

            var i = service.getUserMeetings(userData.Login).ToList();
            Assert.IsTrue(checkIfListOfMeetingAreEqual(expectedMeetingList, service.getUserMeetings(userData.Login).ToList()));

            service.removeUser(userData.Login);

            service.removeMeeting(meetingDataA.Name);
            service.removeMeeting(meetingDataB.Name);
        }

        [TestMethod]
        public void GetMeetingParticipantsWhenLackOfParticipant()
        {
            var meetingDataA = new MeetingData("AA", "AA", "2005-03-05 22:12", 10);
            service.addMeeting(meetingDataA.Name, meetingDataA.Location, meetingDataA.Date, meetingDataA.MaxNumberOfParticipants);
            Assert.AreEqual(0, service.getMeetingParticipants(meetingDataA.Name).ToList().Count);
            service.removeMeeting(meetingDataA.Name);
        }
        [TestMethod]
        public void GetMeetingParticipants()
        {
            var meetingData = new MeetingData("AA", "AA", "2005-03-05 22:12", 10);
            var userDataA = new UserData("Jan", "Kowalski", "jankow", "jan.kowalski@gmail.com");
            var userDataB = new UserData("Adam", "Mrozek", "mrozek", "adam.mrozek@gmail.com");

            service.addMeeting(meetingData.Name, meetingData.Location, meetingData.Date, meetingData.MaxNumberOfParticipants);
            service.addUser(userDataA.Name, userDataA.Surname, userDataA.Login, userDataA.Email);
            service.addUser(userDataB.Name, userDataB.Surname, userDataB.Login, userDataB.Email);

            service.signUpUserToMeeting(userDataA.Login, meetingData.Name);
            service.signUpUserToMeeting(userDataB.Login, meetingData.Name);

            var expectedUserList = new List<MeetingService.User>
            {
                userDataA.convertToUser(),
                userDataB.convertToUser()
            };

            Assert.IsTrue(checkIfUsersListAreEqual(expectedUserList, service.getMeetingParticipants(meetingData.Name).ToList()));

            service.removeUser(userDataA.Login);
            service.removeUser(userDataB.Login);

            service.removeMeeting(meetingData.Name);
        }

        [TestMethod]
        public void SignUserTwiceOnTheSameMeetingIsNotPosible()
        {
            var meetingData = new MeetingData("AA", "AA", "2005-03-05 22:12", 10);
            var userDataA = new UserData("Jan", "Kowalski", "jankow", "jan.kowalski@gmail.com");

            service.addMeeting(meetingData.Name, meetingData.Location, meetingData.Date, meetingData.MaxNumberOfParticipants);
            service.addUser(userDataA.Name, userDataA.Surname, userDataA.Login, userDataA.Email);

            service.signUpUserToMeeting(userDataA.Login, meetingData.Name);
            service.signUpUserToMeeting(userDataA.Login, meetingData.Name);

            var expectedUserList = new List<MeetingService.User>
            {
                userDataA.convertToUser()
            };

            Assert.IsTrue(checkIfUsersListAreEqual(expectedUserList, service.getMeetingParticipants(meetingData.Name).ToList()));

            service.removeUser(userDataA.Login);

            service.removeMeeting(meetingData.Name);
        }
        [TestMethod]
        public void SignUserToMeetoingWithoutFreePlacesIsNotPosible()
        {
            var meetingData = new MeetingData("AA", "AA", "2005-03-05 22:12", 1);
            var userDataA = new UserData("Jan", "Kowalski", "jankow", "jan.kowalski@gmail.com");
            var userDataB = new UserData("Adam", "Mrozek", "mrozek", "adam.mrozek@gmail.com");

            service.addMeeting(meetingData.Name, meetingData.Location, meetingData.Date, meetingData.MaxNumberOfParticipants);
            service.addUser(userDataA.Name, userDataA.Surname, userDataA.Login, userDataA.Email);
            service.addUser(userDataB.Name, userDataB.Surname, userDataB.Login, userDataB.Email);

            service.signUpUserToMeeting(userDataA.Login, meetingData.Name);
            service.signUpUserToMeeting(userDataB.Login, meetingData.Name);

            var expectedUserList = new List<MeetingService.User>
            {
                userDataA.convertToUser(),
            };

            Assert.IsTrue(checkIfUsersListAreEqual(expectedUserList, service.getMeetingParticipants(meetingData.Name).ToList()));

            service.removeUser(userDataA.Login);
            service.removeUser(userDataB.Login);

            service.removeMeeting(meetingData.Name);
        }
    }
}
