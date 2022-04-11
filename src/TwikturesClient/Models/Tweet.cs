namespace TwikturesClient.Models
{
    public class Tweet
    {
        public long Id { get; set; }
        public string Text { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
        public List<string> ImageURIs { get; set; }
    }
}
