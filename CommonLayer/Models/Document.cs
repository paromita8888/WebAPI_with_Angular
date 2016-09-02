using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPI_with_Angular.Models
{
    public class Document
    {
        public string DocumentTitle { get; set; }
        public string DocumentUrl { get; set; }
    
        public DateTime ModifiedDate { get; set; }
      
        public string CountryName { get; set; }
    }
}