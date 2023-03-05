namespace SparkPlug.Contracts;

public enum FieldOperator
{
    Equal,
    NotEqual,
    GreaterThan,
    GreaterThanOrEqual,
    LessThan,
    LessThanOrEqual,
    In,
    NotIn
}

public class FieldFilter : ConditionFilter, IFieldFilter
{
    public FieldFilter(string field, FieldOperator op, object value) : base(field, FilterType.Field)
    {
        Op = op;
        Value = value;
    }
    public FieldOperator Op { get; set; }
    public object? Value { get; set; }
}
