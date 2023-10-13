using System;
using System.Text.RegularExpressions;

namespace Yapoml.Selenium.Components.Conditions.Generic
{
    /// <summary>
    /// Defines a set of textual conditions for a given type of text.
    /// </summary>
    /// <typeparam name="TSelf">The type of the conditions.</typeparam>
    public interface ITextualConditions<TSelf>
    {
        /// <summary>
        /// Checks if the actual text is equal to the expected text using the default string comparison.
        /// </summary>
        /// <param name="value">The expected text.</param>
        /// <param name="timeout">The optional timeout for the check.</param>
        /// <returns>The conditions object for further chaining.</returns>
        TSelf Is(string value, TimeSpan? timeout = default);

        /// <summary>
        /// Checks if the actual text is equal to the expected text using the specified string comparison.
        /// </summary>
        /// <param name="value">The expected text.</param>
        /// <param name="comparisonType">The string comparison type.</param>
        /// <param name="timeout">The optional timeout for the check.</param>
        /// <returns>The conditions object for further chaining.</returns>
        TSelf Is(string value, StringComparison comparisonType, TimeSpan? timeout = default);

        /// <summary>
        /// Checks if the actual text is not equal to the expected text using the default string comparison.
        /// </summary>
        /// <param name="value">The expected text.</param>
        /// <param name="timeout">The optional timeout for the check.</param>
        /// <returns>The conditions object for further chaining.</returns>
        TSelf IsNot(string value, TimeSpan? timeout = default);

        /// <summary>
        /// Checks if the actual text is not equal to the expected text using the specified string comparison.
        /// </summary>
        /// <param name="value">The expected text.</param>
        /// <param name="comparisonType">The string comparison type.</param>
        /// <param name="timeout">The optional timeout for the check.</param>
        /// <returns>The conditions object for further chaining.</returns>
        TSelf IsNot(string value, StringComparison comparisonType, TimeSpan? timeout = default);

        /// <summary>
        /// Checks if the actual text is empty.
        /// </summary>
        /// <param name="timeout">The optional timeout for the check.</param>
        /// <returns>The conditions object for further chaining.</returns>
        TSelf IsEmpty(TimeSpan? timeout = default);

        /// <summary>
        /// Checks if the actual text is not empty.
        /// </summary>
        /// <param name="timeout">The optional timeout for the check.</param>
        /// <returns>The conditions object for further chaining.</returns>
        TSelf IsNotEmpty(TimeSpan? timeout = default);

        /// <summary>
        /// Checks if the actual text starts with the expected text using the default string comparison.
        /// </summary>
        /// <param name="value">The expected text.</param>
        /// <param name="timeout">The optional timeout for the check.</param>
        /// <returns>The conditions object for further chaining.</returns>
        TSelf StartsWith(string value, TimeSpan? timeout = default);

        /// <summary>
        /// Checks if the actual text starts with the expected text using the specified string comparison.
        /// </summary>
        /// <param name="value">The expected text.</param>
        /// <param name="comparisonType">The string comparison type.</param>
        /// <param name="timeout">The optional timeout for the check.</param>
        /// <returns>The conditions object for further chaining.</returns>
        TSelf StartsWith(string value, StringComparison comparisonType, TimeSpan? timeout = default);

        /// <summary>
        /// Checks if the actual text does not start with the expected text using the default string comparison.
        /// </summary>
        /// <param name="value">The expected text.</param>
        /// <param name="timeout">The optional timeout for the check.</param>
        /// <returns>The conditions object for further chaining.</returns>
        TSelf DoesNotStartWith(string value, TimeSpan? timeout = default);

        /// <summary>
        /// Checks if the actual text does not start with the expected text using the specified string comparison.
        /// </summary>
        /// <param name="value">The expected text.</param>
        /// <param name="comparisonType">The string comparison type.</param>
        /// <param name="timeout">The optional timeout for the check.</param>
        /// <returns>The conditions object for further chaining.</returns>
        TSelf DoesNotStartWith(string value, StringComparison comparisonType, TimeSpan? timeout = default);

