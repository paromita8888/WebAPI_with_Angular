using System.Collections.Generic;
using WebAPI_with_Angular.Common;
using WebAPI_with_Angular.Models;
using WebAPI_with_Angular.Helpers;


namespace WebAPI_with_Angular.DAL
{
    class SPCachedRRDocRepository : IRRDocRepository
    {
        static readonly string cacheKey = "RRDocs";
        static SPCachedRRDocRepository()
        {
            IList<string> keys = new List<string>();
            keys.Add(cacheKey);
            SPObjectCache.RegisterForNotification(Constants.ResearchReferenceDocuments, keys);
        }
        public IList<Models.Document> FetchDocuments()
        {
            List<Document> docList = SPObjectCache.Get(cacheKey) as List<Document>;

            if (docList == null)
            {
                SharePointHelper spHelper = new SharePointHelper();
                docList = spHelper.GetDocumnentsByMenu(Constants.ResearchReferenceDocuments);

                SPObjectCache.Add(cacheKey, docList);
            }

            return docList;
        }
    }
}
