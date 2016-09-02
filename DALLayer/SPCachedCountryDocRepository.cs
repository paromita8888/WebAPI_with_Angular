using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI_with_Angular.Common;
using WebAPI_with_Angular.Helpers;
using WebAPI_with_Angular.Models;

namespace WebAPI_with_Angular.DAL
{
    public class SPCachedCountryDocRepository : ICountryDocRepository
    {
        static readonly string cacheKey = "countriesDocs";
        static SPCachedCountryDocRepository()
        {
            IList<string> keys = new List<string>();
            keys.Add(cacheKey);
            SPObjectCache.RegisterForNotification(Constants.CountryDocumentsMappingList, keys);
        }

        public IList<Document> FetchDocuments(string countryCode)
        {
            IList<Document> docList = null;
            try
            {
                Dictionary<string, IList<Document>> countryToDocMap = SPObjectCache.Get(cacheKey) as Dictionary<string, IList<Document>>;

                if (countryToDocMap == null)
                {
                    countryToDocMap = new Dictionary<string, IList<Document>>();
                }

                if (countryToDocMap.ContainsKey(countryCode))
                {
                    docList = countryToDocMap[countryCode];
                }

                if (docList == null)
                {
                    SharePointHelper spHelper = new SharePointHelper();
                    docList = spHelper.GetCountryDocuments(countryCode);

                    countryToDocMap[countryCode] = docList;
                    SPObjectCache.Add(cacheKey, countryToDocMap);
                }
            }
            catch(Exception ex)
            {
                LogHelper.LogError(string.Format(@"Error fetching document for {0}", countryCode), ex);
            }

            return docList;
        }
    }
}
