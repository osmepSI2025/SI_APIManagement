using Microsoft.AspNetCore.Mvc;
using SME_API_Apimanagement.Entities;
using SME_API_Apimanagement.Models;
using SME_API_Apimanagement.Repository;

namespace SME_API_Apimanagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TSystemAPIController : ControllerBase
    {
        private readonly ITSystemAPIRepository _repository;

        public TSystemAPIController(ITSystemAPIRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var apis = await _repository.GetAllAsync();
            return Ok(apis);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var api = await _repository.GetByIdAsync(id);
            return api != null ? Ok(api) : NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TSystemApiMapping api)
        {
            api.FlagDelete = "N";
            api.CreateDate = DateTime.UtcNow;
            await _repository.AddAsync(api);
            return CreatedAtAction(nameof(GetById), new { id = api.Id }, api);
        }
        // 📌 POST: เพิ่มข้อมูล
        [HttpPost]
        [Route("UpsertSystemApi")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<int>> UpsertSystemApi(UpSertSystemApiModels xModels)
        {
            try
            {
                int xdata = await _repository.UpsertSystemApi(xModels); // ใช้ await
                
                return Ok(xdata); // คืนค่า 200 พร้อมข้อมูล
            }
            catch (Exception ex)
            {
                return BadRequest(); // ถ้ามีข้อผิดพลาดให้คืน 400
            }
        }

   
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] TSystemApiMapping api)
        {
            var existing = await _repository.GetByIdAsync(id);
            if (existing == null) return NotFound();

            existing.OwnerSystemCode = api.OwnerSystemCode;
            existing.ApiServiceName = api.ApiServiceName;
            existing.ApiMethod = api.ApiMethod;
            existing.ApiUrlProd = api.ApiUrlProd;
            existing.ApiUrlUat = api.ApiUrlUat;
            existing.ApiKey = api.ApiKey;
            existing.ApiRequestExample = api.ApiRequestExample;
            existing.ApiResponseExample = api.ApiResponseExample;
            existing.ApiNote = api.ApiNote;
            existing.FlagActive = api.FlagActive;
            existing.UpdateBy = api.UpdateBy;
            existing.UpdateDate = DateTime.UtcNow;

            await _repository.UpdateAsync(existing);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _repository.DeleteAsync(id);
            return NoContent();
        }

        [HttpPost]
        [Route("GetTSystemMappingBySearch")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<List<MSystemModels>>> GetRegisterBySearch(MSystemModels xModels)
        {
            try
            {
                var xdata = await _repository.GetTSystemMappingBySearch(xModels); // ใช้ await
                if (xdata == null || !xdata.Any())
                {
                    return NotFound(); // หากไม่พบข้อมูล, คืนค่า 404
                }
                return Ok(xdata); // คืนค่า 200 พร้อมข้อมูล
            }
            catch (Exception ex)
            {
                // อาจจะใส่ log หรือรายละเอียดเพิ่มเติมของข้อผิดพลาดใน ex
                return BadRequest(new { message = ex.Message }); // คืน 400 พร้อมข้อความข้อผิดพลาด
            }
        }
       
    }

}
