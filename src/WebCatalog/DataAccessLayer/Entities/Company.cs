namespace DataAccessLayer.Entities;

public class Company
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }

    public ICollection<Program> Programs { get; set; }
}
