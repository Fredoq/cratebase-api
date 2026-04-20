using Cratebase.Domain.SharedKernel.Validation;

namespace Cratebase.Domain.Collection;

public sealed record CassetteTape : IMedium
{
    private CassetteTape(string tapeType)
    {
        TapeType = tapeType;
    }

    public string TapeType { get; }

    public string Description => TapeType;

    public static CassetteTape Create(string tapeType)
    {
        return new CassetteTape(Guard.RequiredText(tapeType, nameof(tapeType), "cassette_tape.type_required"));
    }
}
