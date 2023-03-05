namespace SparkPlug.Contracts;

public enum UnaryOperator
{
    IsNull,
    IsNotNull
}

public class UnaryFilter : ConditionFilter, IUnaryFilter
{
    public UnaryFilter(string field, UnaryOperator op) : base(field, FilterType.Unary)
    {
        Op = op;
        Field = field;
    }
    public UnaryOperator Op { get; set; }
}
