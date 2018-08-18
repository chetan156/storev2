using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace storev2.Models
{
    [Table("Purchase")]
    public class PurchaseMaster
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PurchaseId { get; set; }
        public DateTime PurchaseDate { get; set; }
        public string PurchaseBillNo { get; set; }
        [ForeignKey("ModelMaster")]
        public int PurchaseModelId { get; set; }
        public int PurchaseQuantity { get; set; }
        public string PurchaseNaration { get; set; }
        [ForeignKey("CatagoryMaster")]
        public int PurchaseCatagoryId { get; set; }
        [ForeignKey("CompanyMaster")]
        public int CompanyId { get; set; }

        public virtual CatagoryMaster CatagoryMaster { get; set; }
        public virtual ModelMaster ModelMaster { get; set; }
        public virtual CompanyMaster CompanyMaster { get; set; }

    }
}