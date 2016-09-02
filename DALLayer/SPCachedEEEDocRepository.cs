using System.Collections.Generic;
using WebAPI_with_Angular.Common;
using WebAPI_with_Angular.Models;
using WebAPI_with_Angular.Helpers;


namespace WebAPI_with_Angular.DAL
{
    class SPCachedEEEDocRepository : IExplanatoryDocRepository
    {
        static readonly string cacheKey = "EEEDocs";
        static SPCachedEEEDocRepository()
        {
            IList<string> keys = new List<string>();
            keys.Add(cacheKey);
            SPObjectCache.RegisterForNotification(Constants.EdelmanEdgeExplanatoryDocuments, keys);
        }
        public IList<Models.Document> FetchDocuments()
        {
            List<Document> docList = SPObjectCache.Get(cacheKey) as List<Document>;

            if (docList == null)
            {
                SharePointHelper spHelper = new SharePointHelper();
                docList = spHelper.GetDocumnentsByMenu(Constants.EdelmanEdgeExplanatoryDocuments);

                SPObjectCache.Add(cacheKey, docList);
            }

            return docList;
        }
    }
}
