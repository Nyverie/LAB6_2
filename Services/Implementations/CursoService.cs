using LAB5_Fatima.Models;
using LAB5_Fatima.Services.Interfaces;
using LAB5_Fatima.Repositories.Interfaces;

namespace LAB5_Fatima.Services.Implementations;

public class CursoService : ICursoService
{
    private readonly IUnitOfWork _unitOfWork;

    public CursoService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<Curso>> GetAllAsync()
        => await _unitOfWork.Repository<Curso>().GetAllAsync();

    public async Task<Curso?> GetByIdAsync(int id)
        => await _unitOfWork.Repository<Curso>().GetByIdAsync(id);

    public async Task<Curso> CreateAsync(Curso entity)
    {
        await _unitOfWork.Repository<Curso>().InsertAsync(entity);
        await _unitOfWork.Complete();
        return entity;
    }

    public async Task<bool> UpdateAsync(int id, Curso entity)
    {
        var existing = await _unitOfWork.Repository<Curso>().GetByIdAsync(id);
        if (existing == null) return false;

        entity.IdCurso = id;
        _unitOfWork.Repository<Curso>().Update(entity);
        await _unitOfWork.Complete();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var existing = await _unitOfWork.Repository<Curso>().GetByIdAsync(id);
        if (existing == null) return false;

        _unitOfWork.Repository<Curso>().Delete(existing);
        await _unitOfWork.Complete();
        return true;
    }
}