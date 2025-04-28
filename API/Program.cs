
using Core.Repositories.Contract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Repository;
using Repository.Data;
using Project.APIS.Extensions;
using Project.APIS.MiddleWare;
using Repository.Data.Identity;
using Core.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Project.Repository.Data.Identity;

namespace API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<StoreContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));

            });
            builder.Services.AddDbContext<AppIdentityDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("IdentityConnection"));

            });
            builder.Services.AddApplicationServices(); //Extensions Methods

            builder.Services.AddIdentity<AppUser, IdentityRole>(Options =>
            {

            }).AddEntityFrameworkStores<AppIdentityDbContext>();

            var app = builder.Build();

            // update database
            using var scop = app.Services.CreateScope();
            var Services = scop.ServiceProvider;
            var _dbcontext = Services.GetRequiredService<StoreContext>(); // Ask CLR Explicitly StoreContext
            var _IdentityDbContext = Services.GetRequiredService<AppIdentityDbContext>(); // Explicitly AppIdentityDbContext
            var _userManger = Services.GetRequiredService<UserManager<AppUser>>();

            var LoggerFactory = Services.GetRequiredService<ILoggerFactory>();
            try
            {
                await _dbcontext.Database.MigrateAsync();// update database
                await _IdentityDbContext.Database.MigrateAsync(); // update Identity database
                await AppIdentityDbContextSeed.SeedUserAsync(_userManger);


            }
            catch (Exception ex)
            {
                var logger = LoggerFactory.CreateLogger<Program>();
                logger.LogError(ex, "an error occurred during migration");
            }

            app.UseMiddleware<ExcptionMiddleWare>();
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseStatusCodePagesWithReExecute("/Errors/{0}");

            app.UseHttpsRedirection();

            app.UseAuthorization(); 

            app.MapControllers();

            app.Run();
        }
    }
}
