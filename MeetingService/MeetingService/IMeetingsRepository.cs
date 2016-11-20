using System;
using System.Collections.Generic;

namespace MeetingService
{
    public interface IMeetingsRepository
    {
        void addMetting(Meeting meeting);
        List<Meeting> getAllMeetings();
        Meeting getMeetingsByName(string name);
        List<Meeting> getMeetingByDate(DateTime? starDate, DateTime? endDate);
        void removeMeeting(String name);
        void decreaseNumberOfFreePlaces(Meeting meeting);
    }
}