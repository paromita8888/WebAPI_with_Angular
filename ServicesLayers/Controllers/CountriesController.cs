using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Routing;
using Microsoft.SharePoint.Client;
using WebAPI_with_Angular.Models;
using WebAPI_with_Angular.Helpers;
//using WebApi.OutputCache.V2;
using WebAPI_with_Angular.Common;
using System.Net.Http.Headers;
using WebAPI_with_Angular.DAL;
using WebAPI_with_Angular.Helpers;
//

namespace WebAPI_with_Angular.Services.Controllers
{
    
    public class CountriesController : ApiController
    {
        [Route("api/countries")]
         public HttpResponseMessage GetCountries()
        {
            EntityTagHeaderValue requestEtag = Request.Headers.IfNoneMatch.FirstOrDefault();
            EntityTagHeaderValue responseEtag;
            HttpResponseMessage response;

            IEnumerable<Country> countryList = new List<Country>();

            try
            {
                ICountriesRepository repository = RepositoryProvider.GetRepository<ICountriesRepository>();
                countryList = repository.FetchAllCountries();

                if (countryList.Count() <= 0)
                {
                    response = Request.CreateResponse(HttpStatusCode.NoContent);
                }
                else
                {
                    responseEtag = new EntityTagHeaderValue("\"" + countryList.GetHashCode() + "\"");

                    if (requestEtag == null || requestEtag.Tag != responseEtag.Tag)
                    {
                        response = Request.CreateResponse(HttpStatusCode.OK, countryList);
                        response.Headers.CacheControl = new System.Net.Http.Headers.CacheControlHeaderValue();
                        response.Headers.CacheControl.Public = true;
                        response.Headers.CacheControl.MustRevalidate = true;
                        response.Headers.CacheControl.MaxAge = new TimeSpan(30000);
                        response.Headers.CacheControl.NoCache = false;
                        response.Headers.ETag = responseEtag;
                    }
                    else
                    {
                        response = Request.CreateResponse(HttpStatusCode.NotModified);
                    }
                }

            }
            catch (Exception exception)
            {
                string errorMessage = "Error while executing GetCountries() in CountriesController class. Error message: " + exception.Message;
                LogHelper.LogError(errorMessage, exception);
                response = Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Internal server error");
            }

            return response;
        }
    }
}
