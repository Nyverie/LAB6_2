using LAB5_Fatima.Models;

namespace LAB5_Fatima.Services.Interfaces;

public interface ICursoService
{
    Task<IEnumerable<Curso>> GetAllAsync();
    Task<Curso?> GetByIdAsync(int id);
    Task<Curso> CreateAsync(Curso entity);
    Task<bool> UpdateAsync(int id, Curso entity);
    Task<bool> DeleteAsync(int id);
}
