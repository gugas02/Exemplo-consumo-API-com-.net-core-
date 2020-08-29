using API.Consumption.test.Domain.Entity;
using API.Consumption.test.Domain.Queries.Input.Advertisement;
using API.Consumption.test.Domain.Shared.Page;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace API.Consumption.test.Domain.Repositories
{
    public interface IAdvertisementRepository
    {
        public Task<bool> Insert(Advertisement advertisement);
        public Task<bool> Update(Advertisement advertisement);
        public Task<bool> Delete(Advertisement advertisement);
        public Task<Advertisement> Get(int id);
        public Task<PagedList<Advertisement>> Get(AdvertisementQuery id);
        Task<List<string>> GetNamesAsync();
    }
}
