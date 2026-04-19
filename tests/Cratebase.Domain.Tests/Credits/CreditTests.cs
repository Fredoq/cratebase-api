using Cratebase.Domain.Catalog;
using Cratebase.Domain.Credits;
using Cratebase.Domain.SharedKernel.Ids;

namespace Cratebase.Domain.Tests.Credits;

public sealed class CreditTests
{
    [Fact]
    public void Credit_links_an_artist_contributor_to_a_release_or_track_target_through_a_role()
    {
        var person = Person.Create(PersonId.New(), "Arthur Baker");
        var group = Group.Create(GroupId.New(), "New Order");
        var releaseCredit = Credit.Create(
            CreditId.New(),
            CreditContributor.FromArtist(group),
            CreditTarget.ForRelease(ReleaseId.New()),
            CreditRole.MainArtist);
        var trackCredit = Credit.Create(
            CreditId.New(),
            CreditContributor.FromArtist(person),
            CreditTarget.ForTrack(TrackId.New()),
            CreditRole.Producer);

        Assert.True(releaseCredit.Target.IsRelease);
        Assert.Equal(group.ArtistId, releaseCredit.Contributor.ArtistId);
        Assert.True(trackCredit.Target.IsTrack);
        Assert.Equal(person.ArtistId, trackCredit.Contributor.ArtistId);
        Assert.Equal(CreditRole.Producer, trackCredit.Role);
    }
}
