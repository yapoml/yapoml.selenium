using System;
using System.Text.RegularExpressions;

namespace Yapoml.Selenium.Components.Conditions.Generic
{
    public interface IStringConditions<TConditions>
    {
        TConditions Is(string value);

        TConditions Is(string value, TimeSpan timeout);

        TConditions Is(string value, StringComparison comparisonType);

        TConditions Is(string value, StringComparison comparisonType, TimeSpan timeout);

        TConditions IsNot(string value);

        TConditions IsNot(string value, TimeSpan timeout);

        TConditions IsNot(string value, StringComparison comparisonType);

        TConditions IsNot(string value, StringComparison comparisonType, TimeSpan timeout);

        TConditions IsEmpty();

        TConditions IsEmpty(TimeSpan timeout);

        TConditions IsNotEmpty();

        TConditions IsNotEmpty(TimeSpan timeout);

        TConditions StartsWith(string value);

        TConditions StartsWith(string value, TimeSpan timeout);

        TConditions StartsWith(string value, StringComparison comparisonType);

        TConditions StartsWith(string value, StringComparison comparisonType, TimeSpan timeout);

        TConditions DoesNotStartWith(string value);

        TConditions DoesNotStartWith(string value, TimeSpan timeout);

        TConditions DoesNotStartWith(string value, StringComparison comparisonType);

        TConditions DoesNotStartWith(string value, StringComparison comparisonType, TimeSpan timeout);

        TConditions EndsWith(string value);

        TConditions EndsWith(string value, TimeSpan timeout);

        TConditions EndsWith(string value, StringComparison comparisonType);

        TConditions EndsWith(string value, StringComparison comparisonType, TimeSpan timeout);

        TConditions DoesNotEndWith(string value);

        TConditions DoesNotEndWith(string value, TimeSpan timeout);

        TConditions DoesNotEndWith(string value, StringComparison comparisonType);

        TConditions DoesNotEndWith(string value, StringComparison comparisonType, TimeSpan timeout);

        TConditions Contains(string value);

        TConditions Contains(string value, TimeSpan timeout);

        TConditions Contains(string value, StringComparison comparisonType);

        TConditions Contains(string value, StringComparison comparisonType, TimeSpan timeout);

        TConditions DoesNotContain(string value);

        TConditions DoesNotContain(string value, TimeSpan timeout);

        TConditions DoesNotContain(string value, StringComparison comparisonType);

        TConditions DoesNotContain(string value, StringComparison comparisonType, TimeSpan timeout);

        TConditions Matches(Regex regex);

        TConditions Matches(Regex regex, TimeSpan timeout);

        TConditions DoesNotMatch(Regex regex);

        TConditions DoesNotMatch(Regex regex, TimeSpan timeout);
    }
}
