using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI_with_Angular.Models;

namespace WebAPI_with_Angular.Common
{
    public interface ICountryDocRepository
    {
        IList<Document> FetchDocuments(string countryCode);
    }
}
