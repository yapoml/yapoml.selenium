using Scriban.Runtime;
using System.Collections.Generic;
using System.Linq;
using Yapoml.Framework.Workspace;

namespace Yapoml.Selenium.SourceGeneration.Services
{
    internal class GenerationService : ScriptObject
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

        private static Dictionary<ComponentContext, string> _returnTypesCache = new Dictionary<ComponentContext, string>();

        public static string GetComponentReturnType(ComponentContext component)
        {
            if (_returnTypesCache.TryGetValue(component, out var cachedRetType))
            {
                return cachedRetType;
            }
            else
            {
                string retType;

                if (component.ReferencedComponent == null)
                {
                    retType = $"global::{component.Namespace}.{component.SingularName}Component";
                }
                else
                {
                    retType = $"global::{component.ReferencedComponent.Namespace}.{component.ReferencedComponent.SingularName}Component";
                }

                _returnTypesCache[component] = retType;

                return retType;
            }
        }

        public static string GetPageClassName(ScriptObject page)
        {
            var name = page.GetSafeValue<string>("name");

            return GetPageClassName(name);
        }

        public static string GetPageClassNameForPage(PageContext page)
        {
            return GetPageClassName(page.Name);
        }

        private static string GetPageClassName(string name)
        {
            if (name.EndsWith("Page"))
            {
                return name;
            }
            else
            {
                return $"{name}Page";
            }
        }

        public static bool HasUserDefinedBaseComponent(ScriptObject space)
        {
            if (space.TryGetValue("components", out object oComponents))
            {
                var components = oComponents as IList<ComponentContext>;

                return components.FirstOrDefault(c => c.Name.Equals("base", System.StringComparison.InvariantCultureIgnoreCase)) != null;
            }
            else
            {
                return false;
            }
        }

        public static bool HasUserDefinedBasePage(ScriptObject space)
        {
            if (space.TryGetValue("pages", out object oPages))
            {
                var pages = oPages as IList<PageContext>;

                return pages.FirstOrDefault(
                    c => c.Name.Equals("Base", System.StringComparison.InvariantCultureIgnoreCase)
                    || c.Name.Equals("BasePage", System.StringComparison.InvariantCultureIgnoreCase)) != null;
            }
            else
            {
                return false;
            }
        }

        public static string GetPageAccessorName(string pageName)
        {
            if (pageName.EndsWith("page", System.StringComparison.OrdinalIgnoreCase))
            {
                return pageName;
            }
            else
            {
                return pageName + "Page";
            }
        }
    }
}