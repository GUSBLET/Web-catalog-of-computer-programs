namespace Domain.Entities;

public class OperatingSystem
{
    public int Id { get; set; }
    public string Name { get; set; }

    public ICollection<Entities.Program> Programs { get; set; }
    
}
