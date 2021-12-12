namespace TwikturesApi.Models;

public class User
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Url { get; set; }
    public string Username { get; set; }
    public string AvatarUrl { get; set; }
    public List<int> Following { get; set; }
    public List<int> Followers { get; set; }
    public static User FromUserV2(UserV2 twitterUser)
    {
        return new User
        {
            Id = twitterUser.Id,
            Name = twitterUser.Name,
            Username = twitterUser.Username,
            AvatarUrl = twitterUser.ProfileImageUrl,
            Url = twitterUser.Url
        };
    }
}
