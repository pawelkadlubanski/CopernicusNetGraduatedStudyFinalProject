namespace MeetingService
{
    public class ParticipationFactory
    {
        public Participation create(string login, string name)
        {
            var participation = new Participation();
            participation.MeetingName = name;
            participation.UserLogin = login;
            return participation;
        }
            
    }
}