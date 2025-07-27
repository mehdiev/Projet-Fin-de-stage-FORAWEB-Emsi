using System.ComponentModel.DataAnnotations;

namespace Foramag.Models
{
    public class V_Doc_lignes
    {
        [Key]
        public string Num_Document { get; set; }           // Identifiant du document (facture, etc.)

        public string Reference { get; set; }              // Code produit
        public string Designation { get; set; }            // Nom ou libellé article
        public int Quantite { get; set; }                  // Quantité commandée
        public decimal Remise { get; set; }                // Remise en pourcentage
        public decimal Prix_Unitaire { get; set; }         // Prix unitaire
    }
}