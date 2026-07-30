namespace Lab15_StudentPortalWeb.Services
{
    public interface IAhmedStampService
    {
        string Stamp { get; }
        string Owner { get; }
    }

    public class AhmedStampService : IAhmedStampService
    {
        public string Owner { get; }

        public string Stamp { get; }

        public AhmedStampService()
        {
            Owner = "Ahmed Sameh";
            Stamp = Guid.NewGuid().ToString().Substring(0, 8);
        }
    }
}
