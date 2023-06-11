using System;

namespace Yapoml.Selenium.Components.Conditions.Generic
{
    public interface INumericConditions<TConditions, TNumber> where TNumber : struct, IComparable<TNumber>
    {
        TConditions Is(TNumber value, TimeSpan? timeout = default);
        TConditions IsGreaterThan(TNumber value, TimeSpan? timeout = default);
        TConditions IsLessThan(TNumber value, TimeSpan? timeout = default);
        TConditions IsNot(TNumber value, TimeSpan? timeout = default);
    }
}