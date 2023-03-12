namespace Domain.Entities;

public class SystemRequirement
{
    public int Id { get; set; }
    public string Name { get; set; }

    public ICollection<Program> Programs { get; set; }
}
