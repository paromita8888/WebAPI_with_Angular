using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebAPI_with_Angular.Common;

namespace WebAPI_with_Angular.Services.Controllers
{
    public class SPListenerController : ApiController
    {
        // POST api/<controller>
        [HttpPost]
        public void RefreshList([FromBody]string listName)
        {
            try
            {
                SPObjectCache.OnSPListupdated(listName);
            }
            catch (Exception exception)
            {
                LogHelper.LogError(string.Format(@"Error refreshing {0}, listName"), exception);

                throw;
            }
        }
    }
}