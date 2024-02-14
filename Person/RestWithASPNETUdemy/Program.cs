using EvolveDb;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;
using RestWithASPNETUdemy.Business;
using RestWithASPNETUdemy.Business.Implementations;
using RestWithASPNETUdemy.Model.Context;
using RestWithASPNETUdemy.Repository;
using RestWithASPNETUdemy.Repository.Implementations;
using Serilog;

namespace RestWithASPNETUdemy
{
    public class Program
    {
        public IWebHostEnvironment Environment { get; }
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            var connection = builder.Configuration["MySQLConnection:MySQLConnectionString"];
            builder.Services.AddDbContext<MySQLContext>(options => options.UseMySql(connection,
                new MySqlServerVersion(new Version(8, 0, 36)
                )));

            if (builder.Environment.IsDevelopment())
            {
                MigrateDatabase(connection);
            }

            //Versionamento da API
            builder.Services.AddApiVersioning();

            //Injetando dependencia
            builder.Services.AddScoped<IPersonRepository, PersonRepositoryImplementation>();
            builder.Services.AddScoped<IPersonBusiness, PersonBusinessImplementation>();
            builder.Services.AddScoped<IBookBusiness, BookBusinessImplementation>();
            builder.Services.AddScoped<IBookRepository, BookRepositoryImplementation>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();

            void MigrateDatabase(string connection)
            {
                try
                {
                    var evolveConnection = new MySqlConnection(connection);
                    var evolve = new Evolve(evolveConnection, Log.Information)
                    {
                        Locations = new List<string> { "db/migrations", "db/dataset" },
                        IsEraseDisabled = true,
                    };
                    evolve.Migrate();
                }
                catch (Exception ex)
                {
                    Log.Error("Database migration failed", ex);
                    throw;
                }
            }
        }
    }
}
