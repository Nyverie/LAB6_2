using LAB5_Fatima.Models;

namespace LAB5_Fatima.Services.Interfaces;

public interface IProfesorService
{
    Task<IEnumerable<Profesore>> GetAllAsync();
    Task<Profesore?> GetByIdAsync(int id);
    Task<Profesore> CreateAsync(Profesore entity);
    Task<bool> UpdateAsync(int id, Profesore entity);
    Task<bool> DeleteAsync(int id);
}