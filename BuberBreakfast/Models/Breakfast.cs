namespace BuberBreakfast.Models;

public class Breakfast
{
    // we don't want anyone to simply set values
    public Guid Id { get; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime StartDateTime { get; set; }
    public DateTime EndDateTime { get; set; }
    public DateTime LastModifiedDateTime { get; set; }
    public List<string> Savory { get; set; }
    public List<string> Sweet { get; set; }

    public Breakfast(Guid id, string name, string description, DateTime start, DateTime end, DateTime last, List<string> sav, List<string> sweet)
    {
        // We can enforce validations here
        Id = id;
        Name = name;
        Description = description;
        StartDateTime = start;
        EndDateTime = end;
        LastModifiedDateTime = last;
        Savory = sav;
        Sweet = sweet;
    }
}