﻿using System.Web.Mvc;
using Sitecore.ContentSearch;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Sitecore.SharedSource.CognitiveServices.Foundation;
using Sitecore.SharedSource.CognitiveServices.Search;
using Sitecore.SharedSource.CognitiveServices.Factories;

namespace Sitecore.SharedSource.CognitiveServices.Controllers.Modals
{
    public class CognitiveAnalysisModalController : Controller
    {
        public IWebUtilWrapper WebUtil;
        public ICognitiveSearchContext Searcher;
        public ICognitiveImageAnalysisFactory ImageAnalysisFactory;
        public ICognitiveTextAnalysisFactory TextAnalysisFactory;
        public ISitecoreDataService DataService;
        
        public string IdParameter => WebUtil.GetQueryString("id", string.Empty);
        public string LanguageParameter => WebUtil.GetQueryString("lang", "en");
        public string DbParameter => WebUtil.GetQueryString("db", "master");
        
        public CognitiveAnalysisModalController(
            IWebUtilWrapper webUtil,
            ICognitiveSearchContext searcher,
            ICognitiveImageAnalysisFactory iaFactory,
            ICognitiveTextAnalysisFactory taFactory,
            ISitecoreDataService dataService)
        {
            Assert.IsNotNull(webUtil, typeof(IWebUtilWrapper));
            Assert.IsNotNull(searcher, typeof(ICognitiveSearchContext));
            Assert.IsNotNull(iaFactory, typeof(ICognitiveImageAnalysisFactory));
            Assert.IsNotNull(taFactory, typeof(ICognitiveTextAnalysisFactory));
            Assert.IsNotNull(dataService, typeof(ISitecoreDataService));

            WebUtil = webUtil;
            Searcher = searcher;
            ImageAnalysisFactory = iaFactory;
            TextAnalysisFactory = taFactory;
            DataService = dataService;
        }

        public ActionResult ImageAnalysis()
        {
            ICognitiveSearchResult csr = Searcher.GetAnalysis(IdParameter, LanguageParameter, DbParameter);

            return View("ImageAnalysis", ImageAnalysisFactory.Create(csr));
        }

        public ActionResult TextAnalysis()
        {
            ICognitiveSearchResult csr = Searcher.GetAnalysis(IdParameter, LanguageParameter, DbParameter);
            
            return View("TextAnalysis", TextAnalysisFactory.Create(csr));
        }

        public ActionResult Reanalyze()
        {
            ID id = DataService.GetID(IdParameter);
            if (id.IsNull)
                return View("TextAnalysis", null);

            Item item = DataService.GetDatabase(DbParameter).GetItem(id);
            if(item == null)
                return View("TextAnalysis", null);

            Searcher.UpdateItemInIndex(item, DbParameter);
            
            ICognitiveSearchResult csr = Searcher.GetAnalysis(IdParameter, LanguageParameter, DbParameter);

            return (item.Paths.IsMediaItem)
                ? View("ImageAnalysis", ImageAnalysisFactory.Create(csr))
                : View("TextAnalysis", TextAnalysisFactory.Create(csr));
        }
    }
}