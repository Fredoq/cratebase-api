using Cratebase.Domain.Artists;
using Cratebase.Domain.Shared;

namespace Cratebase.Domain.Tests.Artists;

public sealed class ArtistTests
{
    [Fact(DisplayName = "Artist creation trims the display name and keeps the selected kind")]
    public void Artist_creation_trims_the_display_name_and_keeps_the_selected_kind()
    {
        var id = EntityId.New();

        var artist = Artist.Create(id, "  Aphex Twin  ", ArtistKind.Project);

        Assert.Equal(id, artist.Id);
        Assert.Equal("Aphex Twin", artist.DisplayName);
        Assert.Equal(ArtistKind.Project, artist.Kind);
    }

    [Theory(DisplayName = "Artist creation rejects a blank display name")]
    [InlineData("")]
    [InlineData("   ")]
    public void Artist_creation_rejects_a_blank_display_name(string displayName)
    {
        var id = EntityId.New();

        ArgumentException exception = Assert.Throws<ArgumentException>(() => Artist.Create(id, displayName, ArtistKind.Person));

        Assert.Equal("displayName", exception.ParamName);
    }
}
