using LAB5_Fatima.Models;
using LAB5_Fatima.Services.Interfaces;
using LAB5_Fatima.Repositories.Interfaces;

namespace LAB5_Fatima.Services.Implementations;

public class MatriculaService : IMatriculaService
{
    private readonly IUnitOfWork _unitOfWork;

    public MatriculaService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<Matricula>> GetAllAsync()
        => await _unitOfWork.Repository<Matricula>().GetAllAsync();

    public async Task<Matricula?> GetByIdAsync(int id)
        => await _unitOfWork.Repository<Matricula>().GetByIdAsync(id);

    public async Task<Matricula> MatricularEstudianteAsync(int idEstudiante, int idCurso, string semestre)
    {
        var estudiante = await _unitOfWork.Repository<Estudiante>().GetByIdAsync(idEstudiante);
        if (estudiante == null)
            throw new InvalidOperationException($"No existe el estudiante con id {idEstudiante}.");

        var curso = await _unitOfWork.Repository<Curso>().GetByIdAsync(idCurso);
        if (curso == null)
            throw new InvalidOperationException($"No existe el curso con id {idCurso}.");

        var yaMatriculado = await _unitOfWork.Repository<Matricula>().ExistsAsync(m =>
            m.IdEstudiante == idEstudiante && m.IdCurso == idCurso && m.Semestre == semestre);

        if (yaMatriculado)
            throw new InvalidOperationException("El estudiante ya está matriculado en este curso para el semestre indicado.");

        var matricula = new Matricula
        {
            IdEstudiante = idEstudiante,
            IdCurso = idCurso,
            Semestre = semestre
        };

        await _unitOfWork.Repository<Matricula>().InsertAsync(matricula);
        await _unitOfWork.Complete();

        return matricula;
    }

    public async Task<bool> UpdateAsync(int id, Matricula entity)
    {
        var existing = await _unitOfWork.Repository<Matricula>().GetByIdAsync(id);
        if (existing == null) return false;

        entity.IdMatricula = id;
        _unitOfWork.Repository<Matricula>().Update(entity);
        await _unitOfWork.Complete();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var existing = await _unitOfWork.Repository<Matricula>().GetByIdAsync(id);
        if (existing == null) return false;

        _unitOfWork.Repository<Matricula>().Delete(existing);
        await _unitOfWork.Complete();
        return true;
    }

    public async Task<IEnumerable<Matricula>> GetMatriculadosPorCursoAsync(int idCurso)
        => await _unitOfWork.Repository<Matricula>().FindAsync(m => m.IdCurso == idCurso);
}
