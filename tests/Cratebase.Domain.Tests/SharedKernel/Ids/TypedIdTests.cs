using Cratebase.Domain.SharedKernel.Ids;

namespace Cratebase.Domain.Tests.SharedKernel.Ids;

public sealed class TypedIdTests
{
    public static TheoryData<Guid> NewIds()
    {
        return
        [
            ArtistId.New().Value,
            ArtistRelationId.New().Value,
            CreditId.New().Value,
            GroupId.New().Value,
            LabelId.New().Value,
            OwnedItemId.New().Value,
            PersonId.New().Value,
            ReleaseId.New().Value,
            TrackId.New().Value,
            TrackRelationId.New().Value
        ];
    }

    [Theory]
    [MemberData(nameof(NewIds))]
    public void New_typed_ids_use_version_seven_guids(Guid value)
    {
        Assert.Equal(7, value.Version);
    }
}
