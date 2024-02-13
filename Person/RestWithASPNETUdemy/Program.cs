using Microsoft.EntityFrameworkCore;
using RestWithASPNETUdemy.Model.Context;
using RestWithASPNETUdemy.Services;
using RestWithASPNETUdemy.Services.Implementations;

namespace RestWithASPNETUdemy
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            var connection = builder.Configuration["MySQLConnection:MySQLConnectionString"];
            builder.Services.AddDbContext<MySQLContext>(options => options.UseMySql(connection ,
                new MySqlServerVersion(new Version(8,0,36)
                )));

            //Versionamento da API
            builder.Services.AddApiVersioning();

            //Injetando dependencia
            builder.Services.AddScoped<IPersonService, PersonServiceImplementation>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
