namespace Domain.Entities;

public class Program
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public bool License { get; set; }
    public DateOnly ReleaseDate { get; set; }
    public string Version { get; set; }
    public string Weight { get; set; }
    public bool UsingConnection { get; set;}
    public bool MultiUserMode { get; set; }
    public byte[] Logo { get; set; }
    public bool CrossPlatform { get; set; }

    public Company Company { get; set; }
    public Type Type { get; set; }

    public ICollection<SystemRequirement> Requirements { get; set; }
    public ICollection<OperatingSystem> OperatingSystems { get; set; }

}
