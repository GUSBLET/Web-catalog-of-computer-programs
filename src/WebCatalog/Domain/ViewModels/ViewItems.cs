namespace Domain.ViewModels;

public class ViewItemsViewModel
{
    public int Id { get; set; }
    public string ProgramName { get; set; }
    public string CompanyName { get; set; }
    public string Type { get; set; }
    public string Version { get; set; }
    public DateOnly ReleaseDate { get; set; }
    public byte[] Logo { get; set; }

}
