using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Microsoft.SharePoint.Client;
using WebAPI_with_Angular.Helpers;
using System.DirectoryServices;
using WebAPI_with_Angular.Common;

namespace WebAPI_with_Angular.Services.Controllers
{
    [RoutePrefix("api/users")]
    public class UsersController : ApiController
    {

        ///<summary>
        ///Gets the windows username of the  user logged in to the system
        ///</summary>
        public string GetUser()
        {

            string userName = string.Empty;
            try
            {
                userName = User.Identity.Name;
                string[] userNameParts = userName.Split(new char[] { '\\' }); 
                if (userNameParts.Length > 1)
                {
                    DirectoryEntry userEntry = new DirectoryEntry("WinNT://" + userNameParts[0] + "/" + userNameParts[1] + ",User"); 
                    string fullUserName = (string)userEntry.Properties["fullname"].Value;
                    string[] fullUserNameParts = fullUserName.Split(new char[] { ',' });  
                    userName = fullUserNameParts[1] +  " " + fullUserNameParts[0];
                }
            }
            catch (Exception exception)
            {
                string errorMessage = "Error in getting logged in user name: " + exception.Message;
                LogHelper.LogError(errorMessage, exception);
                userName.DefaultIfEmpty() ;
            }
            return userName;
        }
    }
}