using Microsoft.AspNetCore.Mvc;
using LAB5_Fatima.Models;
using LAB5_Fatima.Services.Interfaces;

namespace LAB5_Fatima.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AsistenciasController : ControllerBase
{
    private readonly IAsistenciaService _service;

    public AsistenciasController(IAsistenciaService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Asistencia>>> GetAll()
        => Ok(await _service.GetAllAsync());

    [HttpGet("{id}")]
    public async Task<ActionResult<Asistencia>> GetById(int id)
    {
        var item = await _service.GetByIdAsync(id);
        if (item == null) return NotFound();
        return Ok(item);
    }

    [HttpPost]
    public async Task<ActionResult<Asistencia>> Create([FromBody] Asistencia entity)
    {
        var created = await _service.CreateAsync(entity);
        return CreatedAtAction(nameof(GetById), new { id = created.IdAsistencia }, created);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] Asistencia entity)
    {
        var updated = await _service.UpdateAsync(id, entity);
        if (!updated) return NotFound();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _service.DeleteAsync(id);
        if (!deleted) return NotFound();
        return NoContent();
    }
}
