using Cratebase.Domain.Relations;
using Cratebase.Domain.SharedKernel.Errors;
using Cratebase.Domain.SharedKernel.Ids;

namespace Cratebase.Domain.Tests.Relations;

public sealed class RelationTests
{
    [Fact]
    public void Artist_relation_rejects_self_relations_and_invalid_periods()
    {
        var artistId = ArtistId.New();

        DomainException selfException = Assert.Throws<DomainException>(
            () => ArtistRelation.Create(ArtistRelationId.New(), artistId, artistId, ArtistRelationType.Alias));
        DomainException periodException = Assert.Throws<DomainException>(
            () => ArtistRelationPeriod.FromYears(1990, 1989));

        Assert.Equal("artist_relation.self_relation", selfException.Code);
        Assert.Equal("relation_period.invalid_range", periodException.Code);
    }

    [Fact]
    public void Artist_relation_supports_optional_periods()
    {
        var relation = ArtistRelation.Create(
            ArtistRelationId.New(),
            ArtistId.New(),
            ArtistId.New(),
            ArtistRelationType.MemberOf,
            ArtistRelationPeriod.FromYears(1980, 1985));

        Assert.Equal(1980, relation.Period?.StartYear);
        Assert.Equal(1985, relation.Period?.EndYear);
    }

    [Fact]
    public void Track_relation_rejects_self_relations_and_carries_a_relation_type()
    {
        var trackId = TrackId.New();

        DomainException exception = Assert.Throws<DomainException>(
            () => TrackRelation.Create(TrackRelationId.New(), trackId, trackId, TrackRelationType.RemixOf));
        var relation = TrackRelation.Create(
            TrackRelationId.New(),
            TrackId.New(),
            TrackId.New(),
            TrackRelationType.VersionOf);

        Assert.Equal("track_relation.self_relation", exception.Code);
        Assert.Equal(TrackRelationType.VersionOf, relation.RelationType);
    }

    [Fact]
    public void Relation_type_codes_are_cached_and_normalized()
    {
        var artistRelationType = ArtistRelationType.FromCode(" Alias ");
        var trackRelationType = TrackRelationType.FromCode(" Remix_Of ");

        Assert.Same(ArtistRelationType.MemberOf, ArtistRelationType.MemberOf);
        Assert.Same(TrackRelationType.VersionOf, TrackRelationType.VersionOf);
        Assert.Equal("alias", artistRelationType.Code);
        Assert.Equal("remix_of", trackRelationType.Code);
    }
}
