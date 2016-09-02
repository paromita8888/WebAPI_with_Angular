using WebAPI_with_Angular.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI_with_Angular.Common
{
    public interface IMarketingMaterialsRepository
    {
        IList<Document> FetchDocuments();
    }
}
