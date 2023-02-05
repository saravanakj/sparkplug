namespace SparkPlug.Contracts;

public class Order : IOrder
{
    public Order(string field, Direction direction)
    {
        Field = field;
        Direction = direction;
    }
    public string Field { get; set; }
    public Direction Direction { get; set; }
}
