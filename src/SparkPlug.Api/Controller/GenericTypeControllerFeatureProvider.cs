namespace SparkPlug.Api.Controllers;

public class GenericTypeControllerFeatureProvider : IApplicationFeatureProvider<ControllerFeature>
{
    private readonly Type _controllerType;

    public GenericTypeControllerFeatureProvider(Type controllerType)
    {
        _controllerType = controllerType;
    }
    public void PopulateFeature(IEnumerable<ApplicationPart> parts, ControllerFeature feature)
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
            // var entityType = baseType.GenericTypeArguments[1];
            // var controller = ControllerType.MakeGenericType(candidate, entityType, idType);
            var repository = typeof(Repository<,>).MakeGenericType(idType, candidate);
            var controller = _controllerType.MakeGenericType(repository, candidate, idType);
            feature.Controllers.Add(controller.GetTypeInfo());
        }
    }
}
