using LAB5_Fatima.Models;
using LAB5_Fatima.Services.Interfaces;
using LAB5_Fatima.Repositories.Interfaces;

namespace LAB5_Fatima.Services.Implementations;

public class EstudianteService : IEstudianteService
{
    private readonly IUnitOfWork _unitOfWork;

    public EstudianteService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<Estudiante>> GetAllAsync()
        => await _unitOfWork.Repository<Estudiante>().GetAllAsync();

    public async Task<Estudiante?> GetByIdAsync(int id)
        => await _unitOfWork.Repository<Estudiante>().GetByIdAsync(id);

    public async Task<Estudiante> CreateAsync(Estudiante entity)
    {
        await _unitOfWork.Repository<Estudiante>().InsertAsync(entity);
        await _unitOfWork.Complete();
        return entity;
    }

    public async Task<bool> UpdateAsync(int id, Estudiante entity)
    {
        var existing = await _unitOfWork.Repository<Estudiante>().GetByIdAsync(id);
        if (existing == null) return false;

        entity.IdEstudiante = id;
        _unitOfWork.Repository<Estudiante>().Update(entity);
        await _unitOfWork.Complete();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var existing = await _unitOfWork.Repository<Estudiante>().GetByIdAsync(id);
        if (existing == null) return false;

        _unitOfWork.Repository<Estudiante>().Delete(existing);
        await _unitOfWork.Complete();
        return true;
    }
}