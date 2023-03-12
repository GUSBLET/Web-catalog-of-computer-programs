using Microsoft.AspNetCore.Http;

namespace Domain.DTO
{
    public class ModelOfItemDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string DescriptionOfCompany { get; set; }
        public string Company { get; set; }
        public string Type { get; set; }
        public bool License { get; set; }
        public DateOnly ReleaseDate { get; set; }
        public string Weight { get; set; }
        public string Version { get; set; }
        public bool UsingConnection { get; set; }
        public bool MultiUserMode { get; set; }
        public IFormFile Logo { get; set; }
        public bool CrossPlatform { get; set; }
        public string OperatingSystems { get; set; }
        public string Requirements { get; set; }

    }

}
