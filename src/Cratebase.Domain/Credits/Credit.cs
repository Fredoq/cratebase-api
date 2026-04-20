using Cratebase.Domain.SharedKernel.Ids;
using Cratebase.Domain.SharedKernel.Interfaces;

namespace Cratebase.Domain.Credits;

public sealed class Credit : IEntity<CreditId>
{
    public required CreditId Id { get; init; }

    public required CreditContributor Contributor { get; init; }

    public required CreditTarget Target { get; init; }

    public required CreditRole Role { get; init; }

    public static Credit Create(CreditId id, CreditContributor contributor, CreditTarget target, CreditRole role)
    {
        ArgumentNullException.ThrowIfNull(contributor);
        ArgumentNullException.ThrowIfNull(target);
        ArgumentNullException.ThrowIfNull(role);

        return new Credit
        {
            Id = id,
            Contributor = contributor,
            Target = target,
            Role = role
        };
    }
}
