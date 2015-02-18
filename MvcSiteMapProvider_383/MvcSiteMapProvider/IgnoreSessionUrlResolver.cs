using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;
using MvcSiteMapProvider;
using MvcSiteMapProvider.Web;
using MvcSiteMapProvider.Web.Mvc;
using MvcSiteMapProvider.Web.UrlResolver;

namespace MvcSiteMapProvider_383
{
    public class IgnoreSessionUrlResolver : SiteMapNodeUrlResolver
    {
        public IgnoreSessionUrlResolver(IMvcContextFactory mvcContextFactory, IUrlPath urlPath)
            : base(mvcContextFactory, urlPath)
        {
        }

        protected override string ResolveRouteUrl(ISiteMapNode node, string area, string controller, string action, IDictionary<string, object> routeValues, RequestContext requestContext)
        {
            string result = string.Empty;
            var urlHelper = this.mvcContextFactory.CreateUrlHelper(requestContext);
            var routeValueDictionary = new RouteValueDictionary(routeValues);

            // Since the same URL is generated more than once, we need this workaround
            // to ensure that doesn't kick in until the node has been added to the SiteMap
            // because there is a check there to ensure no duplicate URLs.
            if (node.IsReadOnly)
            {
                // Remove the session state from the route values before generating URLs
                this.RemoveSessionValues(node, routeValueDictionary);
            }

            if (!string.IsNullOrEmpty(node.Route))
            {
                routeValueDictionary.Remove("route");
                result = urlHelper.RouteUrl(node.Route, routeValueDictionary);
            }
            else
            {
                result = urlHelper.Action(action, controller, routeValueDictionary);
            }

            if (!string.IsNullOrEmpty(result))
            {
                // NOTE: We are purposely using the current request's context when resolving any absolute 
                // URL so it will read the headers and use the HTTP_HOST of the client machine, if included,
                // in the case where HostName is null or empty and protocol is not. The public facing port is
                // also used when the protocol matches.
                return this.urlPath.ResolveUrl(result, node.Protocol, node.HostName);
            }

            return result;
        }

        private void RemoveSessionValues(ISiteMapNode node, RouteValueDictionary routeValues)
        {
            var keys = Convert.ToString(node.Attributes["copySessionToRoute"]).Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var key in keys)
            {
                routeValues.Remove(key);
            }
        }
    }
}