using System.ComponentModel.DataAnnotations;

namespace Foramag.Models
{
    public class Article
    {
        [Key]
        public string ItemCode { get; set; }
        public string ItemName { get; set; }
        public string Marque { get; set; }
        public decimal Prix_Public { get; set; }
        public int Mag01 { get; set; }
        public int Mag02 { get; set; }
        public int Mag03 { get; set; }
        public int Stock => Mag01 + Mag02 + Mag03;
    }
}