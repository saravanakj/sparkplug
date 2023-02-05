namespace SparkPlug.Contracts;

public enum Direction { Ascending, Descending }

public interface IOrder
{
    string Field { get; set; }
    Direction Direction { get; set; }
}
