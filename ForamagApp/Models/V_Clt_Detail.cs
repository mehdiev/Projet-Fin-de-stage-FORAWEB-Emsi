using System.ComponentModel.DataAnnotations;

namespace Foramag.Models
{
    public class V_Clt_Detail
    {
        [Key]
        public string CardCode { get; set; }
        public string CardName { get; set; }
        public string Address { get; set; }
        public string SlpName { get; set; }
        public string PymntGroup { get; set; }
    }
}