
using API.Consumption.test.Domain.Entity;
using API.Consumption.test.Domain.Queries.Input.Advertisement;
using API.Consumption.test.Domain.Repositories;
using API.Consumption.test.Domain.Shared.Page;
using API.Consumption.test.Infra.DataContexts;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Consumption.test.Infra.Repositories
{
    public class AdvertisementRepository : IAdvertisementRepository
    {
        private readonly TesteDataContext _context;

        public AdvertisementRepository(TesteDataContext context)
        {
            _context = context;
        }

        public async Task<List<string>> GetNamesAsync()
        {
            var query = @"SELECT distinct [FullName]
                      FROM [dbo].[Advertisement]
                        where [Enabled] = 1";

            var usersNames = (await _context
                   .Connection
                   .QueryAsync<string>(
                        query,
                       new { })).ToList();

            return usersNames;
        }

        public async Task<Advertisement> Get(int id)
        {
            var advertisementQuery = @" select ID as Id
                                            ,marca as Brand
                                            ,modelo as Model
                                            ,versao as Version
                                            ,ano as Year
                                            ,quilometragem as Km
                                            ,observacao as Observation 
                                        from [dbo].[tb_AnuncioWebmotors] where ID = @id ";

            using (var multi = await _context.Connection.QueryMultipleAsync(advertisementQuery, new
            {
                id
            }))
            {
                return multi.ReadFirstOrDefault<Advertisement>();
            }
        }

        public async Task<PagedList<Advertisement>> Get(AdvertisementQuery query)
        {
            var countQuery = @" select count(ID) from [dbo].[tb_AnuncioWebmotors] ";
            var advertisementQuery = @" select ID as Id
                                            ,marca as Brand
                                            ,modelo as Model
                                            ,versao as Version
                                            ,ano as Year
                                            ,quilometragem as Km
                                            ,observacao as Observation 
                                        from [dbo].[tb_AnuncioWebmotors] ";

            // TODO: IMPELEMENT LOGIC FOR FILTERING HERE

            advertisementQuery += " ORDER BY ID desc OFFSET @ItemFrom ROWS FETCH NEXT @PageSize ROWS ONLY ";


            using (var multi = await _context.Connection.QueryMultipleAsync(advertisementQuery + countQuery, new
            {
                query.PageSize,
                query.ItemFrom
            }))
            {
                var result = multi.Read<Advertisement>().ToList();
                var countResult = multi.ReadFirstOrDefault<long>();

                return new PagedList<Advertisement>(result, countResult, query.PageSize);
            }
        }

        public async Task<bool> Insert(Advertisement advertisement)
        {
            var query = @"INSERT INTO [dbo].[tb_AnuncioWebmotors]
                               (marca ,modelo ,versao ,ano ,quilometragem ,observacao )
                         VALUES
                               (@Brand, @Model, @Version, @Year, @Km, @Observation)";
            try
            {
                await
                        _context
                        .Connection
                        .QueryFirstOrDefaultAsync(query, new
                        {
                            advertisement.Brand,
                            advertisement.Model,
                            advertisement.Version,
                            advertisement.Year,
                            advertisement.Km,
                            advertisement.Observation
                        });
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> Update(Advertisement advertisement)
        {
            var query = @"UPDATE [dbo].[tb_AnuncioWebmotors]
                           SET marca = @Brand
                            ,modelo = @Model
                            ,versao = @Version
                            ,ano = @Year
                            ,quilometragem = @Km
                            ,observacao = @Observation
                         WHERE [ID] = @Id";

            try
            {
                await
                         _context
                         .Connection
                         .QueryAsync(query, new
                         {
                             advertisement.Id,
                             advertisement.Brand,
                             advertisement.Model,
                             advertisement.Version,
                             advertisement.Year,
                             advertisement.Km,
                             advertisement.Observation,
                         });
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> Delete(Advertisement advertisement)
        {
            var query = @"DELETE FROM [dbo].[tb_AnuncioWebmotors]
                          WHERE [ID] = @Id";

            try
            {
                await
                     _context
                     .Connection
                     .QueryAsync(query, new
                     {
                         advertisement.Id,
                     });
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}