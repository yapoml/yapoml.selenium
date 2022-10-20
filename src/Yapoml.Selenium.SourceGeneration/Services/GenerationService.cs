using Yapoml.Framework.Workspace;

namespace Yapoml.Selenium.SourceGeneration.Services
{
    internal class GenerationService
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

        public static string GetComponentReturnType(ComponentContext component)
        {
            if (component.ReferencedComponent == null)
            {
                if (component.IsPlural)
                {
                    return $"{component.Namespace}.{component.SingularName}Component";
                }
                else
                {
                    return $"{component.Namespace}.{component.Name}Component";
                }
            }
            else
            {
                if (component.ReferencedComponent.IsPlural)
                {
                    return $"{component.ReferencedComponent.Namespace}.{component.ReferencedComponent.SingularName}Component";
                }
                else
                {
                    return $"{component.ReferencedComponent.Namespace}.{component.ReferencedComponent.Name}Component";
                }
            }
        }
    }
}