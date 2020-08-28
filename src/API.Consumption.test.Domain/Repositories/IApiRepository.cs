using API.Consumption.test.Domain.Queries.Output.Api;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace API.Consumption.test.Domain.Repositories
{
    public interface  IApiRepository
    {
        Task<GetBrandsQueryResult> GetBrands(string pageId, string querystring);
        Task<GetModelQueryResult> GetModel(string pageId, string querystring);
        Task<GetVehiclesQueryResult> GetVehicles(string type, string querystring);
        Task<GetVersionQueryResult> GetVersion(string type, string querystring);
    }
}
