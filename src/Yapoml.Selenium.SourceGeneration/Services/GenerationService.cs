using Scriban.Runtime;
using System.Collections.Concurrent;
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

        private static ConcurrentDictionary<ComponentContext, string> _returnTypesCache = new ConcurrentDictionary<ComponentContext, string>();

        public static string GetComponentReturnType(ComponentContext component)
        {
            return _returnTypesCache.GetOrAdd(component, $"{component.SingularName}Component");
        }

        private static ConcurrentDictionary<ComponentContext, string> _returnFullTypesCache = new ConcurrentDictionary<ComponentContext, string>();

        public static string GetComponentReturnFullType(ComponentContext component)
        {
            return _returnFullTypesCache.GetOrAdd(component, $"global::{component.Namespace}.{component.SingularName}Component");
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

        public static string Escape(string str)
        {
            return str.Replace("\"", "\\\"");
        }

        public static string Singularize(string str)
        {
            return new Framework.Workspace.Services.PluralizationService().Singularize(str);
        }
    }
}