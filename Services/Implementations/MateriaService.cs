using LAB5_Fatima.Models;
using LAB5_Fatima.Services.Interfaces;
using LAB5_Fatima.Repositories.Interfaces;

namespace LAB5_Fatima.Services.Implementations;

public class MateriaService : IMateriaService
{
    private readonly IUnitOfWork _unitOfWork;

    public MateriaService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<Materia>> GetAllAsync()
        => await _unitOfWork.Repository<Materia>().GetAllAsync();

    public async Task<Materia?> GetByIdAsync(int id)
        => await _unitOfWork.Repository<Materia>().GetByIdAsync(id);

    public async Task<Materia> CreateAsync(Materia entity)
    {
        await _unitOfWork.Repository<Materia>().InsertAsync(entity);
        await _unitOfWork.Complete();
        return entity;
    }

    public async Task<bool> UpdateAsync(int id, Materia entity)
    {
        var existing = await _unitOfWork.Repository<Materia>().GetByIdAsync(id);
        if (existing == null) return false;

        entity.IdMateria = id;
        _unitOfWork.Repository<Materia>().Update(entity);
        await _unitOfWork.Complete();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var existing = await _unitOfWork.Repository<Materia>().GetByIdAsync(id);
        if (existing == null) return false;

        _unitOfWork.Repository<Materia>().Delete(existing);
        await _unitOfWork.Complete();
        return true;
    }
}