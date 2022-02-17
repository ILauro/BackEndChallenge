namespace BackEndChallenge.Models
{
    public class Logs
    {
        public string Id { get; set; }
        public DateTime Date { get; set; }
        public List<Crypto> Crypto { get; set; }
    }
}
