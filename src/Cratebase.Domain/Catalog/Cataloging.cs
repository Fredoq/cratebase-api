namespace Cratebase.Domain.Catalog;

public sealed record Cataloging
{
    private Cataloging(IReadOnlyList<Genre> genres, IReadOnlyList<Tag> tags)
    {
        Genres = genres;
        Tags = tags;
    }

    public IReadOnlyList<Genre> Genres { get; }

    public IReadOnlyList<Tag> Tags { get; }

    public static Cataloging Empty { get; } = new([], []);

    public Cataloging WithGenre(Genre genre)
    {
        ArgumentNullException.ThrowIfNull(genre);

        return Genres.Contains(genre) ? this : new Cataloging([.. Genres, genre], [.. Tags]);
    }

    public Cataloging WithTag(Tag tag)
    {
        ArgumentNullException.ThrowIfNull(tag);

        return Tags.Contains(tag) ? this : new Cataloging([.. Genres], [.. Tags, tag]);
    }
}
