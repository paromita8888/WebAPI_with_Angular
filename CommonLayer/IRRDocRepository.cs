using WebAPI_with_Angular.Models;
using System.Collections.Generic;

namespace WebAPI_with_Angular.Common
{
    public interface IRRDocRepository
    {
        IList<Document> FetchDocuments();
    }
}
