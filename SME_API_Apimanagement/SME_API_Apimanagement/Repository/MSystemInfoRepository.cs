using Microsoft.EntityFrameworkCore;
using SME_API_Apimanagement.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SME_API_Apimanagement.Repository
{
    public class MSystemInfoRepository : IMSystemInfoRepository
    {
        private readonly ApiMangeDBContext _context;

        public MSystemInfoRepository(ApiMangeDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<MSystemInfo>> GetAllAsync()
        {
            try
            {
                return await _context.Set<MSystemInfo>().Where(x => x.FlagDelete == "N").ToListAsync();
            }
            catch
            {
                return new List<MSystemInfo>();
            }
        }

        public async Task<MSystemInfo?> GetByIdAsync(string sysCode)
        {
            try
            {
                return await _context.Set<MSystemInfo>().AsNoTracking().FirstOrDefaultAsync(x => x.SystemCode == sysCode);
            }
            catch
            {
                return null;
            }
        }

        public async Task<int> AddAsync(MSystemInfo entity)
        {
            try
            {
                _context.Set<MSystemInfo>().Add(entity);
                return await _context.SaveChangesAsync();
            }
            catch
            {
                return 0;
            }
        }

        public async Task<int> UpdateAsync(MSystemInfo entity)
        {
            try
            {
                _context.Set<MSystemInfo>().Update(entity);
                return await _context.SaveChangesAsync();
            }
            catch
            {
                return 0;
            }
        }

        public async Task<int> DeleteAsync(int id)
        {
            try
            {
                var item = await _context.Set<MSystemInfo>().FindAsync(id);
                if (item == null) return 0;
                item.FlagDelete = "Y";
                item.UpdateDate = DateTime.UtcNow;
                return await _context.SaveChangesAsync();
            }
            catch
            {
                return 0;
            }
        }
    }

    public interface IMSystemInfoRepository
    {
        Task<IEnumerable<MSystemInfo>> GetAllAsync();
        Task<MSystemInfo?> GetByIdAsync(string id);
        Task<int> AddAsync(MSystemInfo entity);
        Task<int> UpdateAsync(MSystemInfo entity);
        Task<int> DeleteAsync(int id);
    }
}