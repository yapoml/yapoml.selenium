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

        private static Dictionary<ComponentContext, string> _returnTypesCache= new Dictionary<ComponentContext, string>();

        public static string GetComponentReturnType(ComponentContext component)
        {
            if (_returnTypesCache.TryGetValue(component, out var chachedRetType))
            {
                return chachedRetType;
            }
            else
            {
                string retType;

                if (component.ReferencedComponent == null)
                {
                    if (component.IsPlural)
                    {
                        retType = $"global::{component.Namespace}.{component.SingularName}Component";
                    }
                    else
                    {
                        retType = $"global::{component.Namespace}.{component.Name}Component";
                    }
                }
                else
                {
                    if (component.ReferencedComponent.IsPlural)
                    {
                        retType = $"global::{component.ReferencedComponent.Namespace}.{component.ReferencedComponent.SingularName}Component";
                    }
                    else
                    {
                        retType = $"global::{component.ReferencedComponent.Namespace}.{component.ReferencedComponent.Name}Component";
                    }
                }

                _returnTypesCache[component] = retType;

                return retType;
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

        public static string A(ComponentContext c)
        {
            return "qwe";
        }
    }
}