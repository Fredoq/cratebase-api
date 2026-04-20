using Cratebase.Domain.Catalog;
using Cratebase.Domain.Credits;
using Cratebase.Domain.SharedKernel.Errors;
using Cratebase.Domain.SharedKernel.Ids;

namespace Cratebase.Domain.Tests.Credits;

public sealed class CreditTests
{
    [Fact]
    public void Credit_links_an_artist_contributor_to_a_release_or_track_target_through_a_role()
    {
        var person = Person.Create(PersonId.New(), "Arthur Baker");
        var group = Group.Create(GroupId.New(), "New Order");
        var releaseId = ReleaseId.New();
        var trackId = TrackId.New();
        var releaseCredit = Credit.Create(
            CreditId.New(),
            CreditContributor.FromArtist(group),
            CreditTarget.ForRelease(releaseId),
            CreditRole.MainArtist);
        var trackCredit = Credit.Create(
            CreditId.New(),
            CreditContributor.FromArtist(person),
            CreditTarget.ForTrack(trackId),
            CreditRole.Producer);

        Assert.True(releaseCredit.Target.IsRelease);
        Assert.Equal(releaseId, releaseCredit.Target.ReleaseId);
        Assert.Null(releaseCredit.Target.TrackId);
        Assert.Equal(group.ArtistId, releaseCredit.Contributor.ArtistId);
        Assert.True(trackCredit.Target.IsTrack);
        Assert.Equal(trackId, trackCredit.Target.TrackId);
        Assert.Null(trackCredit.Target.ReleaseId);
        Assert.Equal(person.ArtistId, trackCredit.Contributor.ArtistId);
        Assert.Equal(CreditRole.Producer, trackCredit.Role);
    }

    [Fact]
    public void Credit_target_rejects_ambiguous_and_empty_targets()
    {
        DomainException ambiguous = Assert.Throws<DomainException>(
            () => CreditTarget.Create(ReleaseId.New(), TrackId.New()));
        DomainException empty = Assert.Throws<DomainException>(
            () => CreditTarget.Create(null, null));

        Assert.Equal("credit_target.ambiguous", ambiguous.Code);
        Assert.Equal("credit_target.empty", empty.Code);
    }

    [Fact]
    public void Credit_roles_are_cached_and_normalized()
    {
        var custom = CreditRole.FromCode(" Composer ");

        Assert.Same(CreditRole.Producer, CreditRole.Producer);
        Assert.Equal("composer", custom.Code);
    }
}
