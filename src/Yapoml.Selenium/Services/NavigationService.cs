using System;
using System.Collections.Generic;
using System.Linq;
using Yapoml.Framework.Options;
using Yapoml.Selenium.Options;

namespace Yapoml.Selenium.Services
{
    public class NavigationService
    {
        readonly ISpaceOptions _spaceOptions;

        public NavigationService(ISpaceOptions spaceOptions)
        {
            _spaceOptions = spaceOptions;
        }

        public Uri BuildUri(string url, IList<KeyValuePair<string, string>> segments, IList<KeyValuePair<string, string>> queryParams)
        {
            url = new SegmentService().Replace(url, segments);

            UriBuilder urlBuilder;

            if (Uri.IsWellFormedUriString(url, UriKind.Relative))
            {
                var baseUrl = _spaceOptions.Services.Get<BaseUrlOptions>();

                urlBuilder = new UriBuilder(new Uri(new Uri(baseUrl.Url), url));
            }
            else
            {
                urlBuilder = new UriBuilder(url);
            }

            if (queryParams != null && queryParams.Count > 0)
            {
                urlBuilder.Query = string.Join("&", queryParams.Where(qp => qp.Value is not null).Select(qp => $"{qp.Key}={qp.Value}"));
            }

            return urlBuilder.Uri;
        }
    }
}
