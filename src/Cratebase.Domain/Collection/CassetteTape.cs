using Cratebase.Domain.SharedKernel.Validation;

namespace Cratebase.Domain.Collection;

public sealed record CassetteTape : Medium
{
    public required string TapeType { get; init; }

    public override string Description => TapeType;

    public static CassetteTape Create(string tapeType)
    {
        return new CassetteTape
        {
            TapeType = Guard.RequiredText(tapeType, nameof(tapeType), "cassette_tape.type_required")
        };
    }
}
