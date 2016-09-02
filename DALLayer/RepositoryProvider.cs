using WebAPI_with_Angular.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI_with_Angular.DAL
{
    public class RepositoryProvider
    {
        public static T GetRepository<T>()
        {
            if(typeof(T) == typeof(ICountriesRepository))
            {
                return (T)(ICountriesRepository)new SPCachedCountriesRepository();
            }
            else if (typeof(T) == typeof(ICountryDocRepository))
            {
                return (T)(ICountryDocRepository)new SPCachedCountryDocRepository();
            }
            else if (typeof(T) == typeof(IRegionalSummariesRepository))
            {
                return (T)(IRegionalSummariesRepository)new SPCachedRSRepository();
            }
            else if (typeof(T) == typeof(IMarketingMaterialsRepository))
            {
                return (T)(IMarketingMaterialsRepository)new SPCachedMarketingMaterialRepository();
            }
            else if (typeof(T) == typeof(IBusinessFilesRepository))
            {
                return (T)(IBusinessFilesRepository)new SPCachedNBFRepository();
            }
            else if (typeof(T) == typeof(IRRDocRepository))
            {
                return (T)(IRRDocRepository)new SPCachedRRDocRepository();
            }
            else if (typeof(T) == typeof(IExplanatoryDocRepository))
            {
                return (T)(IExplanatoryDocRepository)new SPCachedEEEDocRepository();
            }

            throw new NotImplementedException(string.Format(@"Creation of {0} is not supported.", typeof(T)));
        }
    }
}