        /// <summary>
        /// Checks if the actual text ends with the expected text using the default string comparison.
        /// </summary>
        /// <param name="value">The expected text.</param>
        /// <param name="timeout">The optional timeout for the check.</param>
        /// <returns>The conditions object for further chaining.</returns>
        TSelf EndsWith(string value, TimeSpan? timeout = default);

        /// <summary>
        /// Checks if the actual text ends with the expected text using the specified string comparison.
        /// </summary>
        /// <param name="value">The expected text.</param>
        /// <param name="comparisonType">The string comparison type.</param>
        /// <param name="timeout">The optional timeout for the check.</param>
        /// <returns>The conditions object for further chaining.</returns>
        TSelf EndsWith(string value, StringComparison comparisonType, TimeSpan? timeout = default);

        /// <summary>
        /// Checks if the actual text does not end with the expected text using the default string comparison.
        /// </summary>
        /// <param name="value">The expected text.</param>
        /// <param name="timeout">The optional timeout for the check.</param>
        /// <returns>The conditions object for further chaining.</returns>
        TSelf DoesNotEndWith(string value, TimeSpan? timeout = default);

        /// <summary>
        /// Checks if the actual text does not end with the expected text using the specified string comparison.
        /// </summary>
        /// <param name="value">The expected text.</param>
        /// <param name="comparisonType">The string comparison type.</param>
        /// <param name="timeout">The optional timeout for the check.</param>
        /// <returns>The conditions object for further chaining.</returns>
        TSelf DoesNotEndWith(string value, StringComparison comparisonType, TimeSpan? timeout = default);

        /// <summary>
        /// Checks if the actual text contains the expected text using the default string comparison.
        /// </summary>
        /// <param name="value">The expected text.</param>
        /// <param name="timeout">The optional timeout for the check.</param>
        /// <returns>The conditions object for further chaining.</returns>
        TSelf Contains(string value, TimeSpan? timeout = default);

        /// <summary>
        /// Checks if the actual text contains the expected text using the specified string comparison.
        /// </summary>
        /// <param name="value">The expected text.</param>
        /// <param name="comparisonType">The string comparison type.</param>
        /// <param name="timeout">The optional timeout for the check.</param>
        /// <returns>The conditions object for further chaining.</returns>
        TSelf Contains(string value, StringComparison comparisonType, TimeSpan? timeout = default);

        /// <summary>
        /// Checks if the actual text does not contain the expected text using the default string comparison.
        /// </summary>
        /// <param name="value">The expected text.</param>
        /// <param name="timeout">The optional timeout for the check.</param>
        /// <returns>The conditions object for further chaining.</returns>
        TSelf DoesNotContain(string value, TimeSpan? timeout = default);

        /// <summary>
        /// Checks if the actual text does not contain the expected text using the specified string comparison.
        /// </summary>
        /// <param name="value">The expected text.</param>
        /// <param name="comparisonType">The string comparison type.</param>
        /// <param name="timeout">The optional timeout for the check.</param>
        /// <returns>The conditions object for further chaining.</returns>
        TSelf DoesNotContain(string value, StringComparison comparisonType, TimeSpan? timeout = default);

        /// <summary>
        /// Checks if the actual text matches the expected regular expression.
        /// </summary>
        /// <param name="regex">The expected regular expression.</param>
        /// <param name="timeout">The optional timeout for the check.</param>
        /// <returns>The conditions object for further chaining.</returns>
        TSelf Matches(Regex regex, TimeSpan? timeout = default);

        /// <summary>
        /// Checks if the actual text does not match the expected regular expression.
        /// </summary>
        /// <param name="regex">The expected regular expression.</param>
        /// <param name="timeout">The optional timeout for the check.</param>
        /// <returns>The conditions object for further chaining.</returns>
        TSelf DoesNotMatch(Regex regex, TimeSpan? timeout = default);
    }
}
