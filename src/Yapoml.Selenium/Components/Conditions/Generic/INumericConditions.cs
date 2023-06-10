using System;

namespace Yapoml.Selenium.Components.Conditions.Generic
{
    public interface INumericConditions<TConditions, TNumber> where TNumber : struct, IComparable<TNumber>
    {
        TConditions Is(TNumber value);
        TConditions Is(TNumber value, TimeSpan timeout);
        TConditions IsGreaterThan(TNumber value);
        TConditions IsGreaterThan(TNumber value, TimeSpan timeout);
        TConditions IsLessThan(TNumber value);
        TConditions IsLessThan(TNumber value, TimeSpan timeout);
        TConditions IsNot(TNumber value);
        TConditions IsNot(TNumber value, TimeSpan timeout);
    }
}