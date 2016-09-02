﻿using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace PresentationLayers.App_Start
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Attribute routing.
            //config.MapHttpAttributeRoutes();

            //config.Routes.MapHttpRoute(
            //    name: "DefaultApi",
            //    routeTemplate: "api/{controller}/{id}",
            //    defaults: new { id = RouteParameter.Optional }
            //);

            //// Uncomment the following line of code to enable query support for actions with an IQueryable or IQueryable<T> return type.
            //// To avoid processing unexpected or malicious queries, use the validation settings on QueryableAttribute to validate incoming queries.
            //// For more information, visit http://go.microsoft.com/fwlink/?LinkId=279712.
            ////config.EnableQuerySupport();

            //// To disable tracing in your application, please comment out or remove the following line of code
            //// For more information, refer to: http://www.asp.net/web-api
            //config.EnableSystemDiagnosticsTracing();

            //var appXmlType = config.Formatters.XmlFormatter.SupportedMediaTypes.FirstOrDefault(t => t.MediaType == "application/xml");
            //config.Formatters.XmlFormatter.SupportedMediaTypes.Remove(appXmlType);

            //// Use camel case for JSON data.
            //config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

            //config.EnsureInitialized();
        }

    }
}