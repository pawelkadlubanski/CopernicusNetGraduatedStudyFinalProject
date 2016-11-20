using System;

namespace MeetingService
{
    public class MeetingFactory
    {
        public Meeting create(String name, String location, string date, int maxNumberOfParticipants)
        {
            var convertedDate = ConverDateStringToDateTime(date);
            if (chaeckIfAnyOfArgumentIsNull(name, location, convertedDate))
            {
                return null;
            }

            var meeting = new Meeting();
            meeting.Name = name;
            meeting.Location = location;
            meeting.Time = convertedDate.Value;
            meeting.MaxNumberOfParticipants = maxNumberOfParticipants;
            meeting.NumberOfFreePlaces = meeting.MaxNumberOfParticipants;

            return meeting;
        }

        private bool chaeckIfAnyOfArgumentIsNull(String name, String location, Nullable<DateTime> date)
        {
            return (name == null || location == null || !date.HasValue);
        }

        private Nullable<DateTime> ConverDateStringToDateTime(String date)
        {
            DateTime tmpDate;
            try
            {
                tmpDate = DateTime.ParseExact(date, "yyyy-MM-dd HH:mm", System.Globalization.CultureInfo.InvariantCulture);
                return tmpDate;
            }
            catch
            {
                return null;
            }
       }
    }
}