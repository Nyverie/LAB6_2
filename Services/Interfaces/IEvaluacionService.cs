using LAB5_Fatima.Models;

namespace LAB5_Fatima.Services.Interfaces;

public interface IEvaluacionService
{
    Task<IEnumerable<Evaluacione>> GetAllAsync();
    Task<Evaluacione?> GetByIdAsync(int id);
    Task<Evaluacione> CreateAsync(Evaluacione entity);
    Task<bool> UpdateAsync(int id, Evaluacione entity);
    Task<bool> DeleteAsync(int id);
}
