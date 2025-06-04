using Microsoft.AspNetCore.Mvc;
using SME_API_Apimanagement.Entities;
using SME_API_Apimanagement.Models;
using SME_API_Apimanagement.Repository;

   [Route("api/SYS-API/[controller]")]
[ApiController]
public class MRegisterController : ControllerBase
{
    private readonly IMRegisterRepository _repository;
    private readonly ITAPIMappingRepository _repTApi;

    public MRegisterController(IMRegisterRepository repository, ITAPIMappingRepository repTApi)
    {
        _repository = repository;
        _repTApi = repTApi;
    }

    // 📌 GET: ดึงข้อมูลทั้งหมด
    [HttpGet]
    public async Task<ActionResult<IEnumerable<MRegister>>> GetRegisters()
    {
        var registers = await _repository.GetRegistersAsync();
        return Ok(registers);
    }

    // 📌 GET: ดึงข้อมูลตาม Id
    [HttpGet("{apikey}")]
    public async Task<ActionResult<MRegister>> GetRegister(string apikey)
    {
        var register = await _repository.GetRegisterByIdAsync(apikey);
        return register == null ? NotFound() : Ok(register);
    }

    // 📌 POST: เพิ่มข้อมูล
    [HttpPost]
    [Route("UpsertRegister")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    [ProducesResponseType(400)]
    public async Task<ActionResult<int>> UpsertRegister(UpSertRegisterApiModels xModels)
    {
        try
        {
            string xdata = await _repository.UpdateOrInsertRegister(xModels); // ใช้ await
            if (xdata != "")
            {
                int TApi = await _repTApi.UpdateOrInsertTApiMapping(xModels, xdata);
            }
            return Ok(200); // คืนค่า 200 พร้อมข้อมูล
        }
        catch (Exception ex)
        {
            return BadRequest(); // ถ้ามีข้อผิดพลาดให้คืน 400
        }
    }
    // 📌 POST: เพิ่มข้อมูล
    [HttpPost]
    [Route("GetRegisterBySearch")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    public async Task<ActionResult<List<MRegisterModels>>> GetRegisterBySearch(MRegisterModels xModels)
    {
        try
        {
            var xdata = await _repository.GetRegisterBySearch(xModels); // ใช้ await
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

    // 📌 PUT: อัปเดตข้อมูล
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateRegister(int id, MRegister register)
    {
        if (id != register.Id) return BadRequest();

        await _repository.UpdateRegisterAsync(register);
        return NoContent();
    }

    // 📌 DELETE: ลบข้อมูล
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteRegister(int id)
    {
        await _repository.DeleteRegisterAsync(id);
        return NoContent();
    }
}

