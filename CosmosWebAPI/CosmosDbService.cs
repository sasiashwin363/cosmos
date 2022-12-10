using CosmosWebAPI.Models;
using Microsoft.Azure.Cosmos;

namespace CosmosWebAPI
{
    public class CosmosDbService : ICosmosDbService
    {
        private Container _container;

        public CosmosDbService(CosmosClient cosmosClient, string databaseName, string containerName)
        {
            _container = cosmosClient.GetContainer(databaseName, containerName);
        }


        public async Task AddAsync(Employee employee)
        {

            await _container.CreateItemAsync(employee, new PartitionKey(employee.id));
        }

        public async Task DeleteAsync(string id)
        {
            await _container.DeleteItemAsync<Employee>(id, new PartitionKey(id));
        }

        public async Task<Employee> GetAsync(string id)
        {
            try
            {
                var response = await _container.ReadItemAsync<Employee>(id, new PartitionKey(id));
                return response;
            }
            catch (CosmosException)
            {

                return null;
            }
        }

        public async Task<IEnumerable<Employee>> GetEmpAsync(int empid)
        {
            var query = _container.GetItemQueryIterator<Employee>(new QueryDefinition("SELECT * FROM EmplyeeContainer where EmplyeeContainer.EmployeeId = " + empid));
            var results = new List<Employee>();
            while (query.HasMoreResults)
            {
                var response = await query.ReadNextAsync();
                results.AddRange(response.ToList());
            }
            return results;

        }

        public async Task<IEnumerable<Employee>> GetMultipleAsync()
        {
            var query = _container.GetItemQueryIterator<Employee>(new QueryDefinition("SELECT * FROM EmplyeeContainer"));
            var results = new List<Employee>();
            while (query.HasMoreResults)
            {
                var response = await query.ReadNextAsync();
                results.AddRange(response.ToList());
            }
            return results;
        }

        public async Task UpdateAsync(string id, Employee employee)
        {

            await _container.UpsertItemAsync(employee, new PartitionKey(id));
        }
    }
}
