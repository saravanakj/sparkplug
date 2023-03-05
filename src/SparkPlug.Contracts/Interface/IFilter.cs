namespace SparkPlug.Contracts;

public enum FilterType
{
    Composite,
    Field,
    Unary
}
public interface IFilter
{
    FilterType FilterType { get; set; }
}
public interface ICompositeFilter : IFilter
{
    CompositeOperator Op { get; set; }
    Filter[]? Filters { get; set; }
}

public interface IConditionFilter : IFilter
{
    string Field { get; set; }
}

public interface IFieldFilter : IConditionFilter
{
    FieldOperator Op { get; set; }
    object? Value { get; set; }
}

public interface IUnaryFilter : IConditionFilter
{
    UnaryOperator Op { get; set; }
}
