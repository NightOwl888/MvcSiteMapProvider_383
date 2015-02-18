using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MvcSiteMapProvider;
using MvcSiteMapProvider.Globalization;
using MvcSiteMapProvider.Web.Mvc;
using MvcSiteMapProvider.Web;

namespace MvcSiteMapProvider_383
{
    public class CopySessionSiteMapNodeFactory : ISiteMapNodeFactory
    {
        protected readonly ISiteMapNodeChildStateFactory siteMapNodeChildStateFactory;
        protected readonly ILocalizationServiceFactory localizationServiceFactory;
        protected readonly ISiteMapNodePluginProvider pluginProvider;
        protected readonly IUrlPath urlPath;
        protected readonly IMvcContextFactory mvcContextFactory;

        public CopySessionSiteMapNodeFactory(
            ISiteMapNodeChildStateFactory siteMapNodeChildStateFactory,
            ILocalizationServiceFactory localizationServiceFactory,
            ISiteMapNodePluginProvider pluginProvider,
            IUrlPath urlPath,
            IMvcContextFactory mvcContextFactory
            ) 
        {
            if (siteMapNodeChildStateFactory == null)
                throw new ArgumentNullException("siteMapNodeChildStateFactory");
            if (localizationServiceFactory == null)
                throw new ArgumentNullException("localizationServiceFactory");
            if (pluginProvider == null)
                throw new ArgumentNullException("pluginProvider");
            if (urlPath == null)
                throw new ArgumentNullException("urlPath");
            if (mvcContextFactory == null)
                throw new ArgumentNullException("mvcContextFactory");

            this.siteMapNodeChildStateFactory = siteMapNodeChildStateFactory;
            this.localizationServiceFactory = localizationServiceFactory;
            this.pluginProvider = pluginProvider;
            this.urlPath = urlPath;
            this.mvcContextFactory = mvcContextFactory;
        }

        public ISiteMapNode Create(ISiteMap siteMap, string key, string implicitResourceKey)
        {
            return CreateInternal(siteMap, key, implicitResourceKey, false);
        }

        public ISiteMapNode CreateDynamic(ISiteMap siteMap, string key, string implicitResourceKey)
        {
            return CreateInternal(siteMap, key, implicitResourceKey, true);
        }

        private ISiteMapNode CreateInternal(ISiteMap siteMap, string key, string implicitResourceKey, bool isDynamic)
        {
            // IMPORTANT: we must create one localization service per node because the service contains its own state that applies to the node
            var localizationService = localizationServiceFactory.Create(implicitResourceKey);

            return new CopySessionSiteMapNode(
                siteMap,
                key,
                isDynamic,
                pluginProvider,
                mvcContextFactory,
                siteMapNodeChildStateFactory,
                localizationService,
                urlPath);
        }
    }
}