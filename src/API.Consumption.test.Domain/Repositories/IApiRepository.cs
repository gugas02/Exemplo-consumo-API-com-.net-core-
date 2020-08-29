using API.Consumption.test.Domain.Queries.Output.Api;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace API.Consumption.test.Domain.Repositories
{
    public interface  IApiRepository
    {
        Task<List<GetBrandsQueryResult>> GetBrands();
        Task<List<GetModelQueryResult>> GetModel(string brandId);
        Task<List<GetVehiclesQueryResult>> GetVehicles(string modelId, string querystring);
        Task<List<GetVersionQueryResult>> GetVersion(string page);
    }
}
