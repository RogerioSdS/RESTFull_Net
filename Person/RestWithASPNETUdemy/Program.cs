using EvolveDb;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;
using RestWithASPNETUdemy.Business;
using RestWithASPNETUdemy.Business.Implementations;
using RestWithASPNETUdemy.Hypermedia.Enricher;
using RestWithASPNETUdemy.Hypermedia.Filters;
using RestWithASPNETUdemy.Model.Context;
using RestWithASPNETUdemy.Repository;
using RestWithASPNETUdemy.Repository.Generic;
using Serilog;
using System.Net.Http.Headers;

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

            builder.Services.AddMvc(options =>
            {
                //é necessário incluir no header do client o content-type e o accept ambos como applicatio/xml
                options.RespectBrowserAcceptHeader = true;//opção que habilita aceitar o formato XML no cabeçalho da Request
                options.FormatterMappings.SetMediaTypeMappingForFormat("xml", MediaTypeHeaderValue.Parse("application/xml").ToString());
                options.FormatterMappings.SetMediaTypeMappingForFormat("json", MediaTypeHeaderValue.Parse("application/json").ToString());
            }).AddXmlSerializerFormatters();

            var filterOptions = new HyperMediaFilterOptions();
            filterOptions.ContentResponseEnricherList.Add(new PersonEnricher());
            filterOptions.ContentResponseEnricherList.Add(new BookEnricher());

            builder.Services.AddSingleton(filterOptions);

            //Versionamento da API
            builder.Services.AddApiVersioning();

            //Injetando dependencia
            builder.Services.AddScoped<IPersonBusiness, PersonBusinessImplementation>();
            builder.Services.AddScoped<IBookBusiness, BookBusinessImplementation>();
            builder.Services.AddScoped( typeof(IRepository<>) , typeof(GenericRepository<>));

            var app = builder.Build();

            // Configure the HTTP request pipeline.

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.MapControllerRoute("DefaultAPI", "v{version=apiVersion}/{controller=values}/{id?}");

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
