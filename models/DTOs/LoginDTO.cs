using System.ComponentModel.DataAnnotations;

namespace account_service.models.DTOs
{
    public class loginDTO
    {
        [Required]
        public string username {  get; set; }
        [Required]
        public string password { get; set; }
        [Required]
        public DateTime timestamp { get; set; }
    }
}
