using Microsoft.EntityFrameworkCore;
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
                    foreach (var item in xModels.LSystem)
                    {
                        var xRaw = new TApiPermisionMapping
                        {
                            StartDate = xModels.MRegister.StartDate,
                            EndDate = xModels.MRegister.EndDate,
                            OrganizationCode = xModels.MRegister.OrganizationCode,
                            SystemCode = item.SystemCode,
                            ApiKey = apiKey,
                            FlagActive = item.IsSelected,
                            FlagDelete = "N",
                            UpdateDate = DateTime.Now,
                            CreateDate = DateTime.Now,
                            CreateBy = xModels.MRegister.CreateBy,
                            UpdateBy = xModels.MRegister.CreateBy
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
            try
            {
                var query = (from r in _context.TApiPermisionMappings
                             join s in _context.MSystems on r.SystemCode equals s.SystemCode
                             select new TApiPermisionMappingModels
                             {
                                 Id = r.Id,
                                 ApiSystemCode = r.SystemCode,
                                 SystemCode = r.SystemCode,
                                 CreateBy = r.CreateBy,
                                 EndDate = r.EndDate,
                                 FlagActive = r.FlagActive,
                                 FlagDelete = r.FlagDelete,
                                 OrganizationCode = r.OrganizationCode,
                                 StartDate = r.StartDate,
                                 UpdateBy = r.UpdateBy,
                                 ApiKey = r.ApiKey,
                                 CreateDate = r.CreateDate,
                                 UpdateDate = r.UpdateDate,
                                 SystemName = s.SystemName,
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

                return await query.ToListAsync();
            }
            catch (Exception ex)
            {
                return new List<TApiPermisionMappingModels>(); // Return List เปล่าแทน null
            }
        }
    }

}
