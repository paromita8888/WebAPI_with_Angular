using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Microsoft.SharePoint.Client;
using WebAPI_with_Angular.Models;
using WebAPI_with_Angular.Helpers;
//using WebApi.OutputCache.V2;
using WebAPI_with_Angular.Common;
using System.Net.Http.Headers;
using WebAPI_with_Angular.DAL;
using WebAPI_with_Angular.Helpers;

namespace WebAPI_with_Angular.Services.Controllers
{
    [RoutePrefix("api/documents")]
    public class DocumentsController : ApiController
    {
        ///<summary>
        ///Gets the list of all documents by alpha3code of a country
        ///</summary>
        [Route("{alpha3code}")]
        public HttpResponseMessage GetDocuments(string alpha3code)
        {

            EntityTagHeaderValue requestEtag = Request.Headers.IfNoneMatch.FirstOrDefault();
            EntityTagHeaderValue responseEtag;
            HttpResponseMessage response;

            IList<Document> documentList = new List<Document>();

            try
            {
                if (string.IsNullOrEmpty(alpha3code) == false)
                {
                    ICountryDocRepository repo = RepositoryProvider.GetRepository<ICountryDocRepository>();
                    documentList = repo.FetchDocuments(alpha3code);


                    if (documentList.Count() <= 0)
                    {
                        response = Request.CreateResponse(HttpStatusCode.NoContent);
                    }
                    else
                    {
                        responseEtag = new EntityTagHeaderValue("\"" + documentList.GetHashCode() + "\"");

                        if (requestEtag == null || requestEtag.Tag != responseEtag.Tag)
                        {
                            response = Request.CreateResponse(HttpStatusCode.OK, documentList);
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
                else
                {
                    response = Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Country code should be specified as parameter.");
                }
            }
            catch (Exception exception)
            {
                string errorMessage = "Error while executing GetDocuments() in DocumentsController class. Error message: " + exception.Message;
                LogHelper.LogError(errorMessage, exception);
                response = Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Internal server error");
            }

            return response;
        }

        /// <summary>
        /// Gets the documents collection by menu title
        /// </summary>
        /// <param name="menuTitle">Title of the menu</param>
        /// <returns>Document Collection</returns>
        [Route("menu/{menuTitle}")]
        public HttpResponseMessage GetDocumnentsByMenu(string menuTitle)
        {
            EntityTagHeaderValue requestEtag = Request.Headers.IfNoneMatch.FirstOrDefault();
            EntityTagHeaderValue responseEtag;
            HttpResponseMessage response;

            IList<Document> documentList = new List<Document>();

            try
            {
                if (string.IsNullOrEmpty(menuTitle) == false)
                {
                    switch (menuTitle)
                    {
                        case Constants.EdelmanEdgeExplanatoryDocuments:
                            {
                                IExplanatoryDocRepository reop = RepositoryProvider.GetRepository<IExplanatoryDocRepository>();
                                documentList = reop.FetchDocuments();
                            }
                            break;

                        case Constants.ResearchReferenceDocuments:
                            {
                                IRRDocRepository reop = RepositoryProvider.GetRepository<IRRDocRepository>();
                                documentList = reop.FetchDocuments();
                            }
                            break;

                        case Constants.NewBusinessFiles:
                            {
                                IBusinessFilesRepository reop = RepositoryProvider.GetRepository<IBusinessFilesRepository>();
                                documentList = reop.FetchDocuments();
                            }
                            break;

                        case Constants.RegionalSummaries:
                            {
                                IRegionalSummariesRepository reop = RepositoryProvider.GetRepository<IRegionalSummariesRepository>();
                                documentList = reop.FetchDocuments();
                            }
                            break;
                        case Constants.MarketingMaterials:
                            {
                                IMarketingMaterialsRepository reop = RepositoryProvider.GetRepository<IMarketingMaterialsRepository>();
                                documentList = reop.FetchDocuments();
                            }
                            break;

                        default:
                            return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Incorrect list name..");
                    }

                    if (documentList.Count() <= 0)
                    {
                        response = Request.CreateResponse(HttpStatusCode.NoContent);
                    }
                    else
                    {
                        responseEtag = new EntityTagHeaderValue("\"" + documentList.GetHashCode() + "\"");

                        if (requestEtag == null || requestEtag.Tag != responseEtag.Tag)
                        {
                            response = Request.CreateResponse(HttpStatusCode.OK, documentList);
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
                else
                {
                    response = Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Menu title should be specified as parameter.");
                }
            }
            catch (Exception exception)
            {
                string errorMessage = string.Format(@"Error while fetching document for {0}", menuTitle);
                LogHelper.LogError(errorMessage, exception);
                response = Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Internal server error");
            }

            return response;
        }

    }
}
