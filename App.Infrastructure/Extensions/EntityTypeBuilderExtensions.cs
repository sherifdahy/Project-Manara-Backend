using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Linq.Expressions;

namespace App.Infrastructure.Extensions;

public static class EntityTypeBuilderExtensions
{
    public static EntityTypeBuilder<TEntity> EnumInRange<TEntity, TEnum>(
        this EntityTypeBuilder<TEntity> builder,
        Expression<Func<TEntity, TEnum>> propertyExpression,
        string? constraintName = null)
        where TEntity : class
        where TEnum : struct, Enum
    {
        var enumType = typeof(TEnum);

        var values = string.Join(
            ", ",
            Enum.GetValues(enumType).Cast<object>().Select(v => Convert.ToInt32(v))
        );

        var memberExpression = propertyExpression.Body as MemberExpression
            ?? throw new InvalidOperationException("Expression must be a simple property access.");

        var propertyName = memberExpression.Member.Name;

        constraintName ??= $"CK_{typeof(TEntity).Name}_{propertyName}";

        builder.HasCheckConstraint(
            constraintName,
            $"[{propertyName}] IN ({values})"
        );

        return builder;
    }
}
