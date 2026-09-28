using LAB5_Fatima.Models;

namespace LAB5_Fatima.Services.Interfaces;

public interface IMatriculaService
{
    Task<IEnumerable<Matricula>> GetAllAsync();
    Task<Matricula?> GetByIdAsync(int id);

    Task<Matricula> MatricularEstudianteAsync(int idEstudiante, int idCurso, string semestre);

    Task<bool> UpdateAsync(int id, Matricula entity);
    Task<bool> DeleteAsync(int id);

    Task<IEnumerable<Matricula>> GetMatriculadosPorCursoAsync(int idCurso);
}
