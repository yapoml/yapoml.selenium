namespace Yapoml.Selenium.Services
{
    public class ByMethodDetector
    {
        public static bool IsXpath(string expression)
        {
            var isXpath = true;

            try
            {
                System.Xml.XPath.XPathExpression.Compile(expression);
            }
            catch { isXpath = false; }

            return isXpath;
        }
    }
}