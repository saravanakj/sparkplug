namespace SparkPlug.Api.Controllers;

class GenericTypeControllerFeatureProvider : IApplicationFeatureProvider<ControllerFeature>
{
    private readonly Type ControllerType;
    public GenericTypeControllerFeatureProvider(Type controllerType)
    {
        ControllerType = controllerType;
    }
    public void PopulateFeature(IEnumerable<ApplicationPart> parts, ControllerFeature feature)
    {
        var assemblies = SwaggerServiceCollectionExtensions.CachedAssemblies;
        foreach (var candidate in assemblies.SelectMany(x => x.GetExportedTypes().Where(x => x.GetCustomAttributes<ApiAttribute>().Any())))
        {
            if (candidate.BaseType == typeof(object) || candidate.BaseType?.GenericTypeArguments.Length != 2)
            {
                throw new ArgumentException("Api attribute should be used only on IRepository<,> classes.");
            }
            var baseType = candidate.BaseType;
            var idType = baseType.GenericTypeArguments[0];
            var entityType = baseType.GenericTypeArguments[1];
            var controller = ControllerType.MakeGenericType(candidate, entityType, idType);
            feature.Controllers.Add(controller.GetTypeInfo());
        }
    }
}
