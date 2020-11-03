using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ModelService;

namespace CountryService
{
    public interface ICountrySvc
    {
        Task<List<CountryModel>> GetCountriesAsync();
    }
}
