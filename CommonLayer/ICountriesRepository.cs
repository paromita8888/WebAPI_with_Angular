using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI_with_Angular.Models;

namespace WebAPI_with_Angular.Common
{
    /// <summary>
    /// Countries repository for accessing all countries where edelman is present.
    /// </summary>
    public interface ICountriesRepository
    {
        IEnumerable<Country> FetchAllCountries();
    }
}
