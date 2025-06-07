using Microsoft.EntityFrameworkCore;
using SME_API_Apimanagement.Entities;
using SME_API_Apimanagement.Models;
using SME_API_Apimanagement.Services;

namespace SME_API_Apimanagement.Repository
{
    public class TSystemAPIRepository : ITSystemAPIRepository
    {
        private readonly ApiMangeDBContext _context;
        private readonly IMSystemInfoService _mSystemInfoService;

        public TSystemAPIRepository(ApiMangeDBContext context
            ,IMSystemInfoService mSystemInfoService)
        {
            _context = context;
            _mSystemInfoService = mSystemInfoService
              ;
        }

        public async Task<IEnumerable<TSystemApiMapping>> GetAllAsync() =>
            await _context.TSystemApiMappings.Where(x => x.FlagDelete == "N").ToListAsync();

        //public async Task<TSystemApiMapping> GetByIdAsync(int id) =>
        //  await _context.TSystemApiMappings
        //      .AsNoTracking() // ✅ ป้องกันปัญหา Entity Tracking ซ้ำ
        //      .FirstOrDefaultAsync(x => x.Id == id && x.FlagDelete == "N");
        public async Task<TSystemApiMapping> GetByIdAsync(int id) =>
    await _context.TSystemApiMappings.AsNoTracking()
        .FirstOrDefaultAsync(x => x.Id == id);

