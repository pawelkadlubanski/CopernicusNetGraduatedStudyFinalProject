namespace MeetingService
{
    public partial class User
    {
        public override bool Equals(System.Object obj)
        {
            if (obj == null)
            {
                return false;
            }

            User user = obj as User;
            if ((System.Object)user == null)
            {
                return false;
            }

            return (this.Name.Equals(user.Name))
                && (this.Surname.Equals(user.Surname))
                && (this.Login.Equals(user.Login))
                && (this.Email.Equals(user.Email));
        }

        public bool Equals(User user)
        {
            if ((object)user == null)
            {
                return false;
            }

            return (this.Name.Equals(user.Name))
                && (this.Surname.Equals(user.Surname))
                && (this.Login.Equals(user.Login))
                && (this.Email.Equals(user.Email));
        }

        public override int GetHashCode()
        {
            return this.Name.GetHashCode()
                + this.Surname.GetHashCode()
                + this.Login.GetHashCode()
                + this.Email.GetHashCode();
        }

        public static bool operator ==(User a, User b)
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
                && a.Surname.Equals(b.Surname)
                && a.Login.Equals(b.Login)
                && a.Email == b.Email;
        }

        public static bool operator !=(User a, User b)
        {
            return !(a == b);
        }

    }
}