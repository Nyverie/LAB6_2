using LAB5_Fatima.Models;

namespace LAB5_Fatima.Services.Interfaces;

public interface IEstudianteService
{
    Task<IEnumerable<Estudiante>> GetAllAsync();
    Task<Estudiante?> GetByIdAsync(int id);
    Task<Estudiante> CreateAsync(Estudiante entity);
    Task<bool> UpdateAsync(int id, Estudiante entity);
    Task<bool> DeleteAsync(int id);
}
