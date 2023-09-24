using System;

namespace Yapoml.Selenium.Components.Conditions.Generic
{
    /// <summary>
    /// Defines a set of numeric conditions for a given type of number.
    /// </summary>
    /// <typeparam name="TConditions">The type of the conditions.</typeparam>
    /// <typeparam name="TNumber">The type of the number.</typeparam>
    public interface INumericConditions<TConditions, TNumber> where TNumber : struct, IComparable<TNumber>
    {
        /// <summary>
        /// Checks if the actual value is equal to the expected value.
        /// </summary>
        /// <param name="value">The expected value.</param> 
        /// <param name="timeout">The optional timeout for the check.</param>
        /// <returns>The conditions object for further chaining.</returns>
        TConditions Is(TNumber value, TimeSpan? timeout = default);

        /// <summary>
        /// Checks if the actual value is not equal to the expected value.
        /// </summary>
        /// <param name="value">The expected value.</param>
        /// <param name="timeout">The optional timeout for the check.</param>
        /// <returns>The conditions object for further chaining.</returns>
        TConditions IsNot(TNumber value, TimeSpan? timeout = default);

        /// <summary>
        /// Checks if the actual value is greater than the expected value.
        /// </summary>
        /// <param name="value">The expected value.</param>
        /// <param name="timeout">The optional timeout for the check.</param>
        /// <returns>The conditions object for further chaining.</returns>
        TConditions IsGreaterThan(TNumber value, TimeSpan? timeout = default);

        /// <summary>
        /// Checks if the actual value is equal to or greater than the expected value.
        /// </summary>
        /// <param name="value">The expected value.</param>
        /// <param name="timeout">The optional timeout for the check.</param>
        /// <returns>The conditions object for further chaining.</returns>
        TConditions AtLeast(TNumber value, TimeSpan? timeout = default);

        /// <summary>
        /// Checks if the actual value is less than the expected value.
        /// </summary>
        /// <param name="value">The expected value.</param>
        /// <param name="timeout">The optional timeout for the check.</param>
        /// <returns>The conditions object for further chaining.</returns>
        TConditions IsLessThan(TNumber value, TimeSpan? timeout = default);

        /// <summary>
        /// Checks if the actual value is equal to or less than the expected value.
        /// </summary>
        /// <param name="value">The expected value.</param>
        /// <param name="timeout">The optional timeout for the check.</param>
        /// <returns>The conditions object for further chaining.</returns>
        TConditions AtMost(TNumber value, TimeSpan? timeout = default);
    }
}