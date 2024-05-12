namespace DigitalFacultySystem.Client.Services.Interfaces
{
    public interface IGenericService<T> where T : class
    {
        Task<bool> Add(T entity, string apiUrl);
        Task<bool> Delete(Guid id, string apiUrl);
        Task<T> GetById(Guid id, string apiUrl);
        Task<List<T>> GetAll(string apiUrl);
        Task<List<T>> GetAllById(Guid? id, string apiUrl);
        Task<bool> Update(T entity, string apiUrl);
        Task<bool> UpdateList(IEnumerable<T> entities, string apiUrl);
        Task<bool> ExecuteProcess(string apiUrl);
        Task<bool> ExecuteProcessById(Guid Id, string apiUrl);



    }
}
