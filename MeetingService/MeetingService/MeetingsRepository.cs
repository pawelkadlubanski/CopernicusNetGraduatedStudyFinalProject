using System;
using System.Collections.Generic;
using System.Linq;

namespace MeetingService
{
    public class MeetingsRepository : IMeetingsRepository
    {
        private MeetingServiceEntities meetingEntities;

        public MeetingsRepository(MeetingServiceEntities meetingServiceEntities)
        {
            this.meetingEntities = meetingServiceEntities;
        }

        public void addMetting(Meeting meeting)
        {
            meetingEntities.Meetings.Add(meeting);
            meetingEntities.SaveChanges();
        }

        public List<Meeting> getAllMeetings()
        {
            return meetingEntities.Meetings.ToList();
        }

        public Meeting getMeetingsByName(string name)
        {
            return meetingEntities.Meetings.Where(m => m.Name.Equals(name)).DefaultIfEmpty(null).Single();
        }

        public List<Meeting> getMeetingByDate(DateTime? starDate, DateTime? endDate)
        {
            if (starDate.HasValue && endDate.HasValue)
                return meetingEntities.Meetings.Where(m => (m.Time.CompareTo(starDate.Value) > 0)
                    && (m.Time.CompareTo(endDate.Value) < 0)).ToList();

            if (starDate.HasValue)
                return meetingEntities.Meetings.Where(m => (m.Time.CompareTo(starDate.Value) > 0)).ToList();

            if (endDate.HasValue)
                return meetingEntities.Meetings.Where(m => (m.Time.CompareTo(endDate.Value) < 0)).ToList();

            return getAllMeetings();
        }

        public void removeMeeting(String name)
        {
            var meeting = getMeetingsByName(name);
            if (meeting != null)
                meetingEntities.Meetings.Remove(meeting);
            meetingEntities.SaveChanges();
        }

        public void decreaseNumberOfFreePlaces(Meeting meeting)
        {
            var meetingToChange = getMeetingsByName(meeting.Name);
            if (meetingToChange != null)
                meetingToChange.NumberOfFreePlaces -= 1;
            meetingEntities.SaveChanges();
        }
    }
}
