namespace SparkPlug.Api.Controllers;

public class GenericControllerRouteConvention : IControllerModelConvention
{
    public void Apply(ControllerModel controller)
    {
        if (controller.ControllerType.IsGenericType)
        {
            var model = controller.ControllerType.GenericTypeArguments[1];
            var attribute = model.GetCustomAttribute<ApiAttribute>();
            var controllerName = string.IsNullOrWhiteSpace(attribute?.Route) ? model.Name : attribute.Route;
            // var genericType = controller.ControllerType.GenericTypeArguments[0];
            // var customNameAttribute = genericType.GetCustomAttribute<ApiAttribute>();
            // var controllerName = string.IsNullOrWhiteSpace(customNameAttribute?.Route) ? genericType.Name : customNameAttribute.Route;
            controller.Selectors.Add(new SelectorModel
            {
                AttributeRouteModel = new AttributeRouteModel(new RouteAttribute(controllerName))
            });
            controller.ControllerName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(controllerName);
        }
    }
}
