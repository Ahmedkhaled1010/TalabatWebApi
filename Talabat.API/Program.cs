
using Microsoft.EntityFrameworkCore;
using Talabat.Core.Entites;
using Talabat.Core.Reposatory;
using Talabat.Reposatory;
using Talabat.Reposatory.Data;

namespace Talabat.API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            #region Configure-Service
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<TalbatDbContext>(option =>
            option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
            );
            builder.Services.AddScoped(typeof(IGenericRepository<>),typeof(GenericRepository<>));
            #endregion

            var app = builder.Build();
            #region Update Database
           using     var scope=app.Services.CreateScope();
            var services = scope.ServiceProvider;
            var LoggerFactory = services.GetRequiredService<ILoggerFactory>();
           try
            {
                var dbcontext = services.GetRequiredService<TalbatDbContext>();
                await dbcontext.Database.MigrateAsync();
              await  TalabatContextSeeding.SeedAsync(dbcontext);
            }
            catch(Exception ex)
            {
                var logger = LoggerFactory.CreateLogger<Program>();
                logger.LogError(ex, "An Error Occured During Applying The Migration");
            }
            #endregion
            // Configure the HTTP request pipeline.
            #region Configure - Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run(); 
            #endregion


        }
    }
}
