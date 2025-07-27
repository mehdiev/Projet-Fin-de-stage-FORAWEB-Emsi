using System.ComponentModel.DataAnnotations;

namespace ForamagApp.Models
{
    public class V_CA_COMM
    {
        public int Year { get; set; }

        public int Month { get; set; }

        public decimal Total_HT { get; set; }

        [Key]
        public int SlpCode { get; set; }

        [StringLength(100)]
        public string Representant { get; set; }
    }
}