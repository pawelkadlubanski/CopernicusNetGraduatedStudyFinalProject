namespace MeetingService
{
    public partial class Participation
    {
        public override bool Equals(System.Object obj)
        {
            if (obj == null)
            {
                return false;
            }

            Participation participation = obj as Participation;
            if ((System.Object)participation == null)
            {
                return false;
            }

            return (this.UserLogin.Equals(participation.UserLogin))
                && (this.MeetingName.Equals(participation.MeetingName));
        }

        public bool Equals(Participation participation)
        {
            if ((object)participation == null)
            {
                return false;
            }

            return (this.UserLogin.Equals(participation.UserLogin))
                && (this.MeetingName.Equals(participation.MeetingName));
        }

        public override int GetHashCode()
        {
            return this.UserLogin.GetHashCode()
                + this.MeetingName.GetHashCode();
        }

        public static bool operator ==(Participation a,Participation b)
        {
            if (System.Object.ReferenceEquals(a, b))
            {
                return true;
            }

            if (((object)a == null) || ((object)b == null))
            {
                return false;
            }

            return a.UserLogin.Equals(b.UserLogin)
                && a.MeetingName.Equals(b.MeetingName);
        }

        public static bool operator !=(Participation a, Participation b)
        {
            return !(a == b);
        }
    }
}