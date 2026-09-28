using LAB5_Fatima.Models;

namespace LAB5_Fatima.Services.Interfaces;

public interface IMateriaService
{
    Task<IEnumerable<Materia>> GetAllAsync();
    Task<Materia?> GetByIdAsync(int id);
    Task<Materia> CreateAsync(Materia entity);
    Task<bool> UpdateAsync(int id, Materia entity);
    Task<bool> DeleteAsync(int id);
}
