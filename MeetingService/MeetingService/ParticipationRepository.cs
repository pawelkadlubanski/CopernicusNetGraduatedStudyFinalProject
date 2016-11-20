using System;
using System.Collections.Generic;
using System.Linq;

namespace MeetingService
{
    public class ParticipationRepository : IParticipationRepository
    {
        private MeetingServiceEntities meetingEntities;

        private bool checkIfParticipationExist(Participation participation)
        {
            return meetingEntities.Participations.Where(p => p.MeetingName.Equals(participation.MeetingName)
                && p.UserLogin.Equals(participation.UserLogin)).DefaultIfEmpty(null).Single() != null;
        }

        public ParticipationRepository(MeetingServiceEntities meetingServiceEntities)
        {
            this.meetingEntities = meetingServiceEntities;
        }

        public void addParticipations(Participation participation)
        {
            if (!checkIfParticipationExist(participation))
            {
                meetingEntities.Participations.Add(participation);
                meetingEntities.SaveChanges();
            }
        }

        public void removeParticipations(Participation participation)
        {
            if (checkIfParticipationExist(participation))
            {
                meetingEntities.Participations.Remove(participation);
                meetingEntities.SaveChanges();
            }
        }

        public List<Participation> getMeetingParticipants(string meetingName)
        {
            return meetingEntities.Participations.Where(m => m.MeetingName.Equals(meetingName)).ToList();
        }

        public List<Participation> getUserMeetings(string userLogin)
        {
            return meetingEntities.Participations.Where(m => m.UserLogin.Equals(userLogin)).ToList();
        }

    }
}