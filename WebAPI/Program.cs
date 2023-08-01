
using LinqToDB;
using LinqToDB.AspNet;
using LinqToDB.AspNet.Logging;
using Microsoft.AspNetCore.OData;
using Microsoft.EntityFrameworkCore;
using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;
using OpenTracer.Business;
using OpenTracer.Core.Abstraction;
using OpenTracer.Core.Entities;
using OpenTracer.Infra;

namespace WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddDbContext<AppDbContext>((options) =>
            {
                options.UseNpgsql(builder.Configuration["OpenTrace:DefaultConnection"]);
            });
            builder.Services.AddLinqToDBContext<AppDbConnection>((provider, options)
            => options
                //will configure the AppDataConnection to use
                //sqite with the provided connection string
                //there are methods for each supported database
                .UsePostgreSQL(builder.Configuration["OpenTrace:DefaultConnection"])
                //default logging will log everything using the ILoggerFactory configured in the provider
                .UseDefaultLogging(provider));
            builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            builder.Services.AddControllers().AddJsonOptions(opt =>
            {
                opt.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
            }).AddOData(
    options => options.EnableQueryFeatures(100).AddRouteComponents(
    "odata",
        EdmModel()));
            builder.Services.AddScoped<ITraceService, TraceService>();
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

        public static IEdmModel EdmModel()
        {
            var modelBuilder = new ODataConventionModelBuilder();
            modelBuilder.EntitySet<Traces>("Attachment");
            return modelBuilder.GetEdmModel();
        }
    }
}