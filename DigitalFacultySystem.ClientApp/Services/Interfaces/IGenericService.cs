﻿namespace DigitalFacultySystem.ClientApp.Services.Interfaces
{
    public interface IGenericService<T> where T : class
    {
        Task<bool> Add(T entity, string apiUrl);
        Task<bool> Delete(Guid id, string apiUrl);
        Task<T> GetById(Guid id, string apiUrl);
        Task<List<T>> GetAll(string apiUrl);
        Task<bool> Update(T entity, string apiUrl);
    }
}
