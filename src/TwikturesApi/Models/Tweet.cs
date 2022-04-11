using Tweetinvi.Models;

namespace TwikturesApi.Models
{
    public class Tweet
    {
        public long Id { get; set; }
        public string Text { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
        public List<string> ImageURIs { get; set; }

        public static Tweet FromITweet (ITweet tweet)
        {
            return new Tweet
            {
                Id = tweet.Id,
                Text = tweet.Text,
                CreatedDate = tweet.CreatedAt,
                ImageURIs = tweet.Media.Where(media => media.MediaType == "photo").Select(media => media.MediaURLHttps).ToList()
            };
        }
    }
}
