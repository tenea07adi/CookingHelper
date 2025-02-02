namespace Core.Ports.Driving
{
    public interface IInfrastructureUtilityService
    {
        public string Init(string defaultUserEmail, string defaultUserName);
        public void RunDatabaseUpdate();
    }
}
