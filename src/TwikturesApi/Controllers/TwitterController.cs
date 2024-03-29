﻿using Tweetinvi.Parameters;

namespace TwikturesApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TwitterController : ControllerBase
{
    private readonly TwitterClient _twitterClient;

    public TwitterController()
    {
        _twitterClient = new TwitterClient(Environment.GetEnvironmentVariable("TWITTER_CONSUMER_KEY"),
            Environment.GetEnvironmentVariable("TWITTER_CONSUMER_SECRET"),
            Environment.GetEnvironmentVariable("TWITTER_BEARER_TOKEN"));
    }

    [HttpGet("user/{username}")]
    public async Task<ActionResult<User>> GetUser(string username)
    {
        var userResponse = await _twitterClient.UsersV2.GetUserByNameAsync(username);

        if (userResponse.Errors != null && userResponse.Errors.Any())
            return BadRequest();

        return Ok(Models.User.FromUserV2(userResponse.User));
    }

    [HttpGet("user/{username}/tweets")]
    public async Task<ActionResult<List<Tweet>>> GetUserTweets(string username, int pageSize = 100, long? oldestId = null, long? newestId = null)
    {
        var parameters = new GetUserTimelineParameters(username)
        {
            IncludeRetweets = false,
            IncludeContributorDetails = false,
            ExcludeReplies = true,
            IncludeEntities = true,
            TrimUser = true,
            PageSize = pageSize,
            SinceId = oldestId,
            MaxId = newestId
        };
        
        var userTweets = await _twitterClient.Timelines.GetUserTimelineAsync(parameters);

        if (userTweets.Count() == 0)
            return NotFound();

        var filteredTweets = userTweets.Where(tweet => tweet.Id != oldestId && tweet.Id != newestId && tweet.Media.Any(media => media.MediaType == "photo")).ToArray();

        return Ok(userTweets.Select(tweet => Tweet.FromITweet(tweet)));
    }
}
