using System;
using Yapoml.Framework.Logging;

namespace Yapoml.Selenium.Components.Conditions.Generic
{
    public class TextualLengthConditons<TConditions> : NumericConditions<TConditions, int>
    {
        private readonly Func<string> _getTextualValueFunc;

        private string _lastTextualValue;

        public TextualLengthConditons(TConditions conditions, TimeSpan timeout, TimeSpan pollingInterval, Func<string> getTextualValueFunc, string subject, ILogger logger)
        : base(conditions, timeout, pollingInterval, subject, logger)
        {
            _getTextualValueFunc = getTextualValueFunc;
        }

        protected override Func<int?> FetchValueFunc => () =>
        {
            _lastTextualValue = _getTextualValueFunc();

            return _lastTextualValue.Length;
        };

        protected override string GetIsError(int? latestValue, int expectedValue)
        {
            return $"The {_subject} remains {latestValue} characters long, which is still not the {expectedValue} characters expected.\n  it was: {_lastTextualValue}";
        }

        protected override string GetIsNotError(int? latestValue, int expectedValue)
        {
            return $"The {_subject} remains {latestValue} characters long.\n  it was: {_lastTextualValue}";
        }

        protected override string GetIsGreaterThanError(int? latestValue, int expectedValue)
        {
            return $"The {_subject} remains {latestValue} characters long, which is still not greater than the {expectedValue} characters expected.\n  it was: {_lastTextualValue}";
        }

        protected override string AtLeast(int? latestValue, int expectedValue)
        {
            return $"The {_subject} remains {latestValue} characters long, which is still not equal to or greater than the {expectedValue} characters expected.\n  it was: {_lastTextualValue}";
        }

        protected override string GetIsLessThanError(int? latestValue, int expectedValue)
        {
            return $"The {_subject} remains {latestValue} characters long, which is still not less than the {expectedValue} characters expected.\n  it was: {_lastTextualValue}";
        }

        protected override string GetAtMostError(int? latestValue, int expectedValue)
        {
            return $"The {_subject} remains {latestValue} characters long, which is still not equal to or less than the {expectedValue} characters expected.\n  it was: {_lastTextualValue}";
        }
    }
}
