using System.Reflection;
using Microsoft.Extensions.Options;
using VotingApp.Application.Features.Queries.Voter;

namespace VotingApp.API;

public static class ServiceRegistrationExtensions
{
    public static OptionsBuilder<TOptions> AddValidatedOptions<TOptions>(
        this IServiceCollection services, string sectionName) where TOptions : class =>
        services.AddOptions<TOptions>()
            .BindConfiguration(sectionName)
            .ValidateDataAnnotations();


    public static void AddMediator(this IServiceCollection services) => 
        services.AddMediatR(cfg => 
            cfg.RegisterServicesFromAssembly(typeof(GetAllVotersQueryHandler).Assembly));
}