namespace Application.Interfaces;

internal interface IApplicationDbContext
{
    DbSet<TweetEntity> Tweets { get; set; }
    DbSet<TwitterUserEntity> TwitterUsers { get; set; }
    DbSet<TwiktureUserEntity> TwiktureUsers { get; set; }

    Task<uint> SaveAsync();
}
