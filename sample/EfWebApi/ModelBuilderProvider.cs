namespace SparkPlug.Sample.Api;

public class ModelBuilderProvider : IModelBuilderProvider
{
    public void Build(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Person>().ToTable("Person");
        modelBuilder.Entity<Person>().HasKey(e => e.Id);
        modelBuilder.Entity<Person>().Property(e => e.PersonName).HasMaxLength(50);
        modelBuilder.Entity<Person>().Property(e => e.Department).HasMaxLength(50);
        modelBuilder.Entity<Person>().Property(e => e.Salary);
        modelBuilder.Entity<Person>().Property(e => e.MobileNo);
        modelBuilder.Entity<Person>().Property(e => e.AddressId);

        modelBuilder.Entity<Person>().HasOne(p => p.Address)
        .WithOne(a => a.Person).HasForeignKey<Person>(x => x.AddressId);

        modelBuilder.Entity<Address>().ToTable("Address");
        modelBuilder.Entity<Address>().HasKey(e => e.Id);
        modelBuilder.Entity<Address>().Property(e => e.FlatNo).HasMaxLength(50);
        modelBuilder.Entity<Address>().Property(e => e.Street).HasMaxLength(50);
        modelBuilder.Entity<Address>().Property(e => e.State).HasMaxLength(50);
        modelBuilder.Entity<Address>().Property(e => e.Country).HasMaxLength(50);
        modelBuilder.Entity<Address>().Property(e => e.Pin).HasMaxLength(50);
    }
}