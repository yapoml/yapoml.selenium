using Humanizer;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading;

namespace Yapoml.Selenium.Services
{
    public static class Waiter
    {
        static Waiter()
        {
            AppDomain.CurrentDomain.AssemblyResolve += CurrentDomain_AssemblyResolve;
        }

        public static TResult Until<TResult>(Func<TResult> condition, TimeSpan timeout, TimeSpan pollingInterval)
        {
            var stopwatch = Stopwatch.StartNew();

            while (stopwatch.Elapsed <= timeout)
            {
                var result = condition();
                if (result != null)
                {
                    return result;
                }

                Thread.Sleep(pollingInterval);
            }

            throw new TimeoutException($"{condition} wasn't met during {timeout.Humanize()} and polling each {pollingInterval.Humanize()}.");
        }

        public static IWebElement UntilDisplayed(string componentFriendlyName, ISearchContext searchContext, By by, TimeSpan timeout, TimeSpan pollingInterval)
        {
            Exception lastError = null;

            IWebElement condition()
            {
                try
                {
                    var element = searchContext.FindElement(by);

                    return element.Displayed ? element : null;
                }
                catch (Exception ex) when (ex is NoSuchElementException || ex is StaleElementReferenceException)
                {
                    lastError = ex;

                    return null;
                }
            }

            try
            {
                return Until(condition, timeout, pollingInterval);
            }
            catch (TimeoutException)
            {
                throw BuildTimeoutException($"{componentFriendlyName} component is not displayed yet '{by}'.", lastError, timeout, pollingInterval, new List<Type> { typeof(NoSuchElementException), typeof(StaleElementReferenceException) });
            }
        }

        public static TimeoutException BuildTimeoutException(string message, Exception innerException, TimeSpan timeout, TimeSpan pollingInterval, IEnumerable<Type> ignoredExceptionTypes)
        {
            var builder = new StringBuilder();

            builder.AppendLine(message);

            builder.AppendLine();

            builder.AppendLine($"  Timeout is {timeout.Humanize()} and polling each {pollingInterval.Humanize()}.");

            if (ignoredExceptionTypes != null)
            {
                builder.AppendLine();

                builder.AppendLine("  Ignored exceptions:");

                foreach (Type ignoredExceptionType in ignoredExceptionTypes)
                {
                    builder.AppendLine($"   - {ignoredExceptionType.Name}");
                }
            }

            return new TimeoutException(builder.ToString(), innerException);
        }

        private static Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        {
            Assembly dep = null;

            if (args.Name.StartsWith("Humanizer"))
            {
                var resourceName = "Yapoml.Selenium.Humanizer.dll";

                using (Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName))
                {
                    using (BinaryReader reader = new BinaryReader(stream))
                    {
                        dep = Assembly.Load(reader.ReadBytes((int)stream.Length));
                    }
                }
            }

            return dep;
        }
    }
}