        public async Task AddAsync(TSystemApiMapping api)
        {
            _context.TSystemApiMappings.Add(api);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(TSystemApiMapping api)
        {
            int result = 0;
            try
            {
                _context.TSystemApiMappings.Update(api);
                result = await _context.SaveChangesAsync();

            }
            catch (Exception ex)
            {

            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var api = await _context.TSystemApiMappings.FindAsync(id);
                if (api != null)
                {

                    _context.TSystemApiMappings.Remove(api);
                    await _context.SaveChangesAsync();
                 
                }
                return true;
            }
            catch 
            {
                return false;
            }
        

            
        }
        public async Task<int> UpsertSystemApi(UpSertSystemApiModels xModels)
        {
            int success = 0;
            try
            {
                var result = await GetByIdAsync(xModels.TSystemAPI.Id); // เรียกใช้ await

                if (result != null)
                {
                    var xRaw = new TSystemApiMapping
                    {
                        Id = result.Id,
                        OwnerSystemCode = xModels.TSystemAPI.OwnerSystemCode,
                        ApiServiceName = xModels.TSystemAPI.ApiServiceName,
                        ApiMethod = xModels.TSystemAPI.ApiMethod,
                        //ApiUrlProd = xModels.TSystemAPI.ApiUrlProd,
                        //ApiUrlUat = xModels.TSystemAPI.ApiUrlUat,
                        ApiKey = xModels.TSystemAPI.ApiKey,
                        ApiRequestExample = xModels.TSystemAPI.ApiRequestExample,
                        ApiResponseExample = xModels.TSystemAPI.ApiResponseExample,
                        ApiNote = xModels.TSystemAPI.ApiNote,
                        FlagActive = true,
                        FlagDelete = "N",
                        UpdateDate = DateTime.Now,
                        UpdateBy = xModels.TSystemAPI.CreateBy
                        ,
                        CreateDate = DateTime.Now
                        ,
                        CreateBy = xModels.TSystemAPI.CreateBy
                        ,ApiPassword = xModels.TSystemAPI.ApiPassword
                        ,ApiServiceType = xModels.TSystemAPI.ApiServiceType
                        ,ApiUrlProdInbound = xModels.TSystemAPI.ApiUrlProdInbound
                        ,ApiUrlProdOutbound = xModels.TSystemAPI.ApiUrlProdOutbound
                        ,ApiUrlUatInbound = xModels.TSystemAPI.ApiUrlUatInbound
                        ,ApiUrlUatOutbound = xModels.TSystemAPI.ApiUrlUatOutbound
                        ,ApiUser = xModels.TSystemAPI.ApiUser
                        ,ApiResponseDescription = xModels.TSystemAPI.ApiResponseDescription
                        ,ApiRequestDescription = xModels.TSystemAPI.ApiRequestDescription
                        ,ApiRequestParamater = xModels.TSystemAPI.ApiRequestParamater
                        ,ApiRequestParamaterType = xModels.TSystemAPI.ApiRequestParamaterType
                        ,ApiResponseParamater = xModels.TSystemAPI.ApiResponseParamater
                        ,ApiResponseParamaterType = xModels.TSystemAPI.ApiResponseParamaterType
                       
                        ,EndPoint = xModels.TSystemAPI.EndPoint
                        , ApiServiceCode = xModels.TSystemAPI.ApiServiceCode
                    };
                    var updatex = UpdateAsync(xRaw);
                }
                else
                {
                    var xRaw = new TSystemApiMapping
                    {
                        OwnerSystemCode = xModels.TSystemAPI.OwnerSystemCode,
                        ApiServiceName = xModels.TSystemAPI.ApiServiceName,
                        ApiMethod = xModels.TSystemAPI.ApiMethod,
                        //ApiUrlProd = xModels.TSystemAPI.ApiUrlProd,
                        //ApiUrlUat = xModels.TSystemAPI.ApiUrlUat,
                        ApiKey = xModels.TSystemAPI.ApiKey,
                        ApiRequestExample = xModels.TSystemAPI.ApiRequestExample,
                        ApiResponseExample = xModels.TSystemAPI.ApiResponseExample,
                        ApiNote = xModels.TSystemAPI.ApiNote,
                        FlagActive = true,
                        FlagDelete = "N",
                        UpdateDate = DateTime.Now,
                        CreateDate = DateTime.Now,
                        CreateBy = xModels.TSystemAPI.CreateBy,
                        UpdateBy = xModels.TSystemAPI.CreateBy
                        ,ApiPassword = xModels.TSystemAPI.ApiPassword
                        ,ApiServiceType = xModels.TSystemAPI.ApiServiceType
                        ,
                        ApiUrlProdInbound = xModels.TSystemAPI.ApiUrlProdInbound
                        ,ApiUrlProdOutbound = xModels.TSystemAPI.ApiUrlProdOutbound
                        ,ApiUrlUatInbound = xModels.TSystemAPI.ApiUrlUatInbound
                        , EndPoint = xModels.TSystemAPI.EndPoint
                        ,ApiUrlUatOutbound = xModels.TSystemAPI.ApiUrlUatOutbound
                        ,ApiUser = xModels.TSystemAPI.ApiUser
                        ,ApiResponseDescription = xModels.TSystemAPI.ApiResponseDescription
                        ,ApiRequestDescription = xModels.TSystemAPI.ApiRequestDescription
                        ,ApiRequestParamater = xModels.TSystemAPI.ApiRequestParamater
                        ,ApiRequestParamaterType = xModels.TSystemAPI.ApiRequestParamaterType
                        ,ApiResponseParamater = xModels.TSystemAPI.ApiResponseParamater
                        ,ApiResponseParamaterType = xModels.TSystemAPI.ApiResponseParamaterType
                 ,ApiServiceCode = xModels.TSystemAPI.ApiServiceCode

                    };

                    _context.TSystemApiMappings.Add(xRaw);
                    await _context.SaveChangesAsync();
                    success = xRaw.Id; // ดึงค่า Id หลัง Save


                    success = await _context.SaveChangesAsync(); // เรียก SaveChangesAsync ครั้งเดียว
                }

            }
            catch (Exception ex)
            {
                success = 0; // ถ้า Error ให้ return 0
            }

            return success;
        }
    
        public async Task<List<TSystemApiMappingModels>> GetTSystemMappingBySearch(TSystemApiMappingModels xModels)
        {
            try
            {
                var query = (from r in _context.TSystemApiMappings
                             join s in _context.MSystems on r.OwnerSystemCode equals s.SystemCode
                             where r.FlagDelete =="N"
                             select new TSystemApiMappingModels
                             {
                                 Id = r.Id,
                                 ApiMethod = r.ApiMethod,
                                 OwnerSystemCode = r.OwnerSystemCode,
                                 ApiNote = r.ApiNote,
                                 FlagActive = r.FlagActive,
                                 ApiRequestExample = r.ApiRequestExample,
                                 ApiResponseExample = r.ApiResponseExample,
                                 ApiServiceName = r.ApiServiceName,
                               
                                 CreateBy = r.CreateBy,
                                 FlagDelete = r.FlagDelete,
                                 UpdateBy = r.UpdateBy,
                                 ApiKey = r.ApiKey,
                                 CreateDate = r.CreateDate,
                                 UpdateDate = r.UpdateDate,
                                   ApiPassword = r.ApiPassword,
                                    ApiServiceType = r.ApiServiceType,
                                     ApiUrlProdInbound = r.ApiUrlProdInbound
                                     , ApiUrlProdOutbound = r.ApiUrlProdOutbound
                                      ,ApiUrlUatInbound = r.ApiUrlUatInbound
                                      ,ApiUrlUatOutbound = r.ApiUrlUatOutbound
                                      , ApiUser = r.ApiUser
                                      , ApiResponseDescription = r.ApiResponseDescription
                                      , ApiRequestDescription = r.ApiRequestDescription
                                      ,ApiRequestParamater = r.ApiRequestParamater
                                      ,ApiRequestParamaterType = r.ApiRequestParamaterType
                                      ,ApiResponseParamater = r.ApiResponseParamater
                                      ,ApiResponseParamaterType = r.ApiResponseParamaterType
                                    
                                      ,EndPoint = r.EndPoint
                                      ,ApiServiceCode = r.ApiServiceCode



                             }).AsQueryable(); // ทำให้ Query เป็น IQueryable

                // Apply Filters
                if (!string.IsNullOrEmpty(xModels?.OwnerSystemCode))
                {
                    query = query.Where(u => u.OwnerSystemCode == xModels.OwnerSystemCode);
                }

                if (xModels?.Id != null && xModels?.Id !=0)
                {
                    query = query.Where(u => u.Id == xModels.Id);
                }

                return await query.ToListAsync();
            }
            catch (Exception ex)
            {
                return new List<TSystemApiMappingModels>(); // Return List เปล่าแทน null
            }
        }
      
    }

}
