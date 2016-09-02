using WebAPI_with_Angular.Common;
using WebAPI_with_Angular.Models;
using Microsoft.SharePoint.Client;
using System.Collections.Generic;
using System.Runtime.Caching;

namespace WebAPI_with_Angular.DAL
{
    /// <summary>
    /// Cached repository for all countries. This repository will be refreshed once
    /// </summary>
    public class SPCachedCountriesRepository : ICountriesRepository
    {
        static readonly string cacheKey = "Countries";

        static SPCachedCountriesRepository()
        {
            IList<string> cacheKeys = new List<string>();
            cacheKeys.Add(cacheKey);
            SPObjectCache.RegisterForNotification(Constants.CountryList, cacheKeys);
        }
        
        public IEnumerable<Models.Country> FetchAllCountries()
        {
            
            List<Country> countryList = SPObjectCache.Get(cacheKey) as List<Country>;

            //return from cache if record exists else fetch from sharepoint.
            if (countryList == null)
            {
                countryList = new List<Country>();
                ClientContext context = new ClientContext(ConfigHelper.GetConfigValue(Constants.ClientSharePointSiteConfigKey));
                List announcementsList = context.Web.Lists.GetByTitle(Constants.CountryList);

                // Get all the items from the Countries list
                CamlQuery query = CamlQuery.CreateAllItemsQuery();
                string queryText = @"<View ><Query>
                        <Where>
                            <Eq>
                                <FieldRef Name='IsActive'/>
                                <Value Type='Integer'>1</Value>
                            </Eq>
                        </Where>
                        <OrderBy>
                            <FieldRef Name='Country'/>
                        </OrderBy>
                        </Query>
                        </View>";
                query.ViewXml = queryText;
                ListItemCollection items = announcementsList.GetItems(query);

                context.Load(items);
                context.ExecuteQuery();
                foreach (ListItem listItem in items)
                {
                    if (listItem[Constants.CountryColoumnDisplayName] != null)
                    {
                        Country country = new Country();
                        country.Name = listItem[Constants.CountryColoumnDisplayName].ToString();
                        country.Aplha3_Code = listItem[Constants.AlphaCodeColoumnDisplayName].ToString();
                        countryList.Add(country);
                    }
                }

                SPObjectCache.Add(cacheKey, countryList);
            }

            return countryList;
        }
    }
}
