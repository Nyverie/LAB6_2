using LAB5_Fatima.Models;

namespace LAB5_Fatima.Services.Interfaces;


public interface IAsistenciaService
{
    Task<IEnumerable<Asistencia>> GetAllAsync();
    Task<Asistencia?> GetByIdAsync(int id);
    Task<Asistencia> CreateAsync(Asistencia entity);
    Task<bool> UpdateAsync(int id, Asistencia entity);
    Task<bool> DeleteAsync(int id);
}
