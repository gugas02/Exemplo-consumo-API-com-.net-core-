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
        public Task Insert(Advertisement employee);
        public Task Update(Advertisement employee);
        public Task Delete(Advertisement employee);
        public Task<Advertisement> Get(int id);
        public Task<PagedList<Advertisement>> Get(AdvertisementQuery id);
        Task<List<string>> GetNamesAsync();
    }
}
