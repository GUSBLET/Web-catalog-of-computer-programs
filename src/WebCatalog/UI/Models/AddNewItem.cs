using System.ComponentModel.DataAnnotations;

namespace UI.Models;

public class AddNewItem
{
    [Required(ErrorMessage = "The name field have to be required!")]
    [Display(Name = "Enter program name:")]
    [DataType(DataType.Text)]
    public string Name { get; set; }

    [Display(Name = "Enter program description:")]
    [DataType(DataType.Text)]
    public string? Description { get; set; }

    [Required(ErrorMessage = "The company name have to be required!")]
    [Display(Name = "Enter company name:")]
    [DataType(DataType.Text)]
    public string CompanyName { get; set; }

    [Display(Name = "Enter company description:")]
    [DataType(DataType.Text)]
    public string? CompanyDescription { get; set; }

    [Required(ErrorMessage = "The program type field have to be required!")]
    [Display(Name = "Enter program type:")]
    [DataType(DataType.Text)]
    public string ProgramType { get; set; }

    [Required(ErrorMessage = "The weight of program field have to be required!")]
    [Display(Name = "Enter weight of program:")]
    [DataType(DataType.Text)]
    public string Weight { get; set; }

    [Required(ErrorMessage = "The program version field have to be required!")]
    [Display(Name = "Enter program version:")]
    [DataType(DataType.Text)]
    public string Version { get; set; }

    [Required(ErrorMessage = "The date field have to be required!")]
    [Display(Name = "Enter release date:")]
    [DataType(DataType.Date)]
    public DateOnly ReleaseDate { get; set; }

    [Required(ErrorMessage = "The needing a license field have to be required!")]
    [Display(Name = "Enter needing a license for the program:")]
    public bool License { get; set; }

    [Required(ErrorMessage = "The multi-user mode field have to be required!")]
    [Display(Name = "Enter program has multi-user mode:")]
    public bool MultiUserMode { get; set; }

    [Required(ErrorMessage = "The network connection field have to be required!")]
    [Display(Name = "Enter needing a network connection for program:")]
    public bool UsingConnection { get; set; }

    [Required(ErrorMessage = "The cross-latform field have to be required!")]
    [Display(Name = "Enter program support cross-latform:")]
    public bool CrossPlatform { get; set; }

    [Required(ErrorMessage = "The logo field have to be required!")]
    [Display(Name = "Enter logo of program:")]
    public IFormFile Logo { get; set; }

    [Required(ErrorMessage = "The date field have to be required!")]
    [Display(Name = "Enter operating systems that supported:")]
    [DataType(DataType.Text)]
    public string OperatingSystems { get; set; }

    [Required(ErrorMessage = "The date field have to be required!")]
    [Display(Name = "Enter system requirements for your program:")]
    [DataType(DataType.Text)]
    public string Requirements { get; set; }

}
