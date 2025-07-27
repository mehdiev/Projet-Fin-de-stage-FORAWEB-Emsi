using System;
using System.ComponentModel.DataAnnotations;

namespace Foramag.Models
{
    public class Document
    {
        [Key]

        public string Num_Document { get; set; } // 👈 Primary key
        public DateTime Date_Document { get; set; }
        public string Code_Client { get; set; }
        public string Nom_Client { get; set; }
        public decimal Total_TTC { get; set; }
        public string Type { get; set; }
        public int Code_COM { get; set; }
        public string Transporteur { get; set; }
        public string Num_Expedition { get; set; }
    }
}