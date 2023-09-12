using AutoMapper;
using EmployeesApi.MapperProfiles;


namespace EmployeesApi;

public static class MapperExtensions
{
    public static IServiceCollection AddEmployeeMapperProfiles(this IServiceCollection services)
    {
        var mapperConfig = new MapperConfiguration(options =>
        {
            options.AddProfile<EmployeeMaps>();
            options.AddProfile<CandidateMaps>();
        });

        var mapper = mapperConfig.CreateMapper(); // IMapper


        services.AddSingleton<IMapper>(mapper);
        services.AddSingleton<MapperConfiguration>(mapperConfig);
        return services;
    }
}
