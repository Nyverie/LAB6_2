using LAB5_Fatima.Models;
using LAB5_Fatima.Services.Interfaces;
using LAB5_Fatima.Repositories.Interfaces;

namespace LAB5_Fatima.Services.Implementations;

public class ProfesorService : IProfesorService
{
    private readonly IUnitOfWork _unitOfWork;

    public ProfesorService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<Profesore>> GetAllAsync()
        => await _unitOfWork.Repository<Profesore>().GetAllAsync();

    public async Task<Profesore?> GetByIdAsync(int id)
        => await _unitOfWork.Repository<Profesore>().GetByIdAsync(id);

    public async Task<Profesore> CreateAsync(Profesore entity)
    {
        await _unitOfWork.Repository<Profesore>().InsertAsync(entity);
        await _unitOfWork.Complete();
        return entity;
    }

    public async Task<bool> UpdateAsync(int id, Profesore entity)
    {
        var existing = await _unitOfWork.Repository<Profesore>().GetByIdAsync(id);
        if (existing == null) return false;

        entity.IdProfesor = id;
        _unitOfWork.Repository<Profesore>().Update(entity);
        await _unitOfWork.Complete();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var existing = await _unitOfWork.Repository<Profesore>().GetByIdAsync(id);
        if (existing == null) return false;

        _unitOfWork.Repository<Profesore>().Delete(existing);
        await _unitOfWork.Complete();
        return true;
    }
}