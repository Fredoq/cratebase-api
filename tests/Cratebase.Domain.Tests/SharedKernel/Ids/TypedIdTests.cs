using System.Reflection;
using Cratebase.Domain.SharedKernel.Ids;

namespace Cratebase.Domain.Tests.SharedKernel.Ids;

public sealed class TypedIdTests
{
    public static TheoryData<Guid> NewIds()
    {
        var data = new TheoryData<Guid>();
        IEnumerable<Type> idTypes = typeof(ArtistId).Assembly
            .GetTypes()
            .Where(type => type is { IsPublic: true, IsValueType: true } && type.Name.EndsWith("Id", StringComparison.Ordinal))
            .OrderBy(type => type.Name);

        foreach (Type idType in idTypes)
        {
            MethodInfo? newMethod = idType.GetMethod(nameof(ArtistId.New), BindingFlags.Public | BindingFlags.Static, []);
            PropertyInfo? valueProperty = idType.GetProperty(nameof(ArtistId.Value), BindingFlags.Public | BindingFlags.Instance);

            if (newMethod is null || valueProperty?.PropertyType != typeof(Guid))
            {
                continue;
            }

            object id = newMethod.Invoke(null, null) ?? throw new InvalidOperationException($"Typed ID {idType.Name}.New returned null.");
            data.Add((Guid)valueProperty.GetValue(id)!);
        }

        return data;
    }

    [Theory]
    [MemberData(nameof(NewIds))]
    public void New_typed_ids_use_version_seven_guids(Guid value)
    {
        Assert.Equal(7, value.Version);
    }
}
