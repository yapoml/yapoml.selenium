using System.Collections.Generic;

namespace Yapoml.Selenium.Services
{
    public class SegmentService
    {
        public string Replace(string value, IList<KeyValuePair<string, string>> segments)
        {
            if (segments != null)
            {
                foreach (var segment in segments)
                {
                    value = value.Replace($"{{{segment.Key}}}", segment.Value);
                }
            }

            return value;
        }
    }
}
