using LAB5_Fatima.Models;
using LAB5_Fatima.Services.Interfaces;
using LAB5_Fatima.Repositories.Interfaces;

namespace LAB5_Fatima.Services.Implementations;

public class AsistenciaService : IAsistenciaService
{
    private readonly IUnitOfWork _unitOfWork;

    public AsistenciaService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<Asistencia>> GetAllAsync()
        => await _unitOfWork.Repository<Asistencia>().GetAllAsync();

    public async Task<Asistencia?> GetByIdAsync(int id)
        => await _unitOfWork.Repository<Asistencia>().GetByIdAsync(id);

    public async Task<Asistencia> CreateAsync(Asistencia entity)
    {
        await _unitOfWork.Repository<Asistencia>().InsertAsync(entity);
        await _unitOfWork.Complete();
        return entity;
    }

    public async Task<bool> UpdateAsync(int id, Asistencia entity)
    {
        var existing = await _unitOfWork.Repository<Asistencia>().GetByIdAsync(id);
        if (existing == null) return false;

        entity.IdAsistencia = id;
        _unitOfWork.Repository<Asistencia>().Update(entity);
        await _unitOfWork.Complete();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var existing = await _unitOfWork.Repository<Asistencia>().GetByIdAsync(id);
        if (existing == null) return false;

        _unitOfWork.Repository<Asistencia>().Delete(existing);
        await _unitOfWork.Complete();
        return true;
    }
}