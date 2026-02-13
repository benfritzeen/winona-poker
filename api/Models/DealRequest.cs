namespace api.Models
{
    public class DealRequest
    {
        [System.ComponentModel.DataAnnotations.Required]
        [System.ComponentModel.DataAnnotations.MinLength(2, ErrorMessage = "At least two player names are required.")]
        public List<string> PlayerNames { get; set; } = new List<string>();
    }
}
