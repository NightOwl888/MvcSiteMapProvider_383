using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MvcSiteMapProvider;
using MvcSiteMapProvider.Globalization;
using MvcSiteMapProvider.Web;
using MvcSiteMapProvider.Web.Mvc;

namespace MvcSiteMapProvider_383
{
    public class CopySessionSiteMapNode : RequestCacheableSiteMapNode
    {
        //private readonly IMvcContextFactory mvcContextFactory;

        public CopySessionSiteMapNode(ISiteMap siteMap, string key, bool isDynamic, ISiteMapNodePluginProvider pluginProvider, IMvcContextFactory mvcContextFactory, ISiteMapNodeChildStateFactory siteMapNodeChildStateFactory, ILocalizationService localizationService, IUrlPath urlPath)
            : base(siteMap, key, isDynamic, pluginProvider, mvcContextFactory, siteMapNodeChildStateFactory, localizationService, urlPath)
        {
            //this.mvcContextFactory = mvcContextFactory;
        }

        public override bool MatchesRoute(IDictionary<string, object> routeValues)
        {
            // If not clickable, we never want to match the node.
            if (!this.Clickable)
                return false;

            // If URL is set explicitly, we should never match based on route values.
            if (!string.IsNullOrEmpty(this.UnresolvedUrl))
                return false;

            // Check whether the configured host name matches (only if it is supplied).
            if (!string.IsNullOrEmpty(this.HostName) && !this.urlPath.IsPublicHostName(this.HostName, this.HttpContext))
                return false;

            // Merge in any query string values from the current context that match keys with
            // the route values configured in the current node (MVC doesn't automatically assign them 
            // as route values). This allows matching on query string values, but only if they 
            // are configured in the node.
            var values = this.MergeRouteValuesAndNamedQueryStringValues(routeValues, this.RouteValues.Keys, this.HttpContext);

            values = this.MergeRouteValuesAndSessionStateValues(values, this.HttpContext);

            return this.RouteValues.MatchesRoute(values);
        }

        private IDictionary<string, object> MergeRouteValuesAndSessionStateValues(IDictionary<string, object> routeValues, HttpContextBase httpContext)
        {
            // Make a copy of the routeValues. We only want to limit this to the scope of the current node.
            var result = new Dictionary<string, object>(routeValues);

            if (this.Attributes.ContainsKey("copySessionToRoute") && httpContext.Session != null)
            {
                var keys = Convert.ToString(this.Attributes["copySessionToRoute"]).Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                var session = httpContext.Session;

                foreach (var key in keys)
                {
                    if (session[key] != null)
                    {
                        // Copy the key from session into the route
                        result[key] = session[key];
                    }
                }
            }

            return result;
        }
    }
}