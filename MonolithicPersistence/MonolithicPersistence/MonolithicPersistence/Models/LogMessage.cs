namespace MonolithicPersistence.Models
{
    public class LogMessage
    {
        private static readonly Random Rand = new Random();

        public int ErrorNumber { get; set; }
        public string Message { get; set; }
        public DateTime LogTime { get; set; }

        public LogMessage()
        {
            Message = "My Log Message " + Rand.Next();
            LogTime = DateTime.UtcNow;
            ErrorNumber = Guid.NewGuid().GetHashCode();
        }
    }
}
