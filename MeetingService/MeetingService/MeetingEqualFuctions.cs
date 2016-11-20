using System;

namespace MeetingService
{
    public partial class Meeting
    {
        public override bool Equals(System.Object obj)
        {
            if (obj == null)
            {
                return false;
            }

            Meeting meeting = obj as Meeting;
            if ((System.Object)meeting == null)
            {
                return false;
            }

            return (this.Name.Equals(meeting.Name))
                && (this.Location.Equals(meeting.Location))
                && (this.Time.Equals(meeting.Time))
                && (this.MaxNumberOfParticipants == meeting.MaxNumberOfParticipants);
        }

        public bool Equals(Meeting meeting)
        {
            if ((object)meeting == null)
            {
                return false;
            }

            return (this.Name.Equals(meeting.Name))
                && (this.Location.Equals(meeting.Location))
                && (this.Time.Equals(meeting.Time))
                && (this.MaxNumberOfParticipants == meeting.MaxNumberOfParticipants);
        }

        public override int GetHashCode()
        {
            return this.Name.GetHashCode()
                + this.Location.GetHashCode() 
                + this.Time.GetHashCode() 
                + this.MaxNumberOfParticipants.GetHashCode();
        }

        public static bool operator ==(Meeting a, Meeting b)
        {
            if (System.Object.ReferenceEquals(a, b))
            {
                return true;
            }

            if (((object)a == null) || ((object)b == null))
            {
                return false;
            }

            return a.Name.Equals(b.Name)
                && a.Location.Equals(b.Location)
                && a.Time.Equals(b.Time)
                && a.MaxNumberOfParticipants == b.MaxNumberOfParticipants;
        }

        public static bool operator !=(Meeting a, Meeting b)
        {
            return !(a == b);
        }
    }
}