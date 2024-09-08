using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using VotingApp.API.Options;
using VotingApp.Infrastructure.Mappings;
using VotingApp.Repository.Context;
using VotingApp.Repository.Repositories;

namespace VotingApp.API;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddMediator();
        builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);

        builder.Services.AddValidatedOptions<DatabaseOptions>("Database");

        builder.Services.AddDbContext<VotingContext>((serviceProvider, options) =>
        {
            var databaseOptions = serviceProvider.GetRequiredService<IOptions<DatabaseOptions>>().Value;
            options.UseInMemoryDatabase(databaseOptions.DbName);
        });

        builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

        builder.Services.AddControllers();

        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowAll", policyBuilder =>
                policyBuilder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
        });
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseCors("AllowAll");

        app.UseAuthorization();


        app.MapControllers();

        app.Run();
    }
}