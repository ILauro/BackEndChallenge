namespace BackEndChallenge.Models
{
    public class ConversionResponse
    {
        public bool success { get; set; }
        public string terms { get; set; }
        public string privacy { get; set; }
        public int timestamp { get; set; }
        public string source { get; set; }
        public Quotes quotes { get; set; }
    }
    public class Quotes
    {
        public double USDEUR { get; set; }
        public double USDGBP { get; set; }
        public double USDCAD { get; set; }
        public double USDPLN { get; set; }
    }
}
