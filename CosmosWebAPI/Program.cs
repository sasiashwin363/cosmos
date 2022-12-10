using Newtonsoft.Json.Serialization;

namespace CosmosWebAPI
{
    public class Program
    {
        
        private static async Task<CosmosDbService> InitializeCosmosClientInstanceAsync(IConfigurationSection configurationSection)
        {
            var databaseName = configurationSection["DatabaseName"];
            var containerName = configurationSection["ContainerName"];
            var account = configurationSection["Account"];
            var key = configurationSection["Key"];
            var client = new Microsoft.Azure.Cosmos.CosmosClient(account, key);
            var database = await client.CreateDatabaseIfNotExistsAsync(databaseName);
            await database.Database.CreateContainerIfNotExistsAsync(containerName, "/id");
            var cosmosDbService = new CosmosDbService(client,databaseName,containerName);
            return cosmosDbService;

        }
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            //var connectionstring = builder.Configuration.GetConnectionString("EmpDbCon");
            // Add services to the container.
            builder.Services.AddSingleton<ICosmosDbService>(InitializeCosmosClientInstanceAsync(builder.Configuration.GetSection("CosmosDb")).GetAwaiter().GetResult());
            builder.Services.AddControllersWithViews().AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore).AddNewtonsoftJson(options => options.SerializerSettings.ContractResolver = new DefaultContractResolver());
            //builder.Services.AddDbContext<EmployeeDBContext>(options => options.UseSqlServer(connectionstring));
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}