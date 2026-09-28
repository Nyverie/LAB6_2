using Microsoft.AspNetCore.Mvc;
using LAB5_Fatima.Models;
using LAB5_Fatima.Services.Interfaces;

namespace LAB5_Fatima.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProfesoresController : ControllerBase
{
    private readonly IProfesorService _service;

    public ProfesoresController(IProfesorService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Profesore>>> GetAll()
        => Ok(await _service.GetAllAsync());

    [HttpGet("{id}")]
    public async Task<ActionResult<Profesore>> GetById(int id)
    {
        var item = await _service.GetByIdAsync(id);
        if (item == null) return NotFound();
        return Ok(item);
    }

    [HttpPost]
    public async Task<ActionResult<Profesore>> Create([FromBody] Profesore entity)
    {
        var created = await _service.CreateAsync(entity);
        return CreatedAtAction(nameof(GetById), new { id = created.IdProfesor }, created);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] Profesore entity)
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