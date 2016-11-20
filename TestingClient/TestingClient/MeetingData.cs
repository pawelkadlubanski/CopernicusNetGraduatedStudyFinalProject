using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestingClient
{
    public class MeetingData
    {
        public String Name { get; set; }
        public String Location { get; set; }
        public String Date { get; set; }
        public int MaxNumberOfParticipants { get; set; }

        public MeetingData(String name, String location, String date, int maxNumberOfParticipants)
        {
            this.Name = name;
            this.Location = location;
            this.Date = date;
            this.MaxNumberOfParticipants = maxNumberOfParticipants;
        }

        public MeetingService.Meeting convertToMeeting()
        {
            var meeting = new MeetingService.Meeting();
            meeting.Name = this.Name;
            meeting.Location = this.Location;
            meeting.MaxNumberOfParticipants = this.MaxNumberOfParticipants;
            meeting.NumberOfFreePlaces = this.MaxNumberOfParticipants;
            meeting.Time = DateTime.ParseExact(this.Date, "yyyy-MM-dd HH:mm", System.Globalization.CultureInfo.InvariantCulture);
            return meeting;
        }
    }


}
