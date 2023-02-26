namespace SparkPlug.Sample.WebApi.Models;

[Api("person", typeof(ApiController<,>))]
public class Person : BaseEntity<long>, IConcurrencyEntity
{
    public string? PersonName { get; set; }
    public string? Department { get; set; }
    public long Salary { get; set; }
    public long AddressId { get; set; }
    public Address? Address { get; set; }
    public string? MobileNo { get; set; }
    [ConcurrencyCheck]
    public int Revision { get; set; }
}

public class Address : BaseEntity<long>, IConcurrencyEntity
{
    public string? FlatNo { get; set; }
    public string? Street { get; set; }
    public string? State { get; set; }
    public string? Country { get; set; }
    public string? Pin { get; set; }
    [JsonIgnore]
    public Person? Person { get; set; }
    [ConcurrencyCheck]
    public int Revision { get; set; }
}
