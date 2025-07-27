using System.ComponentModel.DataAnnotations;

namespace Foramag.Models
{
    public class Login
    {
        
        public string Username { get; set; }

        [Key]
        public int SlpCode { get; set; }
        public string Password { get; set; }
        public bool IsAdmin { get; set; }         // ✅ Role flag
    
    }
}