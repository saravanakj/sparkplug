namespace SparkPlug.Api;

public static class GenericTypeServiceCollectionExtensions
{
    public static IServiceCollection AddGenericTypes(this IServiceCollection services)
    {
        var assemblies = SwaggerServiceCollectionExtensions.CachedAssemblies;
        foreach (var candidate in assemblies.SelectMany(x => x.GetExportedTypes().Where(x => x.GetCustomAttributes<ApiAttribute>().Any())))
        {
            if (candidate.BaseType == typeof(object) || candidate.BaseType?.GenericTypeArguments.Length != 1)
            {
                throw new ArgumentException("Api attribute should be used only on IBaseModel<> implementation");
            }
            var baseType = candidate.BaseType;
            var idType = baseType.GenericTypeArguments[0];
            var repository = typeof(Repository<,>).MakeGenericType(idType, candidate);
            services.AddScoped(repository);
        }

        return services;
    }
}