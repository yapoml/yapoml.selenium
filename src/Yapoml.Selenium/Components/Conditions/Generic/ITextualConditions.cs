using System;
using System.Text.RegularExpressions;

namespace Yapoml.Selenium.Components.Conditions.Generic
{
    public interface ITextualConditions<TConditions>
    {
        TConditions Is(string value, TimeSpan? timeout = default);

        TConditions Is(string value, StringComparison comparisonType, TimeSpan? timeout = default);

        TConditions IsNot(string value, TimeSpan? timeout = default);

        TConditions IsNot(string value, StringComparison comparisonType, TimeSpan? timeout = default);

        TConditions IsEmpty(TimeSpan? timeout = default);

        TConditions IsNotEmpty(TimeSpan? timeout = default);

        TConditions StartsWith(string value, TimeSpan? timeout = default);

        TConditions StartsWith(string value, StringComparison comparisonType, TimeSpan? timeout = default);

        TConditions DoesNotStartWith(string value, TimeSpan? timeout = default);

        TConditions DoesNotStartWith(string value, StringComparison comparisonType, TimeSpan? timeout = default);

        TConditions EndsWith(string value, TimeSpan? timeout = default);

        TConditions EndsWith(string value, StringComparison comparisonType, TimeSpan? timeout = default);

        TConditions DoesNotEndWith(string value, TimeSpan? timeout = default);

        TConditions DoesNotEndWith(string value, StringComparison comparisonType, TimeSpan? timeout = default);

        TConditions Contains(string value, TimeSpan? timeout = default);

        TConditions Contains(string value, StringComparison comparisonType, TimeSpan? timeout = default);

        TConditions DoesNotContain(string value, TimeSpan? timeout = default);

        TConditions DoesNotContain(string value, StringComparison comparisonType, TimeSpan? timeout = default);

        TConditions Matches(Regex regex, TimeSpan? timeout = default);

        TConditions DoesNotMatch(Regex regex, TimeSpan? timeout = default);
    }
}
