﻿using Microsoft.EntityFrameworkCore;
using SME_API_Apimanagement.Entities;
using SME_API_Apimanagement.Models;

namespace SME_API_Apimanagement.Repository
{
    public class TAPIMappingRepository : ITAPIMappingRepository
    {
        private readonly ApiMangeDBContext _context;

        public TAPIMappingRepository(ApiMangeDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TApiPermisionMapping>> GetAllAsync() =>
            await _context.TApiPermisionMappings.Where(x => x.FlagDelete == "N").ToListAsync();

        public async Task<TApiPermisionMapping> GetByIdAsync(int id) =>
            await _context.TApiPermisionMappings.FirstOrDefaultAsync(x => x.Id == id && x.FlagDelete == "N");

        public async Task AddAsync(TApiPermisionMapping mapping)
        {
            _context.TApiPermisionMappings.Add(mapping);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(TApiPermisionMapping mapping)
        {
            _context.TApiPermisionMappings.Update(mapping);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var mapping = await _context.TApiPermisionMappings.FindAsync(id);
            if (mapping != null)
            {
                mapping.FlagDelete = "Y";
                mapping.UpdateDate = DateTime.UtcNow;
                await _context.SaveChangesAsync();
            }
        }
        public async Task<int> DeleteByOrganizationCode(string orgCode)
        {
            var records = _context.TApiPermisionMappings.Where(x => x.OrganizationCode == orgCode);

            if (records.Any())
            {
                _context.TApiPermisionMappings.RemoveRange(records);
                return await _context.SaveChangesAsync();
            }
            else
            {
                return 1;
            }


        }
        public async Task<int> UpdateOrInsertTApiMapping(UpSertRegisterApiModels xModels, string apiKey)
        {
            int success = 0;

            try
            {
                var result = await DeleteByOrganizationCode(xModels.MRegister.OrganizationCode); // เรียกใช้ await

                if (result != 0)
                {
                    foreach (var item in xModels.LPerMapApi)
                    {
                        var xRaw = new TApiPermisionMapping
                        {
                            StartDate = xModels.MRegister.StartDate,
                            EndDate = xModels.MRegister.EndDate,
                            OrganizationCode = item.OrganizationCode,
                            SystemCode = item.SystemCode,
                            ApiKey = apiKey,
                            FlagActive = item.IsSelected,
                            FlagDelete = "N",
                            UpdateDate = DateTime.Now,
                            CreateDate = DateTime.Now,
                            CreateBy = xModels.MRegister.CreateBy,
                            UpdateBy = xModels.MRegister.CreateBy,
                            SystemApiMappingId = item.SystemApiMappingId,
                            

                        };

                        _context.TApiPermisionMappings.Add(xRaw);
                        await _context.SaveChangesAsync();
                        success = xRaw.Id; // ดึงค่า Id หลัง Save
                    }

                    success = await _context.SaveChangesAsync(); // เรียก SaveChangesAsync ครั้งเดียว
                }

            }
            catch (Exception ex)
            {
                success = 0; // ถ้า Error ให้ return 0
            }

            return success;
        }
        public async Task<List<TApiPermisionMappingModels>> GetTApiMappingBySearch(TApiPermisionMappingModels xModels)
        {
            var result = new List<TApiPermisionMappingModels>();
            try
            {
                var query = (
                  from ts in _context.TSystemApiMappings
                  join ms in _context.MSystems
                      on ts.OwnerSystemCode equals ms.SystemCode
                  join ta in _context.TApiPermisionMappings
                      on ts.Id equals ta.SystemApiMappingId into taGroup
                  from ta in taGroup.DefaultIfEmpty()
                  select new TApiPermisionMappingModels
                             {
                                    SystemCode = ms.SystemCode,
                                    SystemName = ms.SystemName,
                                  
                                    ApiKey = ta.ApiKey,
                                    FlagActive = ta.FlagActive,
                                    StartDate =ta.StartDate,
                                     EndDate = ta.EndDate,
                                     OrganizationCode = ta.OrganizationCode
                                     ,ServiceName = ts.ApiServiceName,
                      SystemApiMappingId = ts.Id,
                      IsSelected = ta.FlagActive,

                  }).AsQueryable(); // ทำให้ Query เป็น IQueryable

                // Apply Filters
                if (!string.IsNullOrEmpty(xModels?.OrganizationCode))
                {
                    query = query.Where(u => u.OrganizationCode == xModels.OrganizationCode);
                }

                if (!string.IsNullOrEmpty(xModels?.ApiKey))
                {
                    query = query.Where(u => u.ApiKey == xModels.ApiKey);
                }

                if (query.ToList().Count>0)
                {
                    //check all api
                    var queryCheckdata = (
                 from ts in _context.TSystemApiMappings
                 join ms in _context.MSystems
                     on ts.OwnerSystemCode equals ms.SystemCode

                 select new TApiPermisionMappingModels
                 {
                     SystemCode = ms.SystemCode,
                     SystemName = ms.SystemName,
                     ServiceName = ts.ApiServiceName,
                     SystemApiMappingId = ts.Id,


                 }).AsQueryable(); // ทำให้ Query เป็น IQueryable
                    if (queryCheckdata.ToList().Count == query.ToList().Count)
                    {
                        result = await query.ToListAsync();
                    }
                    else 
                    {

                        var bDict = query.ToList().ToDictionary(
         x => new { x.SystemCode, x.SystemApiMappingId }, x => x.FlagActive
     );

                        result = queryCheckdata.ToList().Select(a =>
                        {
                            var key = new { a.SystemCode, a.SystemApiMappingId };
                            var isSelected = bDict.TryGetValue(key, out bool? flagActive) ? flagActive : false;

                            return new TApiPermisionMappingModels
                            {
                                Id = a.Id,
                                SystemCode = a.SystemCode,
                                SystemName = a.SystemName,
                                SystemApiMappingId = a.SystemApiMappingId,
                                ServiceName = a.ServiceName,
                                OrganizationCode = a.OrganizationCode,
                                ApiKey = a.ApiKey,
                                StartDate = a.StartDate,
                                EndDate = a.EndDate,
                                FlagActive = isSelected,
                                IsSelected = isSelected // Use FlagActive as IsSelected
                            };
                        }).ToList();

                    }
                   
                }
                else 
                {
                    var queryNodata = (
                 from ts in _context.TSystemApiMappings
                 join ms in _context.MSystems
                     on ts.OwnerSystemCode equals ms.SystemCode
            
                 select new TApiPermisionMappingModels
                 {
                     SystemCode = ms.SystemCode,
                     SystemName = ms.SystemName,
                     ServiceName = ts.ApiServiceName,
                     SystemApiMappingId = ts.Id,
                     IsSelected =false,

                 }).AsQueryable(); // ทำให้ Query เป็น IQueryable
                    result = await queryNodata.ToListAsync();
                   
                }

                return result;
            }
            catch (Exception ex)
            {
                return new List<TApiPermisionMappingModels>(); // Return List เปล่าแทน null
            }
        }
    }

}
