using CosmosWebAPI.Models;

namespace CosmosWebAPI
{
    public interface ICosmosDbService
    {
        Task<IEnumerable<Employee>> GetMultipleAsync();
        Task<Employee> GetAsync(string id);
        Task<IEnumerable<Employee>> GetEmpAsync(int id);
        Task AddAsync(Employee employee);
        Task UpdateAsync(string id, Employee employee);
        Task DeleteAsync(string id);
    }
}
