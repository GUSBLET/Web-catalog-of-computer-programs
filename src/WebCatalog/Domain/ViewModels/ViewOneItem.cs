namespace Domain.ViewModels;

public class ViewOneItemViewModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public string CompanyName { get; set; }
    public string? CompanyDescription { get; set; }
    public string ProgramType { get; set; }
    public string Weight { get; set; }
    public string Version { get; set; }
    public DateOnly ReleaseDate { get; set; }
    public bool License { get; set; }
    public bool MultiUserMode { get; set; }
    public bool UsingConnection { get; set; }
    public bool CrossPlatform { get; set; }
    public byte[] Logo { get; set; }
    public string OperatingSystems { get; set; }
    public string Requirements { get; set; }

}