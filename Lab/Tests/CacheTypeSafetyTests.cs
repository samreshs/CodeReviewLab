using Lab.CacheTypeSafety;
using Xunit;

namespace Lab.Tests;

public class CacheTypeSafetyTests
{
    private sealed record User(int Id);

    [Fact]
    public void BrokenTupleCache_Throws_When_Requesting_WrongType()
    {
        var cache = new BrokenTupleCache();
        cache.Set("u1", new User(1));

        Assert.Throws<InvalidCastException>(() =>
        {
            cache.Get<string>("u1");
        });
    }
}