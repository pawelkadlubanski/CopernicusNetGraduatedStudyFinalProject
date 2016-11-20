using System.Collections.Generic;

namespace MeetingService
{
    public interface IParticipationRepository
    {
        void addParticipations(Participation participation);
        List<Participation> getUserMeetings(string userLogin);
        List<Participation> getMeetingParticipants(string meetingName);
        void removeParticipations(Participation participation);
    }
}