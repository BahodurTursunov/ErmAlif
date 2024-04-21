using Erm.src.Erm.BusinessLayer;
using Erm.src.Erm.BusinessLayer.Mapper;
using Erm.src.Erm.BusinessLayer.Services;
using Erm.src.Erm.BusinessLayer.Validators;
using Erm.src.Erm.DataAccess;
using Erm.src.Erm.DataAccess.Repositories;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Project_ERM.src.Erm.DataAccess;

namespace Erm.PresentationWebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddAutoMapper(options
                => options.AddProfile<RiskProfileInfoProfile>());

            builder.Services.AddDbContext<ErmDbContext>(con => con.UseSqlServer("server=localhost;integrated security=True; database=ErmDatabase;TrustServerCertificate=true;"));

            builder.Services.AddStackExchangeRedisCache(options
                => options.Configuration = builder.Configuration.GetConnectionString("RedisConnection"));

            builder.Services.AddScoped<RiskProfileRepository>();
            builder.Services.AddScoped<RiskProfileRepositoryProxy>();
            builder.Services.AddScoped<IRiskProfileService, RiskProfileService>();

            builder.Services.AddScoped<IValidator<RiskProfileInfo>, RiskProfileInfoValidator>();
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

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
