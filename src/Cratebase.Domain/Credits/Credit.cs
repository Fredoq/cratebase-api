using Cratebase.Domain.SharedKernel.Ids;
using Cratebase.Domain.SharedKernel.Interfaces;

namespace Cratebase.Domain.Credits;

public sealed class Credit : IEntity<CreditId>
{
    private Credit(CreditId id, CreditContributor contributor, CreditTarget target, CreditRole role)
    {
        Id = id;
        Contributor = contributor;
        Target = target;
        Role = role;
    }

    public CreditId Id { get; }

    public CreditContributor Contributor { get; }

    public CreditTarget Target { get; }

    public CreditRole Role { get; }

    public static Credit Create(CreditId id, CreditContributor contributor, CreditTarget target, CreditRole role)
    {
        ArgumentNullException.ThrowIfNull(contributor);
        ArgumentNullException.ThrowIfNull(target);

        return new Credit(id, contributor, target, role);
    }
}
