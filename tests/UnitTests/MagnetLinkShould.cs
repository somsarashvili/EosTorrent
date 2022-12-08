using EosTorrent;
using NUnit.Framework;

namespace UnitTests;

public class MagnetLinkShould
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void HaveHash()
    {
        var magnet = new MagnetLink("magnet:?xt=urn:btih:e3811b9539cacff680e418124272177c47477157");
        Assert.That(magnet.Hash, Is.Not.Empty);
        Assert.That(magnet.DisplayName, Is.Null);
        Assert.That(magnet.Trackers, Is.Empty);
    }

    [Test]
    public void HaveAllData()
    {
        var magnet = new MagnetLink(
            "magnet:?xt=urn:btih:5b0b15ca5e5f793c7f9a9dccc7a4ab4bcfc7d605&dn=my-file.txt&tr=udp%3A%2F%2Ftracker.example.org%3A8080&tr=udp%3A%2F%2Ftracker.example.com%3A8080&xl=1024&as=http%3A%2F%2Fdownload.example.org%2Fmy-file.txt");
        Assert.That(magnet.Hash, Is.EqualTo("5b0b15ca5e5f793c7f9a9dccc7a4ab4bcfc7d605"));
        Assert.That(magnet.DisplayName, Is.EqualTo("my-file.txt"));
        Assert.That(magnet.Trackers, Has.Exactly(1).Matches<string>(x => x == "udp://tracker.example.org:8080"));
        Assert.That(magnet.Trackers, Has.Exactly(1).Matches<string>(x => x == "udp://tracker.example.com:8080"));
    }

    [Test]
    public void ThrowArgumentException()
    {
        Assert.Throws<ArgumentException>(() => new MagnetLink(""));
    }
}