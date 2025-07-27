using System.ComponentModel.DataAnnotations;

namespace Foramag.Models
{
    public class V_Marque
    {
        [Key]
        public int ItmsGrpCod { get; set; }
        public string ItmsGrpNam { get; set; }
    }
}