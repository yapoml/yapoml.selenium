using AgileObjects.ReadableExpressions;
using System.Linq.Expressions;

namespace Yapoml.Selenium.Extensions
{
    internal static class ExpressionExtensions
    {
        public static string ToReadable(this LambdaExpression expression)
        {
            return expression.Body.ToReadableString(c => c.ShowCapturedValues);
        }
    }
}
