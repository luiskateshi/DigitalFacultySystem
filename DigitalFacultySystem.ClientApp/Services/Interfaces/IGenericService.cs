using DigitalFacultySystem.Domain.Entities;
using DigitalFacultySystem.Entities.Dtos.RequestResponse;

namespace DigitalFacultySystem.ClientApp.Services.Interfaces
{
    public interface IGenericService<T> where T : class
    {
        Task<bool> Add(T entity);
        Task<bool> Delete(Guid id);
        Task<T> GetById(Guid id);
        Task<List<T>> GetAll();
        Task<bool> Update(T entity);
    }
}
