﻿using System.Linq;
using Sitecore.ContentSearch;
using Sitecore.ContentSearch.Security;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Sitecore.SharedSource.CognitiveServices.Foundation;

namespace Sitecore.SharedSource.CognitiveServices.Search
{
    public class CognitiveSearchContext : ICognitiveSearchContext
    {
        protected readonly ISitecoreDataService DataService;
        
        protected static readonly string IndexNameFormat = "cognitive_{0}_index";
        
        public CognitiveSearchContext(
            ISitecoreDataService dataService)
        {
            DataService = dataService;
        }

        public ICognitiveSearchResult GetAnalysis(string itemId, string languageCode, string dbName)
        {
            var index = ContentSearchManager.GetIndex(GetIndexName(dbName));
            using (var context = index.CreateSearchContext(SearchSecurityOptions.DisableSecurityCheck))
            {
                return context.GetQueryable<CognitiveSearchResult>()
                    .FirstOrDefault(a => 
                        a.UniqueID.Contains(itemId) 
                        && a.Language == languageCode);
            }
        }
        
        public void AddItemToIndex(string itemId, string dbName)
        {
            ID id = DataService.GetID(itemId);
            if (id.IsNull)
                return;

            Item i = DataService.GetDatabase(dbName).GetItem(id);
            if (i == null)
                return;

            AddItemToIndex(i, dbName);
        }

        public void AddItemToIndex(Item item, string dbName)
        {
            if (item == null)
                return;

            var tempItem = (SitecoreIndexableItem)item;
            ContentSearchManager.GetIndex(GetIndexName(dbName)).Refresh(tempItem);
        }

        public void UpdateItemInIndex(string itemId, string dbName)
        {
            ID id = DataService.GetID(itemId);
            if (id.IsNull)
                return;

            Item i = DataService.GetDatabase(dbName).GetItem(id);
            if (i == null)
                return;

            UpdateItemInIndex(i, dbName);
        }

        public void UpdateItemInIndex(Item item, string dbName)
        {
            if (item == null)
                return;

            var tempItem = (SitecoreIndexableItem)item;
            ContentSearchManager.GetIndex(GetIndexName(dbName)).Update(tempItem.UniqueId);
        }

        protected string GetIndexName(string dbName)
        {
            return string.Format(IndexNameFormat, dbName);
        }
    }
}