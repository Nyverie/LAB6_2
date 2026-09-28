using Microsoft.AspNetCore.Mvc;
using LAB5_Fatima.Models;
using LAB5_Fatima.Services.Interfaces;

namespace LAB5_Fatima.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MatriculasController : ControllerBase
{
    private readonly IMatriculaService _service;

    public MatriculasController(IMatriculaService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Matricula>>> GetAll()
        => Ok(await _service.GetAllAsync());

    [HttpGet("{id}")]
    public async Task<ActionResult<Matricula>> GetById(int id)
    {
        var item = await _service.GetByIdAsync(id);
        if (item == null) return NotFound();
        return Ok(item);
    }

    [HttpGet("por-curso/{idCurso}")]
    public async Task<ActionResult<IEnumerable<Matricula>>> GetPorCurso(int idCurso)
        => Ok(await _service.GetMatriculadosPorCursoAsync(idCurso));

    public record MatricularRequest(int IdEstudiante, int IdCurso, string Semestre);

    [HttpPost("matricular")]
    public async Task<ActionResult<Matricula>> Matricular([FromBody] MatricularRequest request)
    {
        try
        {
            var matricula = await _service.MatricularEstudianteAsync(
                request.IdEstudiante, request.IdCurso, request.Semestre);
            return CreatedAtAction(nameof(GetById), new { id = matricula.IdMatricula }, matricula);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { mensaje = ex.Message });
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] Matricula entity)
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
