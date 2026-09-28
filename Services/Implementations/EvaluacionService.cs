using LAB5_Fatima.Models;
using LAB5_Fatima.Services.Interfaces;
using LAB5_Fatima.Repositories.Interfaces;

namespace LAB5_Fatima.Services.Implementations;

public class EvaluacionService : IEvaluacionService
{
    private readonly IUnitOfWork _unitOfWork;

    public EvaluacionService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<Evaluacione>> GetAllAsync()
        => await _unitOfWork.Repository<Evaluacione>().GetAllAsync();

    public async Task<Evaluacione?> GetByIdAsync(int id)
        => await _unitOfWork.Repository<Evaluacione>().GetByIdAsync(id);

    public async Task<Evaluacione> CreateAsync(Evaluacione entity)
    {
        await _unitOfWork.Repository<Evaluacione>().InsertAsync(entity);
        await _unitOfWork.Complete();
        return entity;
    }

    public async Task<bool> UpdateAsync(int id, Evaluacione entity)
    {
        var existing = await _unitOfWork.Repository<Evaluacione>().GetByIdAsync(id);
        if (existing == null) return false;

        existing.IdEstudiante = entity.IdEstudiante;
        existing.IdCurso = entity.IdCurso;
        existing.Calificacion = entity.Calificacion;
        existing.Fecha = entity.Fecha;

        await _unitOfWork.Complete();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var existing = await _unitOfWork.Repository<Evaluacione>().GetByIdAsync(id);
        if (existing == null) return false;

        _unitOfWork.Repository<Evaluacione>().Delete(existing);
        await _unitOfWork.Complete();
        return true;
    }
}